using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DorciaAssistanceBattleController : BattleController
{

    GameObject kahundPrefab;
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

        spawnCells.Add(map.gridObject.gridCells[6, 3]);
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

    public void spawnKahund(bool extra = false)
    {
        if (extra)
        {
            spawnCharacter(Instantiate(kahundPrefab), CharacterAllegiance.Enemey, map.gridObject.getClosestAvailableCell(map.gridObject.gridCells[19, 2]), new Kahund());
        }
        spawnCharacter(Instantiate(kahundPrefab), CharacterAllegiance.Enemey, map.gridObject.getClosestAvailableCell(map.gridObject.gridCells[1, 7]), new Kahund());
    }

    public override void objectiveComplete(string id)
    {
        switch (id)
        {
            case "khFSpawn01":
                spawnKahund(true);
                break;

            case "gtCts01":
                activeCutscene = gameObject.AddComponent<GenericBattleCutscene>();
                activeCutscene.initialize(this);

                activeCutscene.addSprite("Fray", "Fray");
                activeCutscene.addSprite("Morthred", "Morthred");
                activeCutscene.addSprite("Iraden", "Iraden");

                activeCutscene.addFrame("Fray", "Gate up ahead. ", "Fray");
                activeCutscene.addFrame("Morthred", "It looks flimsy. We could just break it down", "Morthred");
                activeCutscene.addFrame("Fray", "Not flimsy enough. If someone gets behind it, they could unbar it", "Fray");

                activeCutscene.createDialogueObject();
                activeCutscene.preInitializeTextbox();
                activeCutscene.setFrame(0);
                toggleCutscene(true);

                break;

            case "pshDorcia01":
                activeCutscene = gameObject.AddComponent<GenericBattleCutscene>();
                activeCutscene.initialize(this);

                activeCutscene.addSprite("Fray", "Fray");
                activeCutscene.addSprite("Dorcia", "Dorcia");
                activeCutscene.addSprite("Iraden", "Iraden");

                activeCutscene.addFrame("Fray", "Gate up ahead. ", "Fray");
                activeCutscene.addFrame("Morthred", "It looks flimsy. We could just break it down", "Morthred");
                activeCutscene.addFrame("Fray", "Not flimsy enough. If someone gets behind it, they could unbar it", "Fray");

                activeCutscene.createDialogueObject();
                activeCutscene.preInitializeTextbox();
                activeCutscene.setFrame(0);
                toggleCutscene(true);

                break;

            case "khSpawn01":
                spawnKahund(false);
                objList.addObjective(new Objective("khSpawn01", 1, BattleEventType.Time).addModifier(ObjectiveModifier.GreaterThan, turnTimer + 10));
                break;
        }
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

        List<ExWhyCell> KahundSpawnField = new List<ExWhyCell>();

        KahundSpawnField.Add(map.gridObject.gridCells[15, 1]);
        KahundSpawnField.Add(map.gridObject.gridCells[16, 1]);
        KahundSpawnField.Add(map.gridObject.gridCells[17, 1]);
        KahundSpawnField.Add(map.gridObject.gridCells[18, 1]);
        KahundSpawnField.Add(map.gridObject.gridCells[15, 2]);

        List<ExWhyCell> gateCutsceneField = new List<ExWhyCell>();

        gateCutsceneField.Add(map.gridObject.gridCells[9, 6]);
        gateCutsceneField.Add(map.gridObject.gridCells[10, 6]);
        gateCutsceneField.Add(map.gridObject.gridCells[11, 6]);

        objList.addObjective(new MovementObjective("gtCts01", gateCutsceneField, 1));

        kahundPrefab = Resources.Load<GameObject>("TestAssets/Kahund");

        objList.addObjective(new Objective("khSpawn01", 1, BattleEventType.Time).addModifier(ObjectiveModifier.GreaterThan, 10));
        spawnCharacter(Instantiate(kahundPrefab), CharacterAllegiance.Enemey, map.gridObject.getClosestAvailableCell(map.gridObject.gridCells[1, 7]), new Kahund());
        spawnCharacter(Instantiate(kahundPrefab), CharacterAllegiance.Enemey, map.gridObject.getClosestAvailableCell(map.gridObject.gridCells[1, 7]), new Kahund());

        objList.addObjective(new MovementObjective("khFSpawn01", KahundSpawnField, 1).addDescription("Teststests"));

        objList.addObjective(new Objective("pshDorcia01", 1, BattleEventType.Movement).addDescription("Push Dorcia out of Danger"));
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
