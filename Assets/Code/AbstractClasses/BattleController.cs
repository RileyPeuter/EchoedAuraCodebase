    using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.ComponentModel.Design;

public abstract class BattleController : MonoBehaviour
{
    //###MemberVariables###
    public static BattleController ActiveBattleController;

    protected bool controllerActive = true;

    public List<BattleCharacterObject> characters;

    ExWhyCellField playersVision;
    ExWhyCellField fogOfWar;


    GameObject hoverAA;
    GameObject currentAA;
    AttackAttempt aa;
    GameObject selectedSatsPanel;
    GameObject StandOffObject;
    Ability selectedAbility;

    protected BattleCutscene activeCutscene;

    protected GameObject miscMenu;

    public BattleEventLog BEL;

    protected ObjectiveList objList;

    Material alliedMaterial;
    Material selectedMaterial;
    Material enemyMaterial;

    ActionMenuController AMC;

    protected CharacterSheetScript CSS;

    public AbilityRange AR;

    protected MapController map;

    public List<AICluster> AIClusters;

    protected int turnTimer = 0;
    public bool hasControl = true;

    protected bool scriptedDeath;

    protected GameObject objectiveHighlightPrefab;
    protected GameObject hoverCursor;
    protected GameObject cursor;
    protected ExWhyCell cursorCell;
    protected Window currentTarget;
    bool ticking = true;
    BattleCharacterObject activeCharacter;
    protected UIController BUIC;

    protected TimerUIScript turnTimerUI;

    Vector3 lastMousePosition;

    protected List<ExWhyCell> spawnCells;

    Camera mainCamera;

    protected List<StoredCharacterObject> charactersToSpawn;

    protected List<GameObject> objectiveHighlights;

    protected List<TacticalAbility> baseTacticalAbilities;

    CellInformationController activeCellInformation;
    CellInformationController hoverCellInformationController;
    CellInformationController targetCellInformation;

    public int tacticalPoints = 0;

    protected int goldRewards = 0;

    protected int casualties = 0;

    public bool stealth = false;

    //###Getters###
    public string getMapResourceString()
    {
        return map.GetResourceName();
    }
    public UIController GetBattleUIController()
    {
        return BUIC;
    }

    public int getTurnTime()
    {
        return turnTimer;
    }

    public BattleCharacterObject getActiveCharacter()
    {
        return activeCharacter;
    }

    public void spendTacticalPoints(int amount = 1)
    {
        tacticalPoints = tacticalPoints - amount;
    }

    //###Setters###
    public void addCharactersToLoad(List<StoredCharacterObject> SCOs)
    {
        charactersToSpawn = SCOs;
        print(charactersToSpawn.Count);
    }
    public void setCharactersToSpawn(List<StoredCharacterObject> nCharactersToSpawn)
    {
        charactersToSpawn = nCharactersToSpawn;
    }

    //###Utilities###
    public void addTacticalPoints()
    {
        GameObject.Instantiate(Resources.Load<GameObject>("TacticalPointsPopup")).transform.position = (mainCamera.ScreenToWorldPoint(turnTimerUI.GetComponent<RectTransform>().position) + new Vector3(-4, 0, 9));
        tacticalPoints += 1;
        turnTimerUI.updateTacticalPoints(tacticalPoints);
    }

    //These should be moved to antoehr one
    public List<BattleCharacterObject> getAllAlliegiances(List<CharacterAllegiance> allegiances)
    {
        List<BattleCharacterObject> output = new List<BattleCharacterObject>();
        foreach (CharacterAllegiance allegiance in allegiances)
        {
            foreach (BattleCharacterObject character in characters)
            {
                if (character.GetAllegiance() == allegiance)
                {
                    output.Add(character);
                }
            }
        }
        return output;
    }

    public ExWhyCell moveTowardsAlliegence(ExWhyCell perspective, CharacterAllegiance ally)
    {
        ExWhyCell currentClosest = null;
        int currentClosestDisatance = 99999;
        foreach(BattleCharacterObject BCO in getAllAllegiance(ally)){
            int possible = ExWhy.getDistanceBetweenCells(BCO.getOccupying(), perspective);
            if(possible < currentClosestDisatance)
            {
                currentClosestDisatance = possible;
                currentClosest = BCO.getOccupying();
            }
        }

        if(currentClosest == null)
        {
            Debug.Log("Hey, you're moving towards null. There might not be a valid target for this");
        }
        return currentClosest;
    }


