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

    }

    public override void GBCTexecutions(int index)
    {

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
        List<TacticalAbility> output = new List<TacticalAbility>();

        output = base.getTacticalAbilities();

        if (getActiveCharacter().getOccupying().xPosition == 14 && getActiveCharacter().getOccupying().yPosition == 2)
        {
            Interact nInteract = new Interact(this, 1, "Survery the oncomoing forces");
            output.Add(nInteract);
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
//                spawnCharacter()
                finishSurverCheck();
                break;

            case "ext0":
                checkExtraction();
                break;

        }
    }

    void finishSurverCheck()
    {
        surveys++;

        if(surveys > 1)
        {

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
    }

    void Start()
    {
        spawnCells = new List<ExWhyCell>();

        fodderGuardPrefab = ResourceLoader.loadGameObject("TestAssets/FodderGuard");
        archerPrefab = ResourceLoader.loadGameObject("TestAssets/Archer");

        initializeBattleController();

        cursor.transform.position.Set(3, 3, 1);
        cursorCell = map.gridObject.gridCells[3, 3];

        initializeGameState();

        initializeAdditionalElements();



        lookForBattleEventListeners();


        spawnDecorations(new Vector2(0, 0), "NightFG");

        objList.addObjective(new Objective("surv0", 1, BattleEventType.Interact).addDescription("Survey south side of the battle."));

        objList.addObjective(new Objective("surv1", 1, BattleEventType.Interact).addDescription("Survey noth side of the battle."));
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
