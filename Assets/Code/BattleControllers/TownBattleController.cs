using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public class TownBattleController : BattleController
{
    public override void endBattle()
    {
        GlobalGameController.GGC.mapToBeLoaded = new ExWhyBridge();
        GlobalGameController.GGC.BCToBeLoaded = 2;
        GlobalGameController.GGC.startBattle();
    }

    public override void loadCharacters()
    {
        List<GameObject> characterGOs;
        List<GameObject> clones = new List<GameObject>();
        characterGOs = new List<GameObject>();
        characterGOs.Add(Resources.Load<GameObject>("TestAssets/Iraden"));
        characterGOs.Add(Resources.Load<GameObject>("TestAssets/Drunk"));
        characterGOs.Add(Resources.Load<GameObject>("TestAssets/Fray"));
        characterGOs.Add(Resources.Load<GameObject>("TestAssets/Morthred"));

        foreach (GameObject character in characterGOs)
        {
            GameObject GO = Instantiate(character);
            clones.Add(GO);
            //GO.GetComponent<BattleCharacterObject>().spawnCharacter(gridObject);
        }

        characters.Add(GameObject.Find("Iraden(Clone)").GetComponent<BattleCharacterObject>());//.initiate(4, 1, new Iraden(), CharacterAllegiance.Allied
        characters.Add(GameObject.Find("Fray(Clone)").GetComponent<BattleCharacterObject>());//.initiate(7, 7, new Iraden(), CharacterAllegiance.Allied);
        characters.Add(GameObject.Find("Drunk(Clone)").GetComponent<BattleCharacterObject>());//.initiate(5, 6, new TrainingDummy(), CharacterAllegiance.Enemey);
        characters.Add(GameObject.Find("Morthred(Clone)").GetComponent<BattleCharacterObject>());//.initiate(4, 6, new TrainingDummy(), CharacterAllegiance.Enemey);
        
        characters[0].initialize(4, 1, new Iraden(), CharacterAllegiance.Controlled);
        characters[1].initialize(7, 7, new Fray(), CharacterAllegiance.Controlled);
        characters[2].initialize(6, 5, new Drunk(), CharacterAllegiance.Enemey);
        characters[2].makeDormant();
        characters[3].initialize(4, 6, new Morthred(), CharacterAllegiance.Controlled);

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

            case "dkAtk0":
                Objective nBattleObjective = new Objective("dkHit0", 1, BattleEventType.Hit).addDescription("Deal damage to the drunk (use Fray's Shadow to reduce dodge)").addModifier(ObjectiveModifier.GreaterThan, 1).addTarget("Drunk");
                objList.addObjective(nBattleObjective);
                
                activeCutscene = gameObject.AddComponent<GenericBattleCutscene>();
                activeCutscene.initialize(this);

                activeCutscene.addSprite("Morthred", "Morthred");
                activeCutscene.addSprite("Iraden", "Iraden");
                activeCutscene.addSprite("Fray", "Fray");
                activeCutscene.addSprite("Tutorial2", "Tutorial2");

                activeCutscene.addFrame("Fray", "Just like the past few guards that took a swing. \n  ", "Fray");
                activeCutscene.addFrame("Morthred", "Fray! We have to the chance to be a little more tactical here", "Morthred");
                activeCutscene.addFrame("Fray", "Ahhh, sure.\n You in the blue, get ready to try again after we've worked a little magic", "Fray");
                activeCutscene.addFrame("Fray", "Morthred. Move to overwhelm. Take his attention", "Fray");
                activeCutscene.addFrame("Morthred", "Ready!", "Morthred");
                activeCutscene.addFrame("Tutorial2", "The Drunk's dodge is too high. \n You can add debuffs to the enemy to lower them. \n", "Tutorial2");
                activeCutscene.addFrame("Tutorial2", "Enemies will automatically become easier to hit when they successfully react to attacks \n Combine this with Fray's non damaging special ability to land damage \n", "Tutorial2");


                activeCutscene.createDialogueObject();
                activeCutscene.preInitializeTextbox();
                activeCutscene.setFrame(0);
                toggleCutscene(true);
                break;

            case "dkHit0":
                activeCutscene = gameObject.AddComponent<GenericBattleCutscene>();
                activeCutscene.initialize(this);

                activeCutscene.addSprite("Morthred", "Morthred");
                activeCutscene.addSprite("Iraden", "Iraden");
                activeCutscene.addSprite("Drunk", "Drunk");
                activeCutscene.addSprite("Fray", "Fray");
                activeCutscene.addFrame("Drunk", "Ehgh!", "Drunk");
                activeCutscene.addFrame("Drunk", ".fIne imm leafing for soemwehe else", "Drunk");
                activeCutscene.addFrame("Drunk", "yah bloody KidS", "Drunk");
                activeCutscene.addFrame("Morthred", "Well, surprisingly aduqette. \n Maintain this efficiency and you'll be an asset to the team", "Morthred");
                activeCutscene.addFrame("Fray", "Our glorious civil service done, lets head back", "Fray", 1);
                

                activeCutscene.createDialogueObject();
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
        activeCutscene.addSprite("Fray", "Fray");
        activeCutscene.addSprite("Morthred", "Morthred");
        activeCutscene.addSprite("Iraden", "Iraden");
        activeCutscene.addSprite("Tutorial1", "Tutorial1");

        
        activeCutscene.addFrame("Fray", "I can't believe we're stucking dealing with senile delinquients", "Fray");
        activeCutscene.addFrame("Morthred", "This shouldn't be an issue. He's blind drunk. \n Medically speaking, she shouldn't even be able to stand"  , "Morthred");
        activeCutscene.addFrame("Fray", "I can think of so many other things we could be doing right now.", "Fray");
        activeCutscene.addFrame("Iraden", "I'll take it you're the one's i'm supposed to find", "Iraden");
        activeCutscene.addFrame("Fray", "Huh? Did fancy pants send you?", "Fray");
        activeCutscene.addFrame("Iraden", "I... think so", "Iraden");
        activeCutscene.addFrame("Fray", "We're trying to get rid of that drunk in the street there.  \n Just hit him with something", "Fray");

        activeCutscene.addFrame("Tutorial", "This is the most complicated thing you'll need to understand. \n" +
        "This is the 'Attack Attempt' window.", "Tutorial1");
        activeCutscene.addFrame("Tutorial", "This show's the chance to hit your opponent on a scale of 1-10, \n depending on what reaction they use. \n " +
        "Any more than 10 will always hit. Less than 0, never will", "Tutorial1");
            
        initializeAdditionalElements();

        initializeGameState();

        activeCutscene.createDialogueObject();
        activeCutscene.preInitializeTextbox();

        activeCutscene.setFrame(0);

        toggleCutscene(true);

        addObjectiveHighlight(characters[2]);
        Objective nBattleObjective = new Objective("dkAtk0", 1, BattleEventType.Attack).addDescription("Attack the drunk. Drive him away").addTarget("Drunk");
        objList.addObjective(nBattleObjective);

        //  activeCutscene.preInitializeTextbox();
        //   activeCutscene.setFrame(0);
        // toggleCutscene(true);

        goldRewards = 20;
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
            case 1:
                openEndWindow();
            break;
        }
    }

}
