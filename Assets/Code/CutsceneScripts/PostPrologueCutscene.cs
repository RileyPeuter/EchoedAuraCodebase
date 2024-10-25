using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PostPrologueCutscene : Cutscene
{
    public override void endCutscene()
    {
        GlobalGameController.GGC.startBattle();
    }

    public override void executeTrigger(int triggerIndex)
    {
        switch (triggerIndex)
        {
            //Iraden Run
            case 1:
                GameObject.Find("Iraden").GetComponent<CutsceneActorController>().SetMove(Vector2.right, 10, 1);
                phaseCutscene(3);
                break;

            //Asa explode
            case 2:

                break;


            //Camera Pan
            case 3:

                break;


            //Iraden Run off
            case 4:

                break;


            //Change scene
            case 5:

                break;
        }

    }

    void Start()
    {
        frames = new List<CutsceneFrame>();
        speakerSprites = new Dictionary<string, Sprite>();

        resourceString = "PostPrelude";

        CutsceneLoader.loadCutscene(this);

        bigTextBox = GameObject.Instantiate(Resources.Load<GameObject>("UIElements/uI_Dialogue_GameObject"));

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
