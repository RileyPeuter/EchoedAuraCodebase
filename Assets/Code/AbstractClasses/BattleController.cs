using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Security.Cryptography.X509Certificates;

public abstract class BattleController : MonoBehaviour
{
    //###MemberVariables###
    public static BattleController ActiveBattleController;

    public List<BattleCharacterObject> characters;

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

    protected GameObject hoverCursor;
    protected GameObject cursor;
    protected ExWhyCell cursorCell;
    protected Window currentTarget;
    bool ticking = true;
    BattleCharacterObject activeCharacter;
    protected BattleUIController BUIC;

    protected TimerUIScript turnTimerUI;

    Vector3 lastMousePosition;

    protected List<ExWhyCell> spawnCells;

    Camera mainCamera;

    List<StoredCharacterObject> charactersToSpawn;

    protected List<TacticalAbility> baseTacticalAbilities;

    CellInformationController activeCellInformation;
    CellInformationController hoverCellInformationController;
    CellInformationController targetCellInformation;

    int tacticalPoints = 0;

    //###Getters###
    public string getMapResourceString()
    {
        return map.GetResourceName();
    }
    public BattleUIController GetBattleUIController()
    {
        return BUIC;
    }

    public BattleCharacterObject getActiveCharacter()
    {
        return activeCharacter;
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
        GameObject.Instantiate(Resources.Load<GameObject>("TacticalPointsPopup")).transform.position = (mainCamera.ScreenToWorldPoint(turnTimerUI.GetComponent<RectTransform>().position) + new Vector3(-4,0,9));
        tacticalPoints += 1;
        turnTimerUI.updateTacticalPoints(tacticalPoints);
        print(tacticalPoints);
    }

    //These should be moved to antoehr one
    public List<BattleCharacterObject> getAllAlliegiances (List<CharacterAllegiance> allegiances)
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

        foreach(StoredCharacterObject SCO in charactersToSpawn)
        {
            GameObject GO = Resources.Load<GameObject>("TestAssets/" + SCO.GetCharacter().resourceString);
            GO = Instantiate(GO);
            GO.GetComponent<BattleCharacterObject>().initialize(SCO.GetCharacter(), CharacterAllegiance.Controlled);
            output.Add(GO);
        }

