using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SiegeCutscene2 : Cutscene
{
    public override void endCutscene()
    {
        GlobalGameController.GGC.startBattle();
    }

    public override void executeTrigger(int triggerIndex)
    {
        switch (triggerIndex)
        {
            case 1:
                foreach (CutsceneActorController CAC in actorControllers)
                {
                    CAC.SetMove(Vector2.right, 4f, 1.6f);
                    phaseCutscene(3f);
                }
                break;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        initialize();

        actorSpawnLocations.Add(new Vector2(-9, -0.7f));
        actorSpawnLocations.Add(new Vector2(-10, -1.4f));
        actorSpawnLocations.Add(new Vector2(-11, -0.8f));
        actorSpawnLocations.Add(new Vector2(-12, -1.4f));

        spawnActors(loadActorsFromStoreCharacters());
        loadSpritesFromStoredCharacters();



        addSprite("Leigh", "Leigh");


        addFrame("", "The party returns to the command room deep in the fort", "");
        addFrame("Leigh", "...no, we have enough kindling, get more fuel. \nThe oiled funiture will light in a flash, so spread it out", "Leigh", 1);


        if (containsSprite("Ruless"))
        {
            addFrame("Ruless", "General. Eastern Hai, Eastern Hai soldiers are here!", "Ruless");
            addFrame("Ruless", "I'm...I'm ready to give my report", "Ruless");
        }

        else if (containsSprite("Morthred"))
        {
            addFrame("Morthred", "General!", "Morthred");
            addFrame("Morthred", "I'd strongly suggest you implement stronger intelligence practices \nprior to things getting to this stage.", "Morthred");
            addFrame("Morthred", "I'd be happy to deliver the report on these problematic forces", "Morthred");
            addFrame("Leigh", "You weren't late, these rebels were early", "Leigh");
        }

        else if (containsSprite("Fray"))
        {
            addFrame("Fray", "We did it...barely", "Fray");
            addFrame("Leigh", "We're in the thick of it", "Leigh");
        }

        else if (containsSprite("Iraden"))
        {
            addFrame("Iraden", "General.", "Iraden");
            addFrame("Iraden", "I count 100 peasants, the front, with Friland arch...", "Iraden");
            addFrame("Leigh", "That'll be all", "Leigh");
        }


        else if (containsSprite("Kim"))
        {
            addFrame("Kim", "There's so many out there", "Kim");
            addFrame("Kim", "I think there's...1,2,3...", "Kim");
            addFrame("Leigh", "No, it's fine, I...appreciate you trying", "Leigh");
        }


        addFrame("Leigh", "Here. The written report. Keep it sealed and safe \nEnsure it gets back to Asa for his review", "Leigh");

        if (containsSprite("Morthred"))
        {
            addFrame("Morthred", "General! We travelled across countries to arrive here to survey this", "Morthred");
            addFrame("Morthred", "I MUST insist you atleast take our account into consideration...", "Morthred");
           if (containsSprite("Fray"))
            {
                addFrame("Fray", "Morth, forget it. We did it", "Fray");
                addFrame("Morthred", "But...", "Morthred");
            }

            addFrame("Leigh", "That will be all. You've served admirablly.", "Leigh");
        }

        else if (containsSprite("Ruless"))
        {
            addFrame("Ruless", "General Leigh...", "Ruless");
            addFrame("Ruless", "We're ready to provide our verbal report too...\ninform the written report", "Ruless");
            if (containsSprite("Fray"))
            {
                addFrame("Fray", "Soldier Ruless, don't you think the general has more to do\nthan listen to his auxliary troops discuss pedantry?", "Fray");
            }
            addFrame("Leigh", "That will be all. You've served admirablly.", "Leigh");
        }


        else if (containsSprite("Iraden"))
        {
            addFrame("Iraden", "Written report? We just got back.", "Iraden");
            addFrame("Iraden", "Surveying wasn't our mission was it...", "Iraden");
            addFrame("Leigh", "Of course it was. Now you need to take the written form back to \n superior officer", "Leigh");
            addFrame("Iraden", "Do you use weave manm or...", "Iraden");
        }

        addFrame("Leigh", "YOU, GET SOME OF THE LAMP OIL, DOWN IN THE CELLAR, LINE THE  GROUND FLOOR WITH IT", "Leigh");
        addFrame("Leigh", "...", "Leigh");

        addFrame("", "The general lets out deep sigh", "Leigh");


        addFrame("Leigh", "They're forcing my hand here. This has to stop...", "Leigh");
        addFrame("Leigh", "You need to get out and deliver that letter. We're preparing to get out of here ourselves", "Leigh");

        if (containsSprite("Fray"))
        {
            addFrame("Fray", "Why leave?", "Fray");
            addFrame("Fray", "Even though they're through the gates, you could hold them outside for weeks.", "Fray");
        }

        if (containsSprite("Morthred"))
        {
            addFrame("Morthred", "A tactical retreat? Falling back to reinforcements", "Morthred");
        }


        addFrame("Leigh", "We're gonna fake a retreat through the fort. Draw some in them in.\nThen light it up. Trap them in there as we surround them", "Leigh");



        if (containsSprite("Ruless"))
        {
            addFrame("Ruless", "A cunning tactic. An Intelligent play", "Ruless");
        }

        if (containsSprite("Iraden"))
        {
            addFrame("Ruless", "Burning people...", "Ruless");
            addFrame("Ruless", "Isn't pretty.", "Ruless");
            addFrame("Ruless", "Even in war. But if you must do it...", "Ruless");

            addFrame("Leigh", "Oh, i'm well aware", "Leigh");
        }


        addFrame("Leigh", "I'm trapping them", "Leigh");
        addFrame("Leigh", "A blaze, visible from anywhere that people might be marching from", "Leigh");
        addFrame("Leigh", "Along with their screams. Then the smell", "Leigh");
        addFrame("Leigh", "Maybe cooking flesh will smell good to people so starved", "Leigh");

        addFrame("Leigh", "We'll surrounding a few them as they burn. Herd them into the fire. ", "Leigh");
        addFrame("Leigh", "Make them feel like animals. Make sure the others see.", "Leigh");

        addFrame("Leigh", "They'll never do this again", "Leigh");

        addFrame("Leigh", "...", "Leigh");

        addFrame("Leigh", "Now, you all must excuse me. I've got the final preperations, and \nyou must finish your mission with the report", "Leigh");
        addFrame("Leigh", "Please tell Asa that you've all performed well", "Leigh");

        bigTextBox = GameObject.Instantiate(Resources.Load<GameObject>("UIElements/uI_Dialogue_GameObject"));
        //bigTextBox.GetComponent<CutsceneTextBoxController>().alterSize(-0.5f);
        bigTextBox.transform.position = Camera.main.transform.position + new Vector3(0, -2, 1);
        spawnVisuals();

        // Camera.main.orthographicSize += 5;
    }

    // Update is called once per frame
    void Update()
    {
        if (phaseTime > 0)
        {
            testPhase();
            return;
        }

        buttonCheck();
    }

    public override void spawnVisuals()
    {
        Instantiate(Resources.Load<GameObject>("CutsceneAssets/LeighMeetCutscene"));

    }
}