    public ExWhyCell moveTowardsAlliegences(ExWhyCell perspective, List<CharacterAllegiance> allies)
    {
        ExWhyCell currentClosest = null;
        int currentClosestDisatance = 99999;

        foreach (CharacterAllegiance ally in allies)
        {
            foreach (BattleCharacterObject BCO in getAllAllegiance(ally))
            {
                int possible = ExWhy.getDistanceBetweenCells(BCO.getOccupying(), perspective);
                if (possible < currentClosestDisatance)
                {
                    currentClosestDisatance = possible;
                    currentClosest = BCO.getOccupying();
                }
            }
        }
        if (currentClosest == null)
        {
            Debug.Log("Hey, you're moving towards null. There might not be a valid target for this");
        }
        return currentClosest;
    }


    public List<BattleCharacterObject> getAllAllegiance(CharacterAllegiance allegiance)
    {
        List<BattleCharacterObject> output = new List<BattleCharacterObject>();
        foreach (BattleCharacterObject character in characters)
        {
            if (character.GetAllegiance() == allegiance)
            {
                output.Add(character);
            }
        }
        return output;
    }

    public List<GameObject> getCharacterGOsFromStored()
    {
        List<GameObject> output = new List<GameObject>();

        foreach (StoredCharacterObject SCO in charactersToSpawn)
        {
            GameObject GO = Resources.Load<GameObject>("TestAssets/" + SCO.GetCharacter().resourceString);
            GO = Instantiate(GO);
            GO.GetComponent<BattleCharacterObject>().initialize(SCO.GetCharacter(), CharacterAllegiance.Controlled);
            output.Add(GO);
        }

        return output;
    }

    //M, use this function reponsibliy 0_0... I'm keeping my eyes on you.
    public void forceCast(BattleCharacterObject caster, ExWhyCell target, Ability ab)
    {
        if (ab.name == "Move")
        {
            ab.cast(target, caster);
            createStandOff(null, ab, caster, null);

        }
        if (ab.abilityType == AbilityType.Area && !(ab is TacticalAbility))
        {
        }

        if (ab.abilityType == AbilityType.Targeted && !(ab is TacticalAbility )) 
        { 
            createStandOff(new AttackAttempt(caster, target.occupier, ab));
        }
    }

    public bool getCastable(Ability abi, CharacterAllegiance targetAllegiance)
    {
        //I'm not sure we're removing this AR
        AbilityRange AbRa = new AbilityRange(abi.GetRange(), activeCharacter.getOccupying(), map.gridObject, abi.GetRangeMode());
        
        AbRa.findCellsInRange(RangeMode.Simple);
        foreach (BattleCharacterObject BCO in AbRa.findCharactersInRange())
        {
            if (BCO.GetAllegiance() == targetAllegiance)
            {
                return true;
            }
        }

        return false;
    }

    public bool getCastable(Ability abi)
    {
        //I'm not sure we're removing this AR

        AbilityRange AbRa = new AbilityRange(abi.GetRange(), activeCharacter.getOccupying(), map.gridObject, abi.GetRangeMode());
        
        //AbRa.initalize(abi.GetRange(), activeCharacter.getOccupying(), ExWhy.activeExWhy);
        AbRa.findCellsInRange(RangeMode.Simple);
        foreach (BattleCharacterObject BCO in AbRa.findCharactersInRange())
        {
            if (BCO.GetAllegiance() == CharacterAllegiance.Allied || BCO.GetAllegiance() == CharacterAllegiance.Controlled)
            {
                return true;
            }
        }

        return false;
    }

    public void destroyARVisual()
    {
        if (AR != null)
        {
            AR.despawnVisuals();
        }
    }

    float tickTime = 0.5f;
    float timer = 0.5f;

    public void endTurn()
    {
        if (AR != null)
        {
            AR.despawnVisuals();
            AR = null;
        }
        selectedAbility = null;

        BEL.addEvent( BattleEventType.EndTurn, activeCharacter.getNameID(), activeCharacter.GetAllegiance().ToString());

        activeCharacter.endTurn();
        activeCharacter.clearAniBools();
        destroyTurnUI();
        timerTick();
        

        ticking = true;
    }

