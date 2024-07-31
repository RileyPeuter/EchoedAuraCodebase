using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DorciaMeetCutscene : Cutscene
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
                print(Camera.main.orthographicSize);
                Destroy(bigTextBox);

                bigTextBox = GameObject.Instantiate(Resources.Load<GameObject>("UIElements/uI_Dialogue_GameObject"));
                bigTextBox.GetComponent<CutsceneTextBoxController>().initialize();

                Camera.main.orthographicSize = 5;
                Camera.main.transform.position = new Vector3(0, 0, -10);

                GameObject.Find("iraden").GetComponent<CutsceneActorController>().SetMove(Vector2.right, 10, 1);
                phaseCutscene(3);
                break;
        }
    }

    public override void spawnVisuals()
    {
        Instantiate(Resources.Load<GameObject>("CutsceneAssets/CutsceneVisualObjectDorcia"));
    }

    private void Start()
    {
        speakerSprites = new Dictionary<string, Sprite>();
        frames = new List<CutsceneFrame>();

        Camera.main.orthographicSize = 8;
        Camera.main.transform.position += new Vector3(-5, -5, 0);

        addSprite("Iraden", "Iraden");
        addSprite("Fray", "Fray");
        addSprite("Dorcia1", "Dorcia1");
        addSprite("Dorcia2", "Dorcia2");
        addSprite("Morthred", "Morthred");

        addFrame("Fray", "Dorcia's been getting too polite to ask for an escort duing her walks.  \n Probably a good idea to check up on her", "Fray");
        addFrame("Morthred", "If we must. She'll atleast be happy for a new person to chat to", "Morthred");
        addFrame("Iraden", "She a talkative person?", "Iraden");
        addFrame("Morthred", "Quite", "Morthred");
        addFrame("Fray", "Uhhh...Yeah.", "Fray",1);
        addFrame("Dorcia", "Fray! Morthred!. Over here!", "Dorcia2");
        addFrame("Dorcia", "I'm a little stuck. These leggings have ceazed a little. I think I'm low on mana ", "Dorcia2");
        addFrame("Fray", "Ask Asa next time. He can problam spare a phantom to keep an eye on you. \n Atleast until we get your new gear figured out", "Fray");
        addFrame("Iraden", "How can i undo the armour? I don't mind helping carry it", "Iraden");
        addFrame("Dorcia", "I've barely met you and you're already trying to get my clothes off!?!", "Dorcia1");
        addFrame("Dorcia", "I'm sorry, but i'm a little attached to it. I couldn't just walk away from it in a place like this", "Dorcia1");
        addFrame("Fray", "Dorcia, Jayf just tried stabbing new guy. Give him a break. New guy, \n ...Iraden. We're gonna have to push her back...Because she's an idiot", "Fray");
        addFrame("Dorcia", "Haha. Yeah, that sounds like him. \n Lets GOOO!", "Dorcia2");


        bigTextBox = GameObject.Instantiate(Resources.Load<GameObject>("UIElements/uI_Dialogue_GameObject"));
     //   bigTextBox.GetComponent<CutsceneTextBoxController>().alterSize(5);
        bigTextBox.transform.position = Camera.main.transform.position + new Vector3(0, -1, 1);

        spawnVisuals();
    }

    void Update()
    {
        if (phaseTime > 0)
        {
            testPhase();
            return;
        }
        buttonCheck();
    }
}
