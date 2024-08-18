using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeBattleController : BattleController
{
    public override void endBattle()
    {
        GlobalGameController.GGC.openManagment();   
    }

    public override void GBCTexecutions(int index)
    {
        switch (index)
        {
            case 2:
                openEndWindow();
            break;
        }
    }

    public override void loadCharacters()
    {
        List<GameObject> characterGOs;
        List<GameObject> clones = new List<GameObject>();
        characterGOs = new List<GameObject>();
        characterGOs.Add(Resources.Load<GameObject>("TestAssets/Jayf"));
        characterGOs.Add(Resources.Load<GameObject>("TestAssets/Fray"));
        characterGOs.Add(Resources.Load<GameObject>("TestAssets/Morthred"));
        characterGOs.Add(Resources.Load<GameObject>("TestAssets/Iraden"));

        foreach (GameObject character in characterGOs)
        {
            GameObject GO = Instantiate(character);
            clones.Add(GO);
            //GO.GetComponent<BattleCharacterObject>().spawnCharacter(gridObject);
        }

        characters.Add(GameObject.Find("Jayf(Clone)").GetComponent<BattleCharacterObject>());//.initiate(5, 6, new TrainingDummy(), CharacterAllegiance.Enemey);
        characters.Add(GameObject.Find("Fray(Clone)").GetComponent<BattleCharacterObject>());//.initiate(7, 7, new Iraden(), CharacterAllegiance.Allied);
        characters.Add(GameObject.Find("Morthred(Clone)").GetComponent<BattleCharacterObject>());//.initiate(4, 6, new TrainingDummy(), CharacterAllegiance.Enemey);
        characters.Add(GameObject.Find("Iraden(Clone)").GetComponent<BattleCharacterObject>());//.initiate(4, 1, new Iraden(), CharacterAllegiance.Allied);

        characters[1].initialize(9, 5, new Fray(), CharacterAllegiance.Controlled);
        characters[0].initialize(9, 9, new Jayf(), CharacterAllegiance.Enemey);
        characters[2].initialize(8, 5, new Morthred(), CharacterAllegiance.Controlled);
        characters[3].initialize(8, 4, new Iraden(), CharacterAllegiance.Controlled);

        foreach (GameObject character in clones)
        {
            character.GetComponent<BattleCharacterObject>().spawnCharacter(map.gridObject);
            //GO.GetComponent<BattleCharacterObject>().spawnCharacter(gridObject);
        }
    }

    public override void objectiveComplete(string id)
    {
        switch (id)
        {
            case "clnKil0":
                activeCutscene = gameObject.AddComponent<GenericBattleCutscene>();
                activeCutscene.initialize(this);
                activeCutscene.addSprite("Jayf", "Jayf");
                activeCutscene.addSprite("Iraden", "Iraden");
                activeCutscene.addSprite("Fray", "Fray");
                activeCutscene.addSprite("Morthred", "Morthred");

                activeCutscene.addFrame("Jayf", "Alright. That should be enough. You've proven yourself", "Jayf");
                activeCutscene.addFrame("Jayf", "One last thing. Why here specifically? \nWhy not a mercenary troup like your father? \n Or just a normal life. You have the skill with Magic to live comfortable. ", "Jayf");
                activeCutscene.addFrame("Iraden", "As a kid traveling with them, I saw some thing. \nVillages beind wipes out in wars that had nothing to do with them", "Iraden");
                activeCutscene.addFrame("Iraden", "I want a say in what happens to me. I'd like to help others have a say \nHere you stop battles. Or ensure they end quickly. \nLeaves people around safe", "Iraden");
                activeCutscene.addFrame("Jayf", "A fair desire. And a fair assessment of what we do. \n Lack the stomach to carry our what's required of you and you won't live \n to try to find a place here. I'll make sure of that", "Jayf");
                activeCutscene.addFrame("Jayf", "Waxing poetic about the morality is right to do before and after missions. \nNever question the mission once you're on it.", "Jayf");
                activeCutscene.addFrame("Jayf", "Accept that, and you have a place here", "Jayf");
                activeCutscene.addFrame("Jayf", "Morthred. See to his wounds \nFray, when you're finished with your duties, find some quaters for Iraden", "Jayf");
                activeCutscene.addFrame("Morthred", "Ooh, a chance to treat someone with hydro mana!", "Morthred");
                activeCutscene.addFrame("Fray", "Fine. Lets go check on the others. Let me know when you start getting tired", "Fray");
                activeCutscene.addFrame("Fray", "Try not to get too weirded out by Morts enthusiam. ", "Fray");
                activeCutscene.addFrame("Fray", "Mort, don't go and try getting blood", "Fray", 2);

                activeCutscene.createDialogueObject();
                initializeAdditionalElements();

                initializeGameState();
                activeCutscene.preInitializeTextbox();
                activeCutscene.setFrame(0);
                toggleCutscene(true);

                break;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        initializeBattleController();
        cursor.transform.position.Set(12, 12, 1);
        cursorCell = map.gridObject.gridCells[3, 3];

        activeCutscene = gameObject.AddComponent<GenericBattleCutscene>();
        activeCutscene.initialize(this);
        activeCutscene.addSprite("Jayf", "Jayf");
        activeCutscene.addSprite("Tutorial", "Tutorials3");
        activeCutscene.addFrame("Jayf", "An adequete dispaly. I think I've gotten a read on your ability", "Jayf");
        activeCutscene.addFrame("Jayf", "Now, to see how you stand in the face of violence", "Jayf");
        activeCutscene.addFrame("Tutorial", "When attacked you will attempt to react. \n Either by dodging, blocking or parrying", "Tutorial");
        activeCutscene.addFrame("Tutorial", "Blocking is safest, reducing some damage even if hit \n Dodge is also safe, adding you movement to make the difference should it fail. \n Parry will perform a counter should it succeed", "Tutorial");
        activeCutscene.createDialogueObject();
        initializeAdditionalElements();

        initializeGameState();
        activeCutscene.preInitializeTextbox();
        activeCutscene.setFrame(0);
        toggleCutscene(true);

        characters[0].getAI().setOverrideAbility(characters[0].getAllAbilities().Find(x => x.name == "MagusPhantasma"));
        AIClusters.Add(new AICluster(999, 0, 1));
        AIClusters[0].ClusterMembers.Add(characters[0].getAI());

        Objective nBattleObjective = new Objective("clnKil0", 3, BattleEventType.Kill).addDescription("Defeat Jay's Phantasmas").addSubject("Jayf");
        objList.addObjective(nBattleObjective);
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
