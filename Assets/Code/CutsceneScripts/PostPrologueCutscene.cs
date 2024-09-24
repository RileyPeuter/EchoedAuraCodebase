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
            case 1:

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