    public void spawnEnemyVision()
    {

        foreach (AICluster AIC in AIClusters)
        {
            AIC.setMemeberModes();
            if (!AIC.getAlerted())
            {
                AIC.generateVision(map.gridObject, BEL);
            }
        }
    }

    public void spawnCharacter(GameObject GO, CharacterAllegiance CA, ExWhyCell EWC, Character CH, AICluster AIC = null)
    {
        BattleCharacterObject BCO = GO.GetComponent<BattleCharacterObject>();
        BCO.initialize(EWC.xPosition, EWC.yPosition, CH, CA, turnTimer);
        BCO.spawnCharacter(map.gridObject);
        //BCO.move(EWC);
        characters.Add(BCO);
        if(AIC != null)
        {
            AIC.ClusterMembers.Add(BCO.getAI());
        }
    }

    public void updateVision()
    {
        if (playersVision != null)
        {
            playersVision.despawnVisuals();
        }

        playersVision = new ExWhyCellField(map.gridObject);
        
         foreach (BattleCharacterObject BCO in getAllAllegiance(CharacterAllegiance.Controlled))
        {
            ExWhyCellField test = BCO.calculateFieldOfView(map.gridObject);
            playersVision.Add(test);
        }
        if (fogOfWar != null)
        {
            fogOfWar.despawnVisuals();
        }

        playersVision.setPrefab();
        playersVision.spawnVisuals(Color.blue * new Color(0.2f, 0.2f, 0.2f, 0.2f));

        fogOfWar = new ExWhyCellField(map.gridObject.getMapAsField(), map.gridObject);
        fogOfWar.Remove(playersVision);
        fogOfWar.setPrefab("FogOfWar");
        fogOfWar.spawnVisuals();

        spawnEnemyVision();


    }

    //###InitalizationMethods
    protected void initializeGameState()
    {
        enemyMaterial = Resources.Load<Material>("BattleAssets/EnemyRenderer");
        selectedMaterial = Resources.Load<Material>("BattleAssets/SelectedRenderer");
        alliedMaterial = Resources.Load<Material>("BattleAssets/AllyRenderer");
        timerTick();
    }

    protected void clearObjectiveHighlights()
    {
        foreach(GameObject go in objectiveHighlights)
        {
            GameObject.Destroy(go);
        }
        objectiveHighlights.Clear();
    }

    protected void addObjectiveHighlight(BattleCharacterObject nBCO)
    {
        objectiveHighlights.Add(Instantiate(objectiveHighlightPrefab, nBCO.gameObject.transform));
    }

    protected void addObjectiveHighlight(int xPosition, int yPosition)
    {
        objectiveHighlights.Add(Instantiate(objectiveHighlightPrefab, map.gridObject.gridCells[xPosition, yPosition].getTransform()));
    }


    public void initializeBattleController()
    {
        objectiveHighlightPrefab = Resources.Load<GameObject>("ObjectiveHighlight");
        activeCellInformation = GameObject.Instantiate(Resources.Load<GameObject>("UIElements/uI_CurrentActiveCell_Panel"), GameObject.Find("Canvas").transform).GetComponent<CellInformationController>();
        baseTacticalAbilities = new List<TacticalAbility>();
        objectiveHighlights = new List<GameObject>();
        mainCamera = Camera.main;
        ActiveBattleController = this;
        characters = new List<BattleCharacterObject>();
        BUIC = GameObject.Find("MapController").GetComponent<UIController>();
        BUIC.initialize(this);
        map = this.gameObject.GetComponent<MapController>();
        cursor = GameObject.Instantiate(Resources.Load<GameObject>("MapTiles/Prefabs/General/TileCursor"));
        hoverCursor = GameObject.Instantiate(Resources.Load<GameObject>("MapTiles/Prefabs/General/HoverCursor"));


        AIClusters = new List<AICluster>();

        loadCharacters();

        GameObject combatLog = BUIC.openWindow("uI_CombatLog_Panel", false, "Canvas", false);
        BEL = combatLog.GetComponent<BattleEventLog>();
        BEL.initialze();

    }



