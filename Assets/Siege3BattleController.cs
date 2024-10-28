using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Siege3BattleController : BattleController
{

    GameObject fodderGuardPrefab;
    GameObject archerPrefab;

    public override void endBattle()
    {

    }

    public override void GBCTexecutions(int index)
    {

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

    public override void objectiveComplete(string id)
    {

    }

    public void spawnPack()
    {
        spawnCharacter(Instantiate(fodderGuardPrefab), CharacterAllegiance.Enemey, map.gridObject.gridCells[8, 5], new FodderGuard());
        spawnCharacter(Instantiate(archerPrefab), CharacterAllegiance.Enemey, map.gridObject.gridCells[7, 7], new Archer());
    }

    void Start()
    {

        fodderGuardPrefab = ResourceLoader.loadGameObject("TestAssets/FodderGuard");
        archerPrefab = ResourceLoader.loadGameObject("TestAssets/Archer");

        spawnCells = new List<ExWhyCell>();

        initializeBattleController();

        cursor.transform.position.Set(3, 3, 1);
        cursorCell = map.gridObject.gridCells[3, 3];

        initializeGameState();

        initializeAdditionalElements();



        lookForBattleEventListeners();


        spawnDecorations(new Vector2(0, 0), "NightFG");

        objList.addObjective(new Objective("surv0", 1, BattleEventType.Interact).addDescription("Extract all agents"));

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
