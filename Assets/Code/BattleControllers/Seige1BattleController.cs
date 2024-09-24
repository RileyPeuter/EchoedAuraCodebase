using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Siege1BattleController : BattleController
{
    public override void endBattle()
    {

    }

    public override void GBCTexecutions(int index)
    {
    
    }

    public override void loadCharacters()
    {
    
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
    }

    void Update()
    {
        
    }
}