    protected void initializeAdditionalElements()
    {
        objList = GameObject.Instantiate(Resources.Load<GameObject>("UIElements/uI_Objectives_Panel"), GameObject.Find("Canvas").transform).GetComponent<ObjectiveList>();
        objList.initialize(this);

        GameObject charSheet = BUIC.openWindow("uI_CharacterSheet_Panel", false, "Canvas", false);
        CSS = charSheet.GetComponent<CharacterSheetScript>();
        CSS.initialize(UIController.HighestWindow);

        miscMenu = GameObject.Instantiate(Resources.Load<GameObject>("UIElements/uI_MiscMenus_Panel"), GameObject.Find("Canvas").transform);
        miscMenu.GetComponent<MiscMenuController>().initialize(BUIC, this, objList.gameObject, BEL.gameObject, charSheet);
        BEL.addListener(objList);


        turnTimerUI = GameObject.Instantiate(Resources.Load<GameObject>("UIElements/uI_Timer_Panel"), GameObject.Find("Canvas").transform).GetComponent<TimerUIScript>();
        turnTimerUI.initialize(this);
        Instantiate(ResourceLoader.loadGameObject("UIElements/uI_EscapeMenu_Button"), GameObject.Find("Canvas").transform);
    }

    public void lookForBattleEventListeners()
    {
        foreach(TacticalAbility tacticalAbility in baseTacticalAbilities)
        {
            if (tacticalAbility is BattleEventListener)
            {
                BEL.addListener((BattleEventListener)tacticalAbility);
            }
        }
    }

    //###UIOperation###
    public void CreateAttackAttempt(BattleCharacterObject attacker, BattleCharacterObject attackee, Ability ab)
    {
        aa = new AttackAttempt(attacker, attackee, ab);

        currentAA = BUIC.openWindow("uI_AttackAttampet_Panel", true, "Canvas", false);
        currentAA.GetComponent<AttackAttemptUIController>().initialize(UIController.HighestWindow, aa);
    }

    public void deSelectAbility()
    {
        selectedAbility = null;
        if (AR != null)
        {
            AR.despawnVisuals(); 
        }
        aa = null;
        if (activeCharacter != null)
        {
            activeCharacter.setAniAnticipate(false);
            activeCharacter.setAniRun(false);
        }
    }

    public void selectAbility(Ability ability)
    {
        GameObject GO = BUIC.openWindow("uI_SelectedAbility_Panel", true);
        GO.GetComponent<AbilitySnippetController>().initialize(UIController.HighestWindow, ability);
        GO.GetComponentInChildren<Button>().interactable = false;
        GO.GetComponentInChildren<BackButtonController>().setDeselectListener(this);
        selectedAbility = ability;
        
        if (AR != null)
        {
            AR.despawnVisuals();
        }

        if (ability.name == "Move")
        {
            activeCharacter.clearAniBools();
            activeCharacter.setAniRun(true);
        }
        else
        {
            activeCharacter.clearAniBools();
            activeCharacter.setAniAnticipate(true);
        }

        createRange(ability);

        BUIC.lockFocus(GO.GetComponent<AbilitySnippetController>());

        if (cursorCell.occupier != activeCharacter)
        {
            checkTarget();
        }
        if(ability.abilityType == AbilityType.Self) { tryCast(selectedAbility); }
    }

    AbilityRange createRange(Ability ability)
    {
        AR = new AbilityRange(ability.GetRange(), activeCharacter.getOccupying(), map.gridObject, ability.GetRangeMode()); //this.gameObject.AddComponent<AbilityRange>();
       // AR.initalize(ability.GetRange(), activeCharacter.getOccupying(), map.gridObject);

        RangeMode rangeMode = ability.GetRangeMode();

        if (rangeMode == RangeMode.Custom)
        {
            AR.setCellsInRange(ability.getCustomRange());
        }
        else 
        {
            AR.findCellsInRange(rangeMode);
        }

        AR.setPrefab();
        AR.spawnVisuals();

        return AR;
    }

    public void timerTick()
    {
        if(!(activeCharacter is null)) {
            switch (activeCharacter.GetAllegiance())
            {
                case CharacterAllegiance.Controlled:
                    activeCharacter.GetComponent<SpriteRenderer>().material = alliedMaterial;
                    break;
                case CharacterAllegiance.Enemey:
                    activeCharacter.GetComponent<SpriteRenderer>().material = enemyMaterial;
                    break;
            }
        }

        foreach (BattleCharacterObject character in characters) 
        {
            if (character.isNextTurn(turnTimer)) {
                
                if(BEL != null) { 
                    BEL.addEvent(BattleEventType.Time, "", "", "", turnTimer.ToString());
                }

                setActiveCharacter(character);
                GameObject.Instantiate(Resources.Load<GameObject>("UIElements/uI_TurnName_Panel"), GameObject.Find("Canvas").transform).GetComponent<TurnNameController>().initialize(activeCharacter.getName());
                return;
            }


        }
        turnTimer++;
        turnTimerUI.updateTime(turnTimer);
        
        timerTick();
        //fam
    }