        return output;
    }
    
    public bool getCastable(Ability abi, CharacterAllegiance targetAllegiance)
    {
        //I'm not sure we're removing this AR
        AbilityRange AbRa = gameObject.AddComponent<AbilityRange>();
        print(abi.name);
        print(activeCharacter.getName());

        AbRa.initalize(abi.GetRange(), activeCharacter.getOccupying(), ExWhy.activeExWhy);
        AbRa.findCellsInRange(RangeMode.Simple);
        foreach (BattleCharacterObject BCO in AbRa.findCharactersInRange())
        {
            if (BCO.GetAllegiance() == targetAllegiance)
            {
                Destroy(AbRa);
                return true;
            }
        }

        Destroy(AbRa);  
        return false;
    }

    public bool getCastable(Ability abi)
    {
        //I'm not sure we're removing this AR
        AbilityRange AbRa = gameObject.AddComponent<AbilityRange>();
        print(abi.name);
        print(activeCharacter.getName());

        AbRa.initalize(abi.GetRange(), activeCharacter.getOccupying(), ExWhy.activeExWhy);
        AbRa.findCellsInRange(RangeMode.Simple);
        foreach (BattleCharacterObject BCO in AbRa.findCharactersInRange())
        {
            if (BCO.GetAllegiance() == CharacterAllegiance.Allied || BCO.GetAllegiance() == CharacterAllegiance.Controlled)
            {
                Destroy(AbRa);
                return true;
            }
        }

        Destroy(AbRa);
        return false;
    }

    public void destroyARVisual()
    {
        if (AR != null)
        {
            AR.destroyVisual();
        }
    }

    float tickTime = 0.5f;
    float timer = 0.5f;

    public void endTurn()
    {
        if (AR)
        {
            AR.destroyVisual();
            AR = null;
        }
        selectedAbility = null;

        BEL.addEvent(activeCharacter.getName(), "End Turn", activeCharacter.GetAllegiance().ToString());

        activeCharacter.endTurn();

        destroyTurnUI();
        timerTick();
        foreach(AICluster AIC in AIClusters)
        {
            AIC.setMemeberModes();
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

    //###InitalizationMethods
    protected void initializeGameState()
    {
        enemyMaterial = Resources.Load<Material>("BattleAssets/EnemyRenderer");
        selectedMaterial = Resources.Load<Material>("BattleAssets/SelectedRenderer");
        alliedMaterial = Resources.Load<Material>("BattleAssets/AllyRenderer");
        timerTick();
    }

    public void initializeBattleController()
    {
        activeCellInformation = GameObject.Instantiate(Resources.Load<GameObject>("UIElements/uI_CurrentActiveCell_Panel"), GameObject.Find("Canvas").transform).GetComponent<CellInformationController>();
        baseTacticalAbilities = new List<TacticalAbility>();
        mainCamera = Camera.main;
        ActiveBattleController = this;
        characters = new List<BattleCharacterObject>();
        BUIC = GameObject.Find("MapController").GetComponent<BattleUIController>();
        BUIC.initialize(this);
        map = this.gameObject.GetComponent<MapController>();
        cursor = GameObject.Instantiate(Resources.Load<GameObject>("MapTiles/Prefabs/General/TileCursor"));
        hoverCursor = GameObject.Instantiate(Resources.Load<GameObject>("MapTiles/Prefabs/General/HoverCursor"));
        loadCharacters();
        AIClusters = new List<AICluster>();
    }

    protected void initializeAdditionalElements()
    {
        objList = GameObject.Instantiate(Resources.Load<GameObject>("UIElements/uI_Objectives_Panel"), GameObject.Find("Canvas").transform).GetComponent<ObjectiveList>();
        objList.initialize(this);

        GameObject charSheet = BUIC.openWindow("uI_CharacterSheet_Panel", false, "Canvas", false);
        CSS = charSheet.GetComponent<CharacterSheetScript>();
        CSS.initialize(BattleUIController.HighestWindow, new BattleCharacterObject());
        GameObject combatLog = BUIC.openWindow("uI_CombatLog_Panel", false, "Canvas", false);

        miscMenu = GameObject.Instantiate(Resources.Load<GameObject>("UIElements/uI_MiscMenus_Panel"), GameObject.Find("Canvas").transform);
        miscMenu.GetComponent<MiscMenuController>().initialize(BUIC, this, objList.gameObject, combatLog, charSheet);
        BEL = combatLog.GetComponent<BattleEventLog>();
        BEL.initialze();
        BEL.addListener(objList);

        turnTimerUI = GameObject.Instantiate(Resources.Load<GameObject>("UIElements/uI_Timer_Panel"), GameObject.Find("Canvas").transform).GetComponent<TimerUIScript>();
        turnTimerUI.initialize(this);
    }

    //###UIOperation###
    public void CreateAttackAttempt(BattleCharacterObject attacker, BattleCharacterObject attackee, Ability ab)
    {
        aa = new AttackAttempt(attacker, attackee, ab);

        currentAA = BUIC.openWindow("uI_AttackAttampet_Panel", true, "Canvas", false);
        currentAA.GetComponent<AttackAttemptUIController>().initialize(BattleUIController.HighestWindow, aa);
    }

    public void deSelectAbility()
    {
        selectedAbility = null;
        if (AR)
        {
            AR.destroyVisual(); 
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
        GO.GetComponent<AbilitySnippetController>().initialize(BattleUIController.HighestWindow, ability);
        GO.GetComponentInChildren<Button>().interactable = false;
        GO.GetComponentInChildren<BackButtonController>().setDeselectListener(this);
        selectedAbility = ability;
        
        if (AR)
        {
            AR.destroyVisual();
        }

        if (ability.name == "Move")
        {
            activeCharacter.setAniRun(true);
        }
        else
        {
            activeCharacter.setAniAnticipate(true);
        }

        createRange(ability);

        BUIC.lockFocus(GO.GetComponent<AbilitySnippetController>());
        checkTarget();
        if(ability.abilityType == AbilityType.Self) { tryCast(); }
    }

    AbilityRange createRange(Ability ability)
    {
        AR = this.gameObject.AddComponent<AbilityRange>();
        AR.initalize(ability.GetRange(), activeCharacter.getOccupying(), map.gridObject);
        AR.findCellsInRange(ability.GetRangeMode());
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
            print(character.getName());
            if (character.isNextTurn(turnTimer)) {
                setActiveCharacter(character);
                GameObject.Instantiate(Resources.Load<GameObject>("UIElements/uI_TurnName_Panel"), GameObject.Find("Canvas").transform).GetComponent<TurnNameController>().initialize(activeCharacter.getName());
                return;
            }

            if (getAllAllegiance(CharacterAllegiance.Controlled).Count == 0)
            {
                Instantiate(Resources.Load<GameObject>("Gameover"), GameObject.Find("Canvas").transform);
                Destroy(this.gameObject); return;
            }
        }
        turnTimer++;
        turnTimerUI.updateTime(turnTimer);
        timerTick();
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
    }

    void openTurnUI(BattleCharacterObject character)
    {
        GameObject GO = BUIC.openWindow("uI_CurrentActiveStat_Panel", false, "Canvas", false);
        GO.GetComponent<Window>().initialize(BattleUIController.HighestWindow, character);
        AMC = BUIC.openWindow("uI_Actions_Panel", false, "Canvas", false).GetComponent<ActionMenuController>();
        AMC.initialize(BattleUIController.HighestWindow, activeCharacter);
        AMC.GetComponent<ActionMenuController>().setBUIC(BUIC);
    }

    public void checkHotkeys()
    {
        if (AMC)
        {
            AMC.checkHotkeys();
        }
    }

    public void finishMove(int amount = -1)
    {

        ticking = true;
        if (amount > 0)
        {

        switch (selectedAbility.abilityType)
            {
            case AbilityType.Self:
                break;

            case AbilityType.Area:
                break;

            case AbilityType.Targeted:
                break;
            }
        }

        deSelectAbility();
        BUIC.closeAllTransient();
        if(activeCharacter is null)
        {
            timerTick();
            return;
        }

        if (activeCharacter.getManaFlow() > 0 && activeCharacter.getMovement() > 0)
        {
            timerTick();
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

    protected void controls()
    { 
        cursor.transform.position = cursorCell.cellGO.transform.position;

        //This should be moved to a function which moves the cursor cells. 
        if(Vector3.Distance(cursor.transform.position, mainCamera.transform.position) > 25)
        {
            //mainCamera.transform.position =  cursor.transform.position + new Vector3(0,0,-10);
            mainCamera.transform.position = ((mainCamera.transform.position  + cursor.transform.position) / 2) + new Vector3(0, 0, -10);
        }

        if (!hasControl) 
        {
            return;
        }

        checkHotkeys();

        BUIC.checkBackButtons();

        if(Input.mousePosition != lastMousePosition)
        {   
            hoverCursor.transform.position = map.getMouseCell().cellGO.transform.position;
        }

        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            cursorCell = map.getMouseCell();
            checkTarget();
        }

        if (Input.GetKeyDown("w"))
        {   
            if (cursorCell.yPosition != map.gridObject.yMax - 1)
            {
                cursorCell = map.gridObject.gridCells[cursorCell.xPosition, cursorCell.yPosition + 1];
                checkTarget();
            }
        }

        if (Input.GetKeyDown("s"))
        {
            if (cursorCell.yPosition != 0)
            {
                cursorCell = map.gridObject.gridCells[cursorCell.xPosition, cursorCell.yPosition - 1];
                checkTarget();
            }
        }
            
        if (Input.GetKeyDown("a"))
        {
            if (cursorCell.xPosition != 0)
            {
                cursorCell = map.gridObject.gridCells[cursorCell.xPosition - 1, cursorCell.yPosition];
                checkTarget();
            }
        }

        if (Input.GetKeyDown("d"))
        {
            if (cursorCell.xPosition != map.gridObject.xMax - 1)
            {
                cursorCell = map.gridObject.gridCells[cursorCell.xPosition + 1, cursorCell.yPosition];
                checkTarget();
            }
        }

        if (Input.GetKeyDown("space") || Input.GetMouseButtonDown(1))
        {
            cursorCell = map.getMouseCell();
            checkTarget();
            tryCast(); 
        }
        
        if(!Input.GetKey("left alt"))
        {
            hoverCellInformationController = null;
        }

        if(Input.GetKeyDown("left alt"))
        {
            GameObject GO = BUIC.openWindow("uI_HoverCell_Panel");
            GO.GetComponent<RectTransform>().position = mainCamera.WorldToScreenPoint(hoverCursor.transform.position) + new Vector3(-130, 0, 0);
            hoverCellInformationController = GO.GetComponent<CellInformationController>();
            hoverCellInformationController.initialize(map.getMouseCell());
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

    //This is horrible, we need to change it
    void tryCast()
    {
        timer = 1;
        if (selectedAbility is null)
        {
            cursorCell = map.getMouseCell();
            quickSelectCheck(cursorCell);
            return;
        }
        ticking = false;
        if(selectedAbility.name == "Move")
        {
            if (AR.findCellsInRange(selectedAbility.GetRangeMode()).Contains(cursorCell))
            {
                if(cursorCell.occupier != null)
                {
                    ticking = true;
                    quickSelectCheck(cursorCell);
                    return;
                }
                BEL.addEvent(activeCharacter.getName(), "Movement", "Move", "X:" + cursorCell.xPosition.ToString() + " Y:"+ cursorCell.yPosition.ToString());

                destroyTurnUI();

                GameObject AC = GameObject.Instantiate(Resources.Load<GameObject>("UIElements/S"), GameObject.Find("Main Camera").transform);
                AC.AddComponent<SoloStandOffController>().initialize(selectedAbility, activeCharacter, map.GetResourceName(), BUIC);
                selectedAbility.cast(cursorCell, activeCharacter);
                destroyARVisual();
                checkTarget();
                hasControl = false;
            }
            return;
        }
        
        //This has to be fixed, this doesn't work in some way
        switch (selectedAbility.abilityType)
        {
        
    case AbilityType.Area:

                if (!AR.findCellsInRange(selectedAbility.GetRangeMode()).Contains(cursorCell))
                {

                    return;           
                }
                else
                {
                    BEL.addEvent(activeCharacter.getName(), "Attack", selectedAbility.name, "X:" + cursorCell.xPosition.ToString() + " Y:" + cursorCell.yPosition.ToString());
                }
                break;
            case AbilityType.Targeted:
                
                if (!AR.findCharactersInRange().Contains(cursorCell.occupier) || cursorCell == activeCharacter.getOccupying())
                {

                    deSelectAbility();
                    quickSelectCheck(cursorCell);
                    //  BEL.addEvent(activeCh aracter, "TargetedAbility", selectedAbility.name, cursorCell.occupier.name)
                    return;
                }
                else
                {
                    if(aa is null)
                    {
                        return;
                    }
                    BEL.addEvent(activeCharacter.getName(), "Attack", selectedAbility.name, cursorCell.occupier.getName());
                }
                break;

            case AbilityType.Self:
                destroyTurnUI();
                BUIC.closeAllTransient();

                destroyARVisual();

                hasControl = false;

                GameObject Thing = GameObject.Instantiate(Resources.Load<GameObject>("UIElements/S"), GameObject.Find("Main Camera").transform);
                Thing.AddComponent<SoloStandOffController>().initialize( selectedAbility, activeCharacter, map.GetResourceName(), BUIC);
                return;

                break;
        }
        
        destroyARVisual();
        BUIC.closeAllTransient();
        destroyTurnUI();

        hasControl = false;
        GameObject AAC = GameObject.Instantiate(Resources.Load<GameObject>("UIElements/S"), GameObject.Find("Main Camera").transform);
        AAC.AddComponent<DualStandOffController>().initialize(aa, this, BUIC);
        aa = null;

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
            selectedSatsPanel.GetComponent<Window>().initialize(BattleUIController.HighestWindow, cursorCell.occupier);
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

        tryCast();
    }

    public void killCharacter(BattleCharacterObject character)
    {
        characters.Remove(character);
        BEL.addEvent(character.getName(), "Kill");
        Destroy(character.characterObject, 2f);
        character.getOccupying().occupier = null;
        if(character == activeCharacter) {  activeCharacter = null; }   
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



    //###Abstracts###
    public virtual List<TacticalAbility> getTacticalAbilities()
    {
        return baseTacticalAbilities;
    }

    public virtual void interact(int index)
    {

    }

    public abstract void loadCharacters();

    public abstract void endBattle();

    public abstract void objectiveComplete(string id);

    public abstract void GBCTexecutions(int index);
}