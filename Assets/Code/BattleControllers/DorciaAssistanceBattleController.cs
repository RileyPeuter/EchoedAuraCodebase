using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DorciaAssistanceBattleController : BattleController
{

    BattleCharacterObject dorciaObject;
    BattleCharacterObject gate;
    public override void endBattle()
    {
        
    }

    public override void GBCTexecutions(int index)
    {

    }

    public override void loadCharacters()
    {

        spawnCells.Add(map.gridObject.gridCells[6, 4]);
        spawnCells.Add(map.gridObject.gridCells[5, 4]);
        spawnCells.Add(map.gridObject.gridCells[7, 4]);

        int spawnIndex = 0;
        foreach (GameObject GO in getCharacterGOsFromStored())
        {
            GO.GetComponent<BattleCharacterObject>().setSpawnCords(spawnCells[spawnIndex].xPosition, spawnCells[spawnIndex].yPosition);
            characters.Add(GO.GetComponent<BattleCharacterObject>());

            characters[spawnIndex].spawnCharacter(map.gridObject);
            spawnIndex++;
        }

        characters.Add(GameObject.Instantiate(Resources.Load<GameObject>("TestAssets/Gate")).GetComponent<BattleCharacterObject>());
        characters[characters.Count - 1].initialize(16, 6, new Gate(), CharacterAllegiance.Neutral);
        characters[characters.Count - 1].spawnCharacter(map.gridObject);
        characters[characters.Count - 1].makeDormant();
        gate = characters[characters.Count - 1];


        characters.Add(GameObject.Instantiate(Resources.Load<GameObject>("TestAssets/Dorcia")).GetComponent<BattleCharacterObject>());
        characters[characters.Count - 1].initialize(6, 6, new Dorcia(), CharacterAllegiance.Allied);
        characters[characters.Count - 1].spawnCharacter(map.gridObject);
        characters[characters.Count - 1].makeDormant();
        dorciaObject = characters[characters.Count - 1];

    }

    public override void objectiveComplete(string id)
    {

    }

    public override List<TacticalAbility> getTacticalAbilities()
    {
        List <TacticalAbility> output = new List<TacticalAbility>();
        output.AddRange(baseTacticalAbilities);


        print(ExWhy.getDistanceBetweenCells(getActiveCharacter().getOccupying(), dorciaObject.getOccupying()));
        print(getActiveCharacter().getOccupying().yPosition);
        print(getActiveCharacter().getOccupying().xPosition);

        if (ExWhy.getDistanceBetweenCells(getActiveCharacter().getOccupying(),dorciaObject.getOccupying()) == 1)
        {
            output.Add(new Interact(this, 1, "Push Docria"));
            print("kappa");
        }

        if (getActiveCharacter().getOccupying().xPosition == 17 && getActiveCharacter().getOccupying().yPosition == 6)
        {
            output.Add(new Interact(this, 2, "Unbar the gate"));
        }

        return output;
    }

    public override void interact(int index)
    {
        switch (index)
        {

            case 1:
                if (map.gridObject.gridCells[dorciaObject.getOccupying().xPosition + 1, dorciaObject.getOccupying().yPosition].occupier == null)
                {
                    dorciaObject.move(map.gridObject.gridCells[dorciaObject.getOccupying().xPosition + 1, dorciaObject.getOccupying().yPosition]);
                }
            break;

            case 2:
                killCharacter(gate);
            break;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        spawnCells = new List<ExWhyCell>();

        initializeBattleController();

        cursor.transform.position.Set(3, 3, 1);
        cursorCell = map.gridObject.gridCells[3, 3];
        initializeGameState();

        initializeAdditionalElements();

        lookForBattleEventListeners();

        //objList.addObjective(new Object("Dorcia "))
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