    public void setActiveCharacter(BattleCharacterObject character)
    {
        hasControl = false;
        activeCharacter = character;
        if (character.GetAllegiance() == CharacterAllegiance.Controlled) {
            openTurnUI(character);
            activeCharacter.GetComponent<SpriteRenderer>().material = selectedMaterial;
            if (activeCutscene is null)
            {
                hasControl = true;
            }
        }
        activeCellInformation.initialize(activeCharacter.getOccupying(), true);
        cursorCell = activeCharacter.getOccupying();
        updateVision();
    }

    void openTurnUI(BattleCharacterObject character)
    {
        GameObject GO = BUIC.openWindow("uI_CurrentActiveStat_Panel", false, "Canvas", false);
        GO.GetComponent<MiniStatsController>().initialize(UIController.HighestWindow, character);
        AMC = BUIC.openWindow("uI_Actions_Panel", false, "Canvas", false).GetComponent<ActionMenuController>();
        AMC.initialize(UIController.HighestWindow, activeCharacter);
        AMC.GetComponent<ActionMenuController>().setBUIC(BUIC);
    }

    public void checkHotkeys()
    {
        if (AMC)
        {
            AMC.checkHotkeys();
        }
    }

    // test
    public void finishMove(int amount = -1)
    {

        ticking = true;
        if (amount > 0)
        {
            //Might Need a switch Statement here

        }

        deSelectAbility();
        BUIC.closeAllTransient();
        if(activeCharacter is null)
        {
            timerTick();
            return;
        }

        if (activeCharacter.getManaFlow() <= 0 && (activeCharacter.getMovement() <= 0 && tacticalPoints <= 0))
        {
            endTurn();
        }
        else
        {
            setActiveCharacter(activeCharacter);
        }
    }

    public void toggleCutscene(bool toggle)
    {
        BUIC.makeActive(!toggle);
        hasControl = !toggle;
        ticking = !toggle;
        if (!toggle)
        {
            activeCutscene = null;
        }
    }

    public void checkHoverCellTarget(ExWhyCell cell)
    {
        if (hoverAA)
        {
            Destroy(hoverAA);
        }

        if (cell.occupier is null)
        {
            return;
        }

        if(selectedAbility is null)
        {
            return;
        }

        if (selectedAbility.abilityType == AbilityType.Targeted)
        {
            hoverAA = BUIC.openWindow("uI_MiniAttackAttampet_Panel", true, "Canvas", false);
            hoverAA.GetComponent<AttackAttemptUIController>().initialize(UIController.HighestWindow, new AttackAttempt(activeCharacter, cell.occupier, selectedAbility));
            hoverAA.GetComponent<RectTransform>().localPosition += mainCamera.WorldToScreenPoint((Vector2)  cell.cellGO.transform.position);
        }
    }

    protected void controls()
    {
        cursor.transform.position = cursorCell.cellGO.transform.position;

        //This should be moved to a function which moves the cursor cells. 
        if (Vector3.Distance(cursor.transform.position, mainCamera.transform.position) > 25)
        {
            //mainCamera.transform.position =  cursor.transform.position + new Vector3(0,0,-10);
            mainCamera.transform.position = ((mainCamera.transform.position + cursor.transform.position) / 2) + new Vector3(0, 0, -10);
        }

        if (!hasControl)
        {
            return;
        }

        checkHotkeys();

        BUIC.checkBackButtons();

        if (hoverCursor.transform.position != map.getMouseCell().cellGO.transform.position)
        {
            hoverCursor.transform.position = map.getMouseCell().cellGO.transform.position;
            checkHoverCellTarget(map.getMouseCell());
        }

        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            moveCursor(map.getMouseCell());
        }

        if (Input.GetKeyDown("w"))
        {
            if (cursorCell.yPosition != map.gridObject.yMax - 1)
            {
                moveCursor(map.gridObject.gridCells[cursorCell.xPosition, cursorCell.yPosition + 1]);
            }
        }

        if (Input.GetKeyDown("s"))
        {
            if (cursorCell.yPosition != 0)
            {
                moveCursor(map.gridObject.gridCells[cursorCell.xPosition, cursorCell.yPosition - 1]);
            }
        }

        if (Input.GetKeyDown("a"))
        {
            if (cursorCell.xPosition != 0)
            {
                moveCursor(map.gridObject.gridCells[cursorCell.xPosition - 1, cursorCell.yPosition]);
            }
        }

        if (Input.GetKeyDown("d"))
        {
            if (cursorCell.xPosition != map.gridObject.xMax - 1)
            {
                moveCursor(map.gridObject.gridCells[cursorCell.xPosition + 1, cursorCell.yPosition]);
            }
        }

        if (Input.GetKeyDown("space") || Input.GetMouseButtonDown(1))
        {
            cursorCell = map.getMouseCell();
            checkTarget();
            tryCast(selectedAbility);
        }

        if (!Input.GetKey("left alt"))
        {
            hoverCellInformationController = null;
        }

        if (Input.GetKeyDown("left alt"))
        {
            GameObject GO = BUIC.openWindow("uI_HoverCell_Panel");
            GO.GetComponent<RectTransform>().position = mainCamera.WorldToScreenPoint(hoverCursor.transform.position) + new Vector3(-130, 0, 0);
            hoverCellInformationController = GO.GetComponent<CellInformationController>();
            hoverCellInformationController.initialize(map.getMouseCell());
        }

