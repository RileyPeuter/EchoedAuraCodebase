using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RulessDeogBattleController : BattleController
{
    GameObject kahundPrefab;
    BattleCharacterObject cart;
    BattleCharacterObject shrub;

    public override void endBattle()
    {
        
        BattleCharacterObject BCO = getCharacterFromName("Ruless");

        if (BCO != null)
        {
            GlobalGameController.GGC.getAgency().addCharacter(BCO.getCharacter());
        }
        GlobalGameController.GGC.completeMission();
        GlobalGameController.GGC.openManagment();
    }

    public override void GBCTexecutions(int index)
    {
        switch (index)
        {
            case 1:
                openEndWindow();
                break;

        }
    }

    public override void interact(int index)
    {
        shrub.heal(10);
    }

    public override void loadCharacters()
    {



        spawnCells.Add(map.gridObject.gridCells[4, 4]);
        spawnCells.Add(map.gridObject.gridCells[5, 4]);
        spawnCells.Add(map.gridObject.gridCells[7, 4]);
        spawnCells.Add(map.gridObject.gridCells[6, 4]);

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

        //Please please please redo this some time, atleast take away the list refernces
        characters.Add(GameObject.Instantiate(Resources.Load<GameObject>("TestAssets/TradeCart")).GetComponent<BattleCharacterObject>());
        characters[characters.Count - 1].initialize(8, 3, new TradeCart(), CharacterAllegiance.Allied);
        characters[characters.Count - 1].spawnCharacter(map.gridObject);
        cart = characters[characters.Count -1];
        cart.makeDormant();

        characters.Add(GameObject.Instantiate(Resources.Load<GameObject>("TestAssets/Ruless")).GetComponent<BattleCharacterObject>());
        characters[characters.Count - 1].initialize(7, 3, new Ruless(), CharacterAllegiance.Controlled);
        characters[characters.Count - 1].spawnCharacter(map.gridObject);

        characters.Add(GameObject.Instantiate(Resources.Load<GameObject>("TestAssets/Shrub")).GetComponent<BattleCharacterObject>());
        characters[characters.Count - 1].initialize(9, 7, new Shrubs(), CharacterAllegiance.Allied);
        characters[characters.Count - 1].spawnCharacter(map.gridObject);
        shrub = characters[characters.Count - 1];
        characters[characters.Count - 1].makeDormant();
    }

    public override void objectiveComplete(string id)
    {
        switch (id)
        {
            case "etKahund0":

                spawnKahund();
                objList.addObjective(new Objective("etKahund0", 1, BattleEventType.EndTurn).addVerb(CharacterAllegiance.Controlled.ToString()));
                break;

            case "etFinish":
                activeCutscene = gameObject.AddComponent<GenericBattleCutscene>();
                activeCutscene.initialize(this);

                activeCutscene.addSprite("Fray", "Fray");
                activeCutscene.addSprite("Ruless", "Ruless");
                activeCutscene.addSprite("Iraden", "Iraden");


                activeCutscene.addFrame("Fray", "We're getting nowhere.", "Fray");
                activeCutscene.addFrame("Ruless", "I can keep this up. We musn't abbandon our duty.", "Ruless");
                activeCutscene.addFrame("Fray", "Normally i'd agree, but your duty was to escort people who ran away hours ago", "Fray");
                activeCutscene.addFrame("Fray", "I don't want to have to explain to our seniors how you died a some pests", "Fray");
                activeCutscene.addFrame("Iraden", "Maybe we can break through the trees. They'll leave us alone for the cart", "Iraden");
                activeCutscene.addFrame("Ruless", "...Fine. For your sakes", "Ruless", 1);

                activeCutscene.createDialogueObject();
                activeCutscene.preInitializeTextbox();
                activeCutscene.setFrame(0);
                toggleCutscene(true);

                break;
        }
    }   

    void spawnKahund(bool extra = false)
    {
        if (Random.Range(0, 2) != 1) {
            spawnCharacter(Instantiate(kahundPrefab), CharacterAllegiance.Enemey, map.gridObject.getClosestAvailableCell(map.gridObject.gridCells[0, 9]), new Kahund(cart.getOccupying()));
        }
        else
        {
            spawnCharacter(Instantiate(kahundPrefab), CharacterAllegiance.Enemey, map.gridObject.getClosestAvailableCell(map.gridObject.gridCells[19, 9]), new Kahund(cart.getOccupying()));
        }
        if (extra)
        {
            spawnCharacter(Instantiate(kahundPrefab), CharacterAllegiance.Enemey, map.gridObject.getClosestAvailableCell(map.gridObject.gridCells[9, 8]), new Kahund(cart.getOccupying()));
            spawnCharacter(Instantiate(kahundPrefab), CharacterAllegiance.Enemey, map.gridObject.getClosestAvailableCell(map.gridObject.gridCells[9, 8]), new Kahund(cart.getOccupying()));
            spawnCharacter(Instantiate(kahundPrefab), CharacterAllegiance.Enemey, map.gridObject.getClosestAvailableCell(map.gridObject.gridCells[9, 8]), new Kahund(cart.getOccupying()));

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


        baseTacticalAbilities.Add(new Supposititious(this));
        objList.addObjective(new Objective("etKahund0", 1, BattleEventType.EndTurn).addSubject("Ruless").makeInvisible());
        objList.addObjective(new Objective("etFinish", 1, BattleEventType.Time).addDescription("Hold out (till T:50)").addModifier(ObjectiveModifier.GreaterThan, 50));
        objList.addObjective(new Objective("defCart0", 1, BattleEventType.Time).addDescription("Hold out (till T:50)").addModifier(ObjectiveModifier.GreaterThan, 50));

        lookForBattleEventListeners();
        spawnKahund(true);
    }

    public override List<TacticalAbility> getTacticalAbilities()
    {
        List<TacticalAbility> output =  new List<TacticalAbility>();
        output.AddRange(baseTacticalAbilities);
        if(ExWhy.getDistanceBetweenCells(getActiveCharacter().getOccupying(), shrub.getOccupying()) < 2)
        {
            output.Add(new Interact(this, 1));
        }

        return output;
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
