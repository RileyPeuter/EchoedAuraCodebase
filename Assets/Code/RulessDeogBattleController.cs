using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RulessDeogBattleController : BattleController
{
    GameObject kahundPrefab;
    BattleCharacterObject cart;

    public override void endBattle()
    {

    }

    public override void GBCTexecutions(int index)
    {

    }

    public override void loadCharacters()
    {

        spawnCells.Add(map.gridObject.gridCells[4, 4]);
        spawnCells.Add(map.gridObject.gridCells[5, 4]);
        spawnCells.Add(map.gridObject.gridCells[7, 4]);

        int spawnIndex = 0;
        foreach(GameObject GO in getCharacterGOsFromStored())
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


        characters.Add(GameObject.Instantiate(Resources.Load<GameObject>("TestAssets/TradeCart")).GetComponent<BattleCharacterObject>());
        characters[characters.Count - 1].initialize(8, 3, new TradeCart(), CharacterAllegiance.Allied);
        characters[characters.Count - 1].spawnCharacter(map.gridObject);
        cart = characters[characters.Count -1];
        cart.makeDormant();

        characters.Add(GameObject.Instantiate(Resources.Load<GameObject>("TestAssets/Ruless")).GetComponent<BattleCharacterObject>());
        characters[characters.Count - 1].initialize(7, 3, new Ruless(), CharacterAllegiance.Controlled);
        characters[characters.Count - 1].spawnCharacter(map.gridObject);
    }

    public override void objectiveComplete(string id)
    {
        switch (id)
        {
            case "etKahund0":
                spawnKahund();
                objList.addObjective(new ObjectiveListElement("etKahund0", 1, ObjectiveType.Time).addEventType("End Turn").addVerb(CharacterAllegiance.Controlled.ToString()));
                break;

        }
    }

    void spawnKahund()
    {
        if (Random.Range(0, 2) != 1) { 
            spawnCharacter(Instantiate(kahundPrefab), CharacterAllegiance.Enemey, map.gridObject.getClosestAvailableCell(map.gridObject.gridCells[0, 9]), new Kahund(cart.getOccupying()));
        }
        else
        {
            spawnCharacter(Instantiate(kahundPrefab), CharacterAllegiance.Enemey, map.gridObject.getClosestAvailableCell(map.gridObject.gridCells[0, 9]), new Kahund(cart.getOccupying()));
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        kahundPrefab = Resources.Load<GameObject>("TestAssets/Kahund");
        spawnCells = new List<ExWhyCell>();

        initializeBattleController();

        cursor.transform.position.Set(3, 3, 1);
        cursorCell = map.gridObject.gridCells[3, 3];
        initializeGameState();

        //putCutsceneHere

        initializeAdditionalElements();

        objList.addObjective(new ObjectiveListElement("etKahund0", 1, ObjectiveType.Time).addEventType("End Turn").addVerb(CharacterAllegiance.Controlled.ToString()).addDescription("You shouldn't be able to see this"));
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
