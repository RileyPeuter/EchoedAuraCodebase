using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TrainingGroundBattleController : BattleController
{

    public override void objectiveComplete(string id)
    {
        switch (id)
        {
            case "tgAtk0":
                objList.addObjective("tgAtk1", "Use a Ranged Strike at the target", 1, ObjectiveType.Attack, null, "Hit", "RangedStrike", characters[2].getName(), null );
                addTacticalPoints();

                activeCutscene = gameObject.AddComponent<GenericBattleCutscene>();
                activeCutscene.initialize(this);
                activeCutscene.addSprite("Jayf", "Jayf");
                activeCutscene.addFrame("Jayf", "Hmm. Decent power. COULD do some serious damage.", "Jayf");
                activeCutscene.addFrame("Jayf", "Now try throwing it at your target.", "Jayf");
                activeCutscene.createDialogueObject();
                activeCutscene.preInitializeTextbox();
                activeCutscene.setFrame(0);
                toggleCutscene(true);

                break;


            case "tgAtk1":
                objList.addObjective("tgAtk2", "Throw your spell at the  tartget", 1, ObjectiveType.Attack, null, "Hit", "IceShuriken", characters[2].getName(), null);
                addTacticalPoints();

                activeCutscene = gameObject.AddComponent<GenericBattleCutscene>();
                activeCutscene.initialize(this);
                activeCutscene.addSprite("Jayf", "Jayf");
                activeCutscene.addFrame("Jayf", "Congratulations, you've managed to hit a stationary target.", "Jayf");
                activeCutscene.addFrame("Jayf", "Ranged attacks usually do less damage and are easier to block and parry. \n However they are harder to dodge, and you won't leave yourself open to \n a counter attack should they parry you.", "Jayf");
                activeCutscene.addFrame("Jayf", "Now, show me this Spell. ", "Jayf");
                activeCutscene.createDialogueObject();
                activeCutscene.preInitializeTextbox();
                activeCutscene.setFrame(0);
                toggleCutscene(true);

                break;

            case "tgAtk2":
                activeCutscene = gameObject.AddComponent<GenericBattleCutscene>();
                activeCutscene.initialize(this);
                activeCutscene.addSprite("Jayf", "Jayf");
                activeCutscene.addSprite("Iraden", "Iraden");
                activeCutscene.addFrame("Jayf", "Hmmm, impressive. You've shown some creativity. \n Some skill with mana and physical ability...", "Jayf");
                activeCutscene.addFrame("Iraden", "Does that mean you'll take me in.", "Iraden");
                activeCutscene.addFrame("Jayf", "...but seems too cumbersome to use. Not a great weapon at the moment. ", "Jayf");
                activeCutscene.addFrame("Jayf", "And I have no intention of 'taking you in'. \n We expect people to pull their own weight here", "Jayf");
                activeCutscene.addFrame("Jayf", "Hmm. I do have two people on an errand in a town. \n Go and find them. I'll be watching to see how you do", "Jayf");
                activeCutscene.addFrame("Iraden", "Ok", "Iraden", 2);
                activeCutscene.createDialogueObject();
                activeCutscene.preInitializeTextbox();
                activeCutscene.setFrame(0);
                toggleCutscene(true);
                break;

        }
     objList.updateList();
    }



    public override void endBattle()
    {
        GlobalGameController.GGC.mapToBeLoaded = new ExWhyLocalTown();

        GlobalGameController.GGC.BCToBeLoaded = 1;
        GlobalGameController.GGC.startBattle();
    }


    public override void loadCharacters()
    {
        List<GameObject> characterGOs;
        List<GameObject> clones = new List<GameObject>();
        characterGOs = new List<GameObject>();
        characterGOs.Add(Resources.Load<GameObject>("TestAssets/Iraden"));
        characterGOs.Add(Resources.Load<GameObject>("TestAssets/TrainingDummy"));
        characterGOs.Add(Resources.Load<GameObject>("TestAssets/TargetDummy"));
        characterGOs.Add(Resources.Load<GameObject>("TestAssets/Jayf"));

        foreach (GameObject character in characterGOs)
        {
            GameObject GO = Instantiate(character);
            clones.Add(GO);

            //GO.GetComponent<BattleCharacterObject>().spawnCharacter(gridObject);
            
        }

        characters.Add(GameObject.Find("Jayf(Clone)").GetComponent<BattleCharacterObject>());//.initiate(7, 7, new Iraden(), CharacterAllegiance.Allied);
        characters.Add(GameObject.Find("TrainingDummy(Clone)").GetComponent<BattleCharacterObject>());//.initiate(5, 6, new TrainingDummy(), CharacterAllegiance.Enemey);
         characters.Add(GameObject.Find("TargetDummy(Clone)").GetComponent<BattleCharacterObject>());//.initiate(4, 6, new TrainingDummy(), CharacterAllegiance.Enemey);
         characters.Add(GameObject.Find("Iraden(Clone)").GetComponent<BattleCharacterObject>());//.initiate(4, 1, new Iraden(), CharacterAllegiance.Allied);

        characters[0].initialize(7, 7, new Jayf(), CharacterAllegiance.Allied);
        characters[0].makeDormant();
        characters[1].initialize(6, 5, new TrainingDummy(), CharacterAllegiance.Allied);
        characters[1].makeDormant();
        characters[2].initialize(4, 6, new TrainingTarget(), CharacterAllegiance.Allied);
        characters[2].makeDormant();
        characters[3].initialize(4, 1, new Iraden(), CharacterAllegiance.Controlled);

        foreach (GameObject character in clones)
        {
            character.GetComponent<BattleCharacterObject>().spawnCharacter(map.gridObject);
            //GO.GetComponent<BattleCharacterObject>().spawnCharacter(gridObject);
        }

    } 


    // Start is called before the first frame update
    void Start()
    {
        initializeBattleController();
        cursor.transform.position.Set(3, 3, 1);
        cursorCell = map.gridObject.gridCells[3, 3];
        initializeGameState();

   
        
        activeCutscene = gameObject.AddComponent<GenericBattleCutscene>();
        activeCutscene.initialize(this);
        activeCutscene.addSprite("Jayf", "Jayf");
        activeCutscene.addSprite("Tutorial1", "Tutorials1");
        activeCutscene.addSprite("Tutorial2", "Tutorials2");

        activeCutscene.addFrame("Jayf", "Now, let's start off with a simple strike with your, so called, long sword. \nGo up and slash the currias.", "Jayf");
        activeCutscene.addFrame("Tutorial", "Use WASD/Left Click to move the cursor. \n Use Space/Right Click with a selected ability to act.", "Tutorial1");
        activeCutscene.addFrame("Tutorial", "Your actions each turn are limited by your Movement and Mana Flow. \n Hit 'End Turn' to refil", "Tutorial2");

        activeCutscene.createDialogueObject();

        initializeAdditionalElements();


        activeCutscene.preInitializeTextbox();
        activeCutscene.setFrame(0);
        toggleCutscene(true);
        objList.addObjective("tgAtk0", "Use your melee strike on the Training Dummy", 1, ObjectiveType.Attack, null, "Hit", "MeleeStrike", "Training Dummy", null);

        GlobalGameController.GGC.playMusic(Resources.Load<AudioClip>("Audio/Music/HonkHonk"));
    }

    // Update is called once per frame
    void Update()
    {
        controls();

    }




    public override void GBCTexecutions(int index)
    {
        switch (index)
        {
            case 2:
                endBattle();
                break;
        }

    }

}
