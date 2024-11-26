using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Siege3BattleController : BattleController
{

    GameObject fodderGuardPrefab;
    GameObject archerPrefab;
    GameObject samuraiPrefab;
    GameObject shrubPrefab;
    GameObject kahundPrefab;

    public override void endBattle()
    {

    }

    public override void GBCTexecutions(int index)
    {

    }

    public override void loadCharacters()
    {
        spawnCells.Add(map.gridObject.gridCells[8, 23]);
        spawnCells.Add(map.gridObject.gridCells[11, 23]);
        spawnCells.Add(map.gridObject.gridCells[12, 23]);
        spawnCells.Add(map.gridObject.gridCells[9, 23]);


        int spawnIndex = 0;

        foreach (GameObject GO in getCharacterGOsFromStored())
        {
            GO.GetComponent<BattleCharacterObject>().setSpawnCords(spawnCells[spawnIndex].xPosition, spawnCells[spawnIndex].yPosition);
            characters.Add(GO.GetComponent<BattleCharacterObject>());

            characters[spawnIndex].spawnCharacter(map.gridObject);
            spawnIndex++;
        }


        spawnCharacter(Instantiate(kahundPrefab), CharacterAllegiance.Enemey, map.gridObject.getClosestAvailableCell(map.gridObject.gridCells[12, 18]), new Kahund());
        spawnCharacter(Instantiate(fodderGuardPrefab), CharacterAllegiance.Enemey, map.gridObject.getClosestAvailableCell(map.gridObject.gridCells[0, 14]), new FodderGuard());
        spawnCharacter(Instantiate(fodderGuardPrefab), CharacterAllegiance.Enemey, map.gridObject.getClosestAvailableCell(map.gridObject.gridCells[0, 17]), new FodderGuard());
        spawnCharacter(Instantiate(shrubPrefab), CharacterAllegiance.Enemey, map.gridObject.getClosestAvailableCell(map.gridObject.gridCells[3, 9]), new Shrubs());
        spawnCharacter(Instantiate(shrubPrefab), CharacterAllegiance.Enemey, map.gridObject.getClosestAvailableCell(map.gridObject.gridCells[10, 11]), new Shrubs());
        spawnCharacter(Instantiate(archerPrefab), CharacterAllegiance.Enemey, map.gridObject.getClosestAvailableCell(map.gridObject.gridCells[11, 14]), new Archer());



    }

    public override void objectiveComplete(string id)
    {
        switch (id)
        {

            case "ext0":
                if(!checkExtraction()){
                    objList.removeObjective("ext0");
                    objList.addObjective(new Objective("ext0", 1, BattleEventType.Attack).addDescription("Escape through the forest").addVerb("Extraction"));
                }
            break;
        }

    }

    public void spawnPack()
    {
        spawnCharacter(Instantiate(fodderGuardPrefab), CharacterAllegiance.Enemey, map.gridObject.gridCells[5, 23], new FodderGuard());
        spawnCharacter(Instantiate(archerPrefab), CharacterAllegiance.Enemey, map.gridObject.gridCells[6, 23], new Archer());
    }

    void Start()
    {

        fodderGuardPrefab = ResourceLoader.loadGameObject("TestAssets/FodderGuard");
        archerPrefab = ResourceLoader.loadGameObject("TestAssets/Archer");
        samuraiPrefab = ResourceLoader.loadGameObject("TestAssets/Samurai");
        kahundPrefab = ResourceLoader.loadGameObject("TestAssets/Kahund");
        shrubPrefab= ResourceLoader.loadGameObject("TestAssets/Shrub");


        spawnCells = new List<ExWhyCell>();

        initializeBattleController();

        cursor.transform.position.Set(3, 3, 1);
        cursorCell = map.gridObject.gridCells[3, 3];

        initializeGameState();

        initializeAdditionalElements();



        lookForBattleEventListeners();


        spawnDecorations(new Vector2(0, 0), "NightFG");
        spawnDecorations(new Vector2(32, 96.56f), "Doorway");

        objList.addObjective(new Objective("ext0", 1, BattleEventType.Attack).addDescription("Withdraw from the fight and deliver your report").addVerb("Extraction"));

        addObjectiveHighlight(7, 1);

        activeCutscene = gameObject.AddComponent<GenericBattleCutscene>();
        activeCutscene.initialize(this);

        activeCutscene.loadSpritesFromStoredCharacters();

        if (activeCutscene.containsSprite("Morthred"))
        {
            activeCutscene.addFrame("Morthred", "Lets maneuver through the trees, then we can find a way back home.", "Morthred");
            activeCutscene.addFrame("Morthred", "Quickly. These extreme tactics get quite ugly", "Morthred");

            if (activeCutscene.containsSprite("Fray"))
            {
                activeCutscene.addFrame("Fray", "Yeah, quickly.", "Fray");
                activeCutscene.addFrame("Fray", "I don't really have the stomach for these things that you do.", "Fray");
            }
        }

        else if (activeCutscene.containsSprite("Fray"))
        {
            activeCutscene.addFrame("Fray", "Lets head south through the forest.\nThen head back west after we're clear of this place", "Fray");
            activeCutscene.addFrame("Fray", "Everything about this place...", "Fray");
        }

        else if (activeCutscene.containsSprite("Iraden"))
        {
            activeCutscene.addFrame("Iraden", "The thing's Leigh's doing...", "Iraden");
            activeCutscene.addFrame("Iraden", "Lets get out of here", "Iraden");
        }

        if (activeCutscene.containsSprite("Kim"))
        {
            activeCutscene.addFrame("Kim", "Leigh...", "Kim");
            activeCutscene.addFrame("Kim", "He takes after the Hatchlings...", "Kim");
            if (activeCutscene.containsSprite("Iraden"))
            {
                activeCutscene.addFrame("Iraden", "Who...", "Iraden");
                if (activeCutscene.containsSprite("Fray"))
                {
                    activeCutscene.addFrame("Fray", "I'll explain later", "Fray");
                }
            }
        }

        activeCutscene.createDialogueObject();
        activeCutscene.preInitializeTextbox();
        activeCutscene.setFrame(0);
        toggleCutscene(true);

    }

    public override List<TacticalAbility> getTacticalAbilities()
    {
        List<TacticalAbility> output;

        output = base.getTacticalAbilities();

        if(getActiveCharacter().getOccupying().yPosition == 1)
        {
            output.Add(new Extract(this));
        }

        return output;
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