        if ((Input.GetKey("left shift") && Input.GetKeyDown("e")) || Input.GetKeyDown("left shift") && Input.GetKey("e"))
        {
            endTurn();
        }

    }

    public void moveCursor(ExWhyCell nCursorCell, bool needsVisual = false)
    {
        if (targetCellInformation != null)
        {
            targetCellInformation.closeWindow();
        }

        cursorCell = nCursorCell;
        if (playersVision.contains(nCursorCell) || !needsVisual)
        {
            checkTarget();
        }

    }

    //I think this method is gonna be wacky
    //If we decide to switch to the generic damage thing, this could be made cleaner
    public void quickSelectCheck(ExWhyCell cellToCheck)
    {
        Ability toBeSelected = null;
        
        toBeSelected = activeCharacter.getBasicAbilities().Find(x => x.name == "MeleeStrike");
        if (toBeSelected != null && cellToCheck.occupier != null)
        {
            if (toBeSelected.isCastable(activeCharacter) && getCastable(toBeSelected, CharacterAllegiance.Enemey))
            {
                selectAbility(toBeSelected);
                return;
            }
        }

        toBeSelected = activeCharacter.getBasicAbilities().Find(x => x.name == "RangedStrike");
        if (toBeSelected != null && cellToCheck.occupier != null)
        {
            if (toBeSelected.isCastable(activeCharacter))
            {
                selectAbility(toBeSelected);
                return;
            }
        }

        selectAbility(activeCharacter.getMovementAbility());
    }

    public bool checkValidTarget(AbilityRange range, ExWhyCell target, bool needsOccupied, bool needsUnoccpied, AbilityType abilityType)
    {
        if (abilityType == AbilityType.Self) { return true; }
        
        if(needsOccupied && target.occupier == null) { return false;}

        if(needsUnoccpied && target.occupier != null) { return false; }

        if (range.findCellsInRange(RangeMode.Custom).Contains(target)) { return true; }

        return false;
    }

    //This is horrible, we need to change it
    void tryCast(Ability ability)
    {
        timer = 1;
        if (ability is null)
        {
            cursorCell = map.getMouseCell();
            quickSelectCheck(cursorCell);
            return;
        }
        ticking = false;

        List<ExWhyCell> range = AR.findCellsInRange(ability.GetRangeMode());

        if(ability.name == "Move")
        {
            if(checkValidTarget(AR, cursorCell, false, true, AbilityType.Targeted))
            {
                BEL.addEvent(BattleEventType.Movement, activeCharacter.getNameID(), "Move", cursorCell.ToString());

                ability.cast(cursorCell, activeCharacter);
                createStandOff(null, ability, activeCharacter, null);
                return;
            }
            else
            {
                ticking = true;
                quickSelectCheck(cursorCell);
                return;
            }
        }

        switch (ability.abilityType)
        {
            case AbilityType.Self:
                BEL.addEvent(BattleEventType.Attack, activeCharacter.getNameID(), ability.name);
                if (ability is TacticalAbility)
                {
                    createStandOff(null, null, activeCharacter, (TacticalAbility)ability);
                }
                else
                {
                    createStandOff(null, ability, activeCharacter);
                }
                return;

            case AbilityType.Targeted:
                if(checkValidTarget(AR, cursorCell, true, false, AbilityType.Targeted))
                {   
                    BEL.addEvent(BattleEventType.Attack, activeCharacter.getNameID(), ability.name, cursorCell.occupier.getNameID());
                    if (ability is TacticalAbility)
                    {
                        createStandOff(null, null, activeCharacter, (TacticalAbility)ability);
                    }
                    else
                    {
                        createStandOff(aa);
                    }
                    return;
                }
            break;

            case AbilityType.Support:
                if(checkValidTarget(AR, cursorCell, true, false, AbilityType.Support))
                {
                    BEL.addEvent(BattleEventType.Attack, activeCharacter.getNameID(), ability.name, cursorCell.occupier.getNameID());
                    if(ability is TacticalAbility)
                    {
                        createStandOff(null, null, null, (TacticalAbility)ability);
                    }
                    return;
                }

            break;
        }

        
       
        deSelectAbility();
        quickSelectCheck(cursorCell);
    }

    public void createStandOff(AttackAttempt nAttackAttempt = null,  Ability nAbility = null, BattleCharacterObject nBCO = null, TacticalAbility nTacticalAbility = null)
    {
        hasControl = false;
        destroyARVisual();
        BUIC.closeAllTransient();
        destroyTurnUI();

        GameObject AAC = GameObject.Instantiate(Resources.Load<GameObject>("UIElements/S"), GameObject.Find("Main Camera").transform);

        if (nTacticalAbility != null)
        {
            AAC.AddComponent<TacticalStandOffController>().initialize(nTacticalAbility, nBCO, map.GetResourceName(), BUIC, cursorCell);
            return;
        }

        if (nAbility != null)
        {
            AAC.AddComponent<SoloStandOffController>().initialize(nAbility, nBCO, map.GetResourceName(), BUIC);
            return;
        }

        if (nAttackAttempt != null)
        {
            AAC.AddComponent<DualStandOffController>().initialize(nAttackAttempt, this, BUIC);
            aa = null;
            return;
        }
        Debug.Log("Hey, fam. You didn't make a StandOff for some reason");
    }


    public void destroyTurnUI()
    {
        BUIC.closeAllTransient();
        if (!(GameObject.Find("uI_CurrentActiveStat_Panel(Clone)") is null))
        {
            GameObject.Find("uI_CurrentActiveStat_Panel(Clone)").GetComponent<MiniStatsController>().closeWindow();
        }

        if (!(GameObject.Find("uI_Actions_Panel(Clone)") is null))
        {
            GameObject.Find("uI_Actions_Panel(Clone)").GetComponent<ActionMenuController>().closeWindow();
        }
        }


    void checkTarget()
    {

        if (currentAA)
        {
            Destroy(currentAA);
        }
        if (selectedSatsPanel)
        {
            Destroy(selectedSatsPanel);
        }

        if(targetCellInformation != null){
            targetCellInformation.closeWindow();
        }

        targetCellInformation = BUIC.openWindow("uI_TargetCell_Panel").GetComponent<CellInformationController>();
        targetCellInformation.initialize(cursorCell, true);

        if (!(cursorCell.occupier is null))
        {
            //Maybe change this to disabled instead of realoading
            selectedSatsPanel = BUIC.openWindow("uI_CurrentTargetStat_Panel", true, "Canvas", false);
            selectedSatsPanel.GetComponent<MiniStatsController>().initialize(UIController.HighestWindow, cursorCell.occupier);
            miscMenu.GetComponent<MiscMenuController>().setSheetCharacter(cursorCell.occupier.getCharacter());

            if (!(selectedAbility is null ) && selectedAbility.abilityType == AbilityType.Targeted)
            {
                CreateAttackAttempt(activeCharacter, cursorCell.occupier, selectedAbility);
                
            }
        }
    }

    protected void aiTick()
    {
        if (selectedAbility is null)
        {
            Ability currentSelect = activeCharacter.getAI().getAbility();

            if(currentSelect is null)
            {
                endTurn();
                return;
            }

            selectAbility(currentSelect);
            
            if (selectedAbility == activeCharacter.getAI().getOverrideAbility()) { activeCharacter.getAI().setOverrideAbility(null); }
            //if(selectedAbility.abilityType == AbilityType.Self) { tryCast(); }
            
            return;
        }

        //There is a target switching bug that looks cool, keep it in unless it fucks up    
        ExWhyCell target = activeCharacter.getAI().getTarget(selectedAbility, AR);
        if(target is null) { endTurn(); return; }
        if (cursorCell != target)
        {
            cursorCell = target;
            checkTarget();
            return;
        }

        tryCast(selectedAbility);
    }

    public void killCharacter(BattleCharacterObject character)
    {
        BEL.addEvent(BattleEventType.Kill, character.getNameID());
        character.setAniDie();
        characters.Remove(character);
        Destroy(character.characterObject, 2f);
        character.getOccupying().occupier = null;
        if(character == activeCharacter) {  activeCharacter = null; }

        if (getAllAllegiance(CharacterAllegiance.Controlled).Count == 0)
        {
            Instantiate(Resources.Load<GameObject>("Gameover"), GameObject.Find("Canvas").transform);
            Destroy(this.gameObject); return;
        }
    }

    public void exileCharacter(BattleCharacterObject character)
    {
        characters.Remove(character);
        character.getOccupying().occupier = null;
        Destroy(character.characterObject);
        if (character == activeCharacter) { activeCharacter = null; }
    }

    protected void ticker()
    {
        if (ticking)
        {
            timer = timer - Time.deltaTime;
        }
        if(timer < 0)
        {
            timer = tickTime;
            aiTick();
        }
    }

    public BattleCharacterObject getCharacterFromNameID(string name)
    {
        foreach (BattleCharacterObject battleCharacterObject in characters) 
        {
            if (battleCharacterObject.getNameID() == name)
            {

            return battleCharacterObject; 
            }
        }
        Debug.Log("Fam, there's a wrong character name. maybe they died");
        return null;
    }

    //Bro, this won't work for duplicates, don't over rely on this.
    public BattleCharacterObject getCharacterFromName(string name)
    {
        foreach (BattleCharacterObject battleCharacterObject in characters)
        {
            if (battleCharacterObject.getName() == name)
            {

                return battleCharacterObject;
            }
        }
        Debug.Log("Fam, there's a wrong character name. maybe they died");
        return null;
    }

    public void spawnDecorations(Vector2 location, string decorationName)
    {
        Instantiate(Resources.Load<GameObject>("Decorations/" + decorationName)).transform.position = location;
    }

    //Obscelete for now
    public void getMouseCell()
    {
        int x;
        int y;

        Vector3 mousePoint = Camera.main.ScreenToWorldPoint(Input.mousePosition); ;

        x = Mathf.RoundToInt((mousePoint.x) / 4);
        y = Mathf.RoundToInt((mousePoint.y) / 4);

        if ((y < 0 || x < 0) || (y > map.gridObject.yMax || x > map.gridObject.xMax))
        {
            return;
        }

        cursorCell = map.gridObject.gridCells[x, y];
    }

    public virtual bool checkExtraction(bool exit = true)
    {
        if(getAllAllegiance(CharacterAllegiance.Controlled).Count == 1)
        {
            if (exit)
            {
                openEndWindow();
            }
            return true;
        }
        return false;

    }

    //###Abstracts###
    public virtual List<TacticalAbility> getTacticalAbilities()
    {
        List<TacticalAbility> output = new List<TacticalAbility>();

        foreach(TacticalAbility tAbility in baseTacticalAbilities)
        {
            output.Add(tAbility);
        }

        return output;
    }

    public virtual void interact(int index)
    {
        BEL.addEvent(BattleEventType.Interact, activeCharacter.getNameID(), index.ToString());
    }

    public virtual void openEndWindow()
    {
        GameObject.Instantiate(Resources.Load<GameObject>("UIElements/uI_MissionComplete_Panel"), GameObject.Find("Canvas").transform).GetComponent<EndMissionController>().initialize(this, goldRewards, casualties, turnTimer);
    }

    public virtual void battleTriggers(string id){}

    public virtual void fieldTrigger(string id) {}

    public abstract void loadCharacters();

    public abstract void endBattle();

    public abstract void objectiveComplete(string id);

    public abstract void GBCTexecutions(int index);
}