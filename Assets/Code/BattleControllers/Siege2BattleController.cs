using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Siege2BattleController : BattleController
{

    GameObject fodderGuardPrefab;
    GameObject archerPrefab;
    GameObject samuraiPrefab;
    
    int surveys = 0;

    public void spawnPack()
    {
        spawnCharacter(Instantiate(fodderGuardPrefab), CharacterAllegiance.Enemey, map.gridObject.gridCells[8, 5], new FodderGuard());
        spawnCharacter(Instantiate(archerPrefab), CharacterAllegiance.Enemey, map.gridObject.gridCells[7, 7], new Archer());
    }

    public override void endBattle()
    {
        GlobalGameController.GGC.mapToBeLoaded = new ExWhySiege3();

        GlobalGameController.GGC.BCToBeLoaded = 8;
        GlobalGameController.GGC.CutsceneToBeLoaded = 6;

        GlobalGameController.GGC.startCutscene();
    }

    public override void GBCTexecutions(int index)
    {
        switch (index)
        {
            case 1:
                activeCutscene.phaseCutscene(2f);


                ExWhyCell cell2 = map.gridObject.getClosestAvailableCell(12, 15);
                forceCast(characters[characters.Count - 1], cell2, characters[characters.Count - 1].getMovementAbility());
                break;
        }
    }

    public override void interact(int index)
    {
        base.interact(index);
        switch (index)
        {
            case 1:
                exileCharacter(getActiveCharacter());
                break;
        }
    }

    public override List<TacticalAbility> getTacticalAbilities()
    {
     
        //I feel like this should be wrong, but I guess it's fine?

        List<TacticalAbility> output;

        output = base.getTacticalAbilities();

        if (getActiveCharacter().getOccupying().xPosition == 4 && getActiveCharacter().getOccupying().yPosition == 6)
        {
            Interact Interact = new Interact(this, 4, "Survery the oncomoing forces");
            output.Add(Interact);
        }
        
        if (getActiveCharacter().getOccupying().xPosition == 4 && getActiveCharacter().getOccupying().yPosition == 9)
        {
            Interact nInteract = new Interact(this, 5, "Survery the oncomoing forces");
            output.Add(nInteract);
        }

        if(surveys >=  2 && getActiveCharacter().getOccupying().xPosition == 12)
        {
            output.Add(new Extract(this));
        }


        return output;
    }


    public override void objectiveComplete(string id)
    {
        switch (id)
        {
            case "surv0":
                finishSurverCheck();
                break;


            case "surv1":
                ExWhyCell cell = map.gridObject.getClosestAvailableCell(5, 9);

                spawnCharacter(Instantiate(samuraiPrefab), CharacterAllegiance.Enemey, cell , new Samurai());
                finishSurverCheck();

                forceCast(characters[characters.Count - 1], cell, characters[characters.Count - 1].getMovementAbility());

                activeCutscene = gameObject.AddComponent<GenericBattleCutscene>();
                activeCutscene.initialize(this);
                activeCutscene.addSprite(getActiveCharacter().getCharacter().getResourceString(), getActiveCharacter().getCharacter().getResourceString());
                activeCutscene.addSprite("Samurai", "Samurai");

                activeCutscene.addFrame(getActiveCharacter().getCharacter().getResourceString(), "What!", getActiveCharacter().getCharacter().getResourceString());
                activeCutscene.addFrame("???", "Children in a place like this?", "Samurai");
                activeCutscene.addFrame("???", "This won't be my proudest kill, but none the less...", "Samurai");
                activeCutscene.createDialogueObject();
                activeCutscene.preInitializeTextbox();
                activeCutscene.setFrame(0);
                toggleCutscene(true);
                activeCutscene.phaseCutscene(2f);

                objList.addObjective(new VagueObjective("hitSam0", 1, BattleEventType.Hit).addDescription("Eliminate the Foreign Soldier").addTarget("Foreign Swordsman"));

                break;

            case "hitSam0":
                activeCutscene = gameObject.AddComponent<GenericBattleCutscene>();
                activeCutscene.initialize(this);
                activeCutscene.addSprite("Samurai", "Samurai");
                activeCutscene.addFrame("???", "HAA!", "Samurai");
                activeCutscene.addFrame("???", "Nice moves", "Samurai", 1);
                activeCutscene.addFrame("???", "It might even be time for me to withdraw", "Samurai");
                activeCutscene.createDialogueObject();
                activeCutscene.preInitializeTextbox();
                activeCutscene.setFrame(0);
                toggleCutscene(true);


                break;

            case "ext0":
                if (!checkExtraction())
                {
                    objList.removeObjective("ext0");
                    objList.addObjective(new Objective("ext0", 1, BattleEventType.Attack).addDescription("Withdraw from the fight and deliver your report").addVerb("Extraction"));
                }
                break;

        }
    }

    void finishSurverCheck()
    {
        surveys++;

        if(surveys > 1)
        {
            objList.addObjective(new Objective("ext0", 1, BattleEventType.Attack).addDescription("Withdraw from the fight and deliver your report").addVerb("Extraction"));
            clearObjectiveHighlights();
            addObjectiveHighlight(12, 1);
            addObjectiveHighlight(12, 2);
            addObjectiveHighlight(12, 3);

        }
    }

    public override void loadCharacters()
    {
        spawnCells.Add(map.gridObject.gridCells[12, 2]);
        spawnCells.Add(map.gridObject.gridCells[11, 2]);
        spawnCells.Add(map.gridObject.gridCells[12, 3]);
        spawnCells.Add(map.gridObject.gridCells[11, 3]);



        int spawnIndex = 0;

        foreach (GameObject GO in getCharacterGOsFromStored())
        {
            GO.GetComponent<BattleCharacterObject>().setSpawnCords(spawnCells[spawnIndex].xPosition, spawnCells[spawnIndex].yPosition);
            characters.Add(GO.GetComponent<BattleCharacterObject>());

            characters[spawnIndex].spawnCharacter(map.gridObject);
            spawnIndex++;


        }

        spawnCharacter(Instantiate(fodderGuardPrefab), CharacterAllegiance.Enemey, map.gridObject.gridCells[5, 5], new FodderGuard());
        spawnCharacter(Instantiate(archerPrefab), CharacterAllegiance.Enemey, map.gridObject.gridCells[6, 9], new Archer());


        spawnCharacter(Instantiate(fodderGuardPrefab), CharacterAllegiance.Enemey, map.gridObject.gridCells[6, 14], new FodderGuard(true));
        spawnCharacter(Instantiate(archerPrefab), CharacterAllegiance.Enemey, map.gridObject.gridCells[7, 14], new Archer(true));
    }

    void Start()
    {
        spawnCells = new List<ExWhyCell>();

        fodderGuardPrefab = ResourceLoader.loadGameObject("TestAssets/FodderGuard");
        archerPrefab = ResourceLoader.loadGameObject("TestAssets/Archer");
        samuraiPrefab = ResourceLoader.loadGameObject("TestAssets/Samurai");
        initializeBattleController();

        cursor.transform.position.Set(3, 3, 1);
        cursorCell = map.gridObject.gridCells[3, 3];

        initializeGameState();

        initializeAdditionalElements();



        lookForBattleEventListeners();


        spawnDecorations(new Vector2(0, 0), "NightFG");

        objList.addObjective(new Objective("surv0", 1, BattleEventType.Interact).addDescription("Survey south side of the battle.").addVerb("4"));

        objList.addObjective(new Objective("surv1", 1, BattleEventType.Interact).addDescription("Survey noth side of the battle.").addVerb("5"));
    
        activeCutscene = gameObject.AddComponent<GenericBattleCutscene>();
        activeCutscene.initialize(this);
        activeCutscene.addSprite(getActiveCharacter().getCharacter().getResourceString(), getActiveCharacter().getCharacter().getResourceString());
        activeCutscene.addFrame(getActiveCharacter().getCharacter().getResourceString(), "We can get a view of the incoming forces on those \nscaffoldings", getActiveCharacter().getCharacter().getResourceString());
        activeCutscene.addFrame(getActiveCharacter().getCharacter().getResourceString(), "They look pretty rickety though...", getActiveCharacter().getCharacter().getResourceString());
        activeCutscene.createDialogueObject();
        activeCutscene.preInitializeTextbox();
        activeCutscene.setFrame(0);
        toggleCutscene(true);
        activeCutscene.phaseCutscene(1f);

        addObjectiveHighlight(4, 6);
        addObjectiveHighlight(4, 9);

    }

    void Update()
    {
        controls();

        if (getActiveCharacter() != null)
        {
            if (getActiveCharacter().GetAllegiance() != CharacterAllegiance.Controlled)
            {
                ticker();
            }
        }
    }
}
