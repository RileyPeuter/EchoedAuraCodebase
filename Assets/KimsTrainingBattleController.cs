    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KimsTrainingBattleController : BattleController
{
    public override void endBattle()
    {

    }

    public override void GBCTexecutions(int index)
    {

    }

    public override void loadCharacters()
    {

        spawnCells.Add(map.gridObject.gridCells[3, 4]);
        spawnCells.Add(map.gridObject.gridCells[3, 5]);
        spawnCells.Add(map.gridObject.gridCells[3, 6]);

        int spawnIndex = 0;
        foreach (GameObject GO in getCharacterGOsFromStored())
        {
            GO.GetComponent<BattleCharacterObject>().setSpawnCords(spawnCells[spawnIndex].xPosition, spawnCells[spawnIndex].yPosition);
            characters.Add(GO.GetComponent<BattleCharacterObject>());

            characters[spawnIndex].spawnCharacter(map.gridObject);
            spawnIndex++;
        }
        foreach (BattleCharacterObject bco in characters)
        {
            print(bco.getName());
        }

        characters.Add(GameObject.Instantiate(Resources.Load<GameObject>("TestAssets/Kim")).GetComponent<BattleCharacterObject>());
        characters[characters.Count - 1].initialize(2, 3, new Kim(), CharacterAllegiance.Controlled);
        characters[characters.Count - 1].spawnCharacter(map.gridObject);
    }

    public override void objectiveComplete(string id)
    {

    }

    private void Start()
    {
        spawnCells = new List<ExWhyCell>();

        initializeBattleController();

        cursor.transform.position.Set(3, 3, 1);
        cursorCell = map.gridObject.gridCells[3, 3];
        initializeGameState();


        initializeAdditionalElements();
    }

    // Update is called once per frame
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
