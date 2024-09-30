using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Siege1BattleController : BattleController
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
        
        fodderGuardPrefab = ResourceLoader.loadGameObject("TestAssets/FodderGuard");
        archerPrefab = ResourceLoader.loadGameObject("TestAssets/Archer");

        spawnCells.Add(map.gridObject.gridCells[0, 8]);
        spawnCells.Add(map.gridObject.gridCells[0, 7]);
        spawnCells.Add(map.gridObject.gridCells[0, 6]);
        spawnCells.Add(map.gridObject.gridCells[0, 5]);

        
        int spawnIndex = 0;

        foreach (GameObject GO in getCharacterGOsFromStored())
        {
            GO.GetComponent<BattleCharacterObject>().setSpawnCords(spawnCells[spawnIndex].xPosition, spawnCells[spawnIndex].yPosition);
            characters.Add(GO.GetComponent<BattleCharacterObject>());

            characters[spawnIndex].spawnCharacter(map.gridObject);
            spawnIndex++;
        }
        spawnCharacter(Instantiate(fodderGuardPrefab), CharacterAllegiance.Enemey, map.gridObject.gridCells[8, 5], new FodderGuard());
        spawnCharacter(Instantiate(fodderGuardPrefab), CharacterAllegiance.Enemey, map.gridObject.gridCells[9, 5], new FodderGuard());
        spawnCharacter(Instantiate(fodderGuardPrefab), CharacterAllegiance.Enemey, map.gridObject.gridCells[10, 5], new FodderGuard());
        spawnCharacter(Instantiate(fodderGuardPrefab), CharacterAllegiance.Enemey, map.gridObject.gridCells[11, 5], new FodderGuard());
        spawnCharacter(Instantiate(fodderGuardPrefab), CharacterAllegiance.Enemey, map.gridObject.gridCells[7, 8], new FodderGuard());
        spawnCharacter(Instantiate(fodderGuardPrefab), CharacterAllegiance.Enemey, map.gridObject.gridCells[8, 8], new FodderGuard());
        spawnCharacter(Instantiate(fodderGuardPrefab), CharacterAllegiance.Enemey, map.gridObject.gridCells[9, 8], new FodderGuard());
        spawnCharacter(Instantiate(fodderGuardPrefab), CharacterAllegiance.Enemey, map.gridObject.gridCells[10, 8], new FodderGuard());

        spawnCharacter(Instantiate(archerPrefab), CharacterAllegiance.Enemey, map.gridObject.gridCells[7, 7], new Archer());

    }

    public override void objectiveComplete(string id)
    {
    
    }

    void Start()
    {
        spawnCells = new List<ExWhyCell>();

        initializeBattleController();

        cursor.transform.position.Set(3, 3, 1);
        cursorCell = map.gridObject.gridCells[3, 3];
        initializeGameState();

        initializeAdditionalElements();

        lookForBattleEventListeners();

        spawnDecorations(new Vector2(52, 22), "Rope");

        addObjectiveHighlight(14, 2);
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
