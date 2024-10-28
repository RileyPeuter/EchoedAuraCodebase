using JetBrains.Annotations;
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
            Interact nInteract = new Interact(this, 1, "Climb the rope and extract");
            output.Add(nInteract);
        }

        return output;
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


        AIClusters.Add(new AICluster(1, 1, 1, false));
        AIClusters.Add(new AICluster(1, 1, 1, false));



        spawnCharacter(Instantiate(fodderGuardPrefab), CharacterAllegiance.Enemey, map.gridObject.gridCells[8, 5], new FodderGuard(), AIClusters[0]);
        spawnCharacter(Instantiate(fodderGuardPrefab), CharacterAllegiance.Enemey, map.gridObject.gridCells[9, 5], new FodderGuard(), AIClusters[0]);
        spawnCharacter(Instantiate(fodderGuardPrefab), CharacterAllegiance.Enemey, map.gridObject.gridCells[10, 5], new FodderGuard(), AIClusters[0]);
        spawnCharacter(Instantiate(fodderGuardPrefab), CharacterAllegiance.Enemey, map.gridObject.gridCells[11, 5], new FodderGuard(), AIClusters[0]);
        spawnCharacter(Instantiate(fodderGuardPrefab), CharacterAllegiance.Enemey, map.gridObject.gridCells[7, 8], new FodderGuard(), AIClusters[1]);
        spawnCharacter(Instantiate(fodderGuardPrefab), CharacterAllegiance.Enemey, map.gridObject.gridCells[8, 8], new FodderGuard(), AIClusters[1]);
        spawnCharacter(Instantiate(fodderGuardPrefab), CharacterAllegiance.Enemey, map.gridObject.gridCells[9, 8], new FodderGuard(), AIClusters[1]);
        spawnCharacter(Instantiate(fodderGuardPrefab), CharacterAllegiance.Enemey, map.gridObject.gridCells[10, 8], new FodderGuard(), AIClusters[1]);

        spawnCharacter(Instantiate(archerPrefab), CharacterAllegiance.Enemey, map.gridObject.gridCells[7, 7], new Archer(), AIClusters[1]);
        spawnCharacter(Instantiate(archerPrefab), CharacterAllegiance.Enemey, map.gridObject.gridCells[8, 7], new Archer(), AIClusters[0]);


        AIClusters[0].makeDormant();

        AIClusters[1].makeDormant();

        AIClusters.Add(new AICluster(1, 1, 1, false));

        spawnCharacter(Instantiate(ResourceLoader.loadGameObject("TestAssets/Kahund")), CharacterAllegiance.Enemey, map.gridObject.gridCells[5, 0], new Kahund(), AIClusters[2]);
    }

    public override void objectiveComplete(string id)
    {
        switch (id)
        {
            case "ext0":
                if (getAllAllegiance(CharacterAllegiance.Controlled).Count == 1)
                {
                    openEndWindow();
                }
                else
                {
                    objList.removeObjective("ext0");
                    objList.addObjective(new Objective("ext0", 1, BattleEventType.Interact).addDescription("Find a way to enter the fort"));
                    Debug.Log("this worked btw");
                }
                break;
        }
    }

    public void spawnPack()
    {

        spawnCharacter(Instantiate(fodderGuardPrefab), CharacterAllegiance.Enemey, map.gridObject.gridCells[8, 5], new FodderGuard());
        spawnCharacter(Instantiate(archerPrefab), CharacterAllegiance.Enemey, map.gridObject.gridCells[7, 7], new Archer(), AIClusters[1]);
    }

    void Start()
    {
        spawnCells = new List<ExWhyCell>();

        initializeBattleController();

        cursor.transform.position.Set(3, 3, 1);
        cursorCell = map.gridObject.gridCells[3, 3];

        activeCutscene = gameObject.AddComponent<GenericBattleCutscene>();

        activeCutscene.initialize(this);

        foreach (BattleCharacterObject BCO in getAllAllegiance(CharacterAllegiance.Controlled))
        {
            switch (BCO.getName())
            {
                case "Iraden":
                    activeCutscene.addSprite("Iraden", "Iraden");
                    break;


                case "Fray":
                    activeCutscene.addSprite("Fray", "Fray");
                    break;


                case "Morthred":
                    activeCutscene.addSprite("Morthred", "Morthred");
                    break;

                case "Ruless":
                    activeCutscene.addSprite("Ruless", "Ruless");
                    break;

                case "Kim":
                    activeCutscene.addSprite("Kim", "Kim");
                    break;


                case "Dorcia":
                    activeCutscene.addSprite("Dorcia", "Dorcia");
                    break;
            }
        }

        if(activeCutscene.containsSprite("Fray"))
        {
            activeCutscene.addFrame("Fray", "Quiet. Up ahead", "Fray");
            activeCutscene.addFrame("Fray", "I don't think they're friendlies", "Fray");
        }else if(activeCutscene.containsSprite("Morthred"))
        {
            activeCutscene.addFrame("Morthred", "Shush. I hear something", "Morthred");
            activeCutscene.addFrame("Morthred", "Eastern Hai people ahead, but not members of the official militia", "Morthred");
            activeCutscene.addFrame("Morthred", "Dirty peasant rebels!", "Morthred");
        }

        if (activeCutscene.containsSprite("Ruless"))
        {
            activeCutscene.addFrame("Ruless", "Disgusting", "Ruless");
            activeCutscene.addFrame("Ruless", "I could cut these down in a matter of seconds", "Ruless");
            activeCutscene.addFrame("Ruless", "Even with those Frilend archers", "Ruless");
        }
        
        if (activeCutscene.containsSprite("Kim"))
        {
            activeCutscene.addFrame("Kim", "I'd appreciate the chance to cleave my way through them", "Kim");
            activeCutscene.addFrame("Kim", "To test my strength against an enemy so numerous", "Kim");
        }

        if (activeCutscene.containsSprite("Iraden"))
        {
            activeCutscene.addFrame("Iraden", "If they knew we were coming, they might've left a way for us to get past them", "Iraden");
            activeCutscene.addFrame("Iraden", "We should search around", "Iraden");
            activeCutscene.addFrame("Iraden", "Maybe we can avoid a fight if we stay out of sight", "Iraden");
        }

        else if (activeCutscene.containsSprite("Morthred"))
        {
            activeCutscene.addFrame("Morthred", "Operation etiquette suggests that the general provide an alternative access route \n if available", "Morthred");
            activeCutscene.addFrame("Morthred", "We should look around", "Morthred");
        }

        else if (activeCutscene.containsSprite("Fray"))
        {
            activeCutscene.addFrame("Fray", "I'm don't feel like trying to take all these people one", "Fray");
            activeCutscene.addFrame("Fray", "They probably have more coming too", "Fray");
            activeCutscene.addFrame("Fray", "With a bit of luck, there should be another way. \n They knew we were coming, so there's probably something.", "Fray");
        }

        activeCutscene.createDialogueObject();

        initializeGameState();

        initializeAdditionalElements();


        activeCutscene.preInitializeTextbox();
        activeCutscene.setFrame(0);
        toggleCutscene(true);

        lookForBattleEventListeners();

        spawnDecorations(new Vector2(52, 22), "Rope");
        spawnDecorations(new Vector2(0, 0), "NightFG");

        addObjectiveHighlight(14, 2);


        objList.addObjective(new Objective("ext0", 1, BattleEventType.Interact).addDescription("Find a way to enter the fort"));

        objList.addObjective(new Objective("pkSpawn0", 1, BattleEventType.Time).addModifier(ObjectiveModifier.GreaterThan, 50).makeInvisible());
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
