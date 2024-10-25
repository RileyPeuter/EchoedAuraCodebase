using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RulessDogCutscene : Cutscene
{
    public override void endCutscene()
    {
        GlobalGameController.GGC.startBattle();
    }


    // Start is called before the first frame update
    void Start()
    {

        Camera.main.orthographicSize = 3;
        Camera.main.transform.position += new Vector3(-5, 0, 0);
        speakerSprites = new Dictionary<string, Sprite>();
        speakerSprites.Add("Iraden", Resources.Load<Sprite>("CutsceneSpeakerSprites/Iraden"));
        speakerSprites.Add("Ruless", Resources.Load<Sprite>("CutsceneSpeakerSprites/Ruless"));
        speakerSprites.Add("Fray", Resources.Load<Sprite>("CutsceneSpeakerSprites/Fray"));
        speakerSprites.Add("Morthred", Resources.Load<Sprite>("CutsceneSpeakerSprites/Morthred"));

        frames = new List<CutsceneFrame>();

        addFrame("Fray", "We should probably go check out on Ruless. \nShe was doing a bit of escorting for merchants. \n It's a 50 50 that she met some trouble, or she's playing with her sword ", "Fray");
        addFrame("Morthred", "She's our sword dueling specialist. \nShe is quite persist, though noticably proud of her training", "Morthred");
        addFrame("Fray", "You can trust her to be your frontline \nRuless may come from some foreign rich family but \n she's not affraid to try her luck parrying danger", "Fray");
        addFrame("", "A hint of mold and rotten flesh becomes noticable", "");
        addFrame("Ruless", "Disguting vermin.", "Ruless", 1);
        addFrame("Fray", "Need some help here?", "Fray");
        addFrame("Ruless", "The cowards took their steads and ran leaving me here \n I should be fine dealing with this things alone \n until they return", "Ruless");
        addFrame("Ruless", "...though if you could help keeping their filthy bodies away from \n the food in the cart and me, I wouldn't say no", "Ruless");
        addFrame("Morthred", "Kahunds are...less than sanitary. \nMy blades will need disinfecting after this", "Morthred");
        addFrame("Ruless", " 'Kahunds'...The swift wolves of Cerila atleast had some...nobility about them.", "Ruless");
        addFrame("Iraden", "I think they're coming", "Iraden");

        bigTextBox = GameObject.Instantiate(Resources.Load<GameObject>("UIElements/uI_Dialogue_GameObject"));
        bigTextBox.GetComponent<CutsceneTextBoxController>().alterSize(-0.5f);
        bigTextBox.transform.position = Camera.main.transform.position + new Vector3(0, -1, 1);
        spawnVisuals();
    }

    public override void spawnVisuals()
    {
        Instantiate(Resources.Load<GameObject>("CutsceneAssets/CutsceneVisualObjectRulessDog"));
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
}
