using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericBattleCutscene : BattleCutscene
{
    public override void endCutscene()
    {
        BC.toggleCutscene(false);
        GameObject.Destroy(bigTextBox);
        //This looks wacky, and sort of redundant, but I think I need to do it this way.
        Destroy(gameObject.GetComponent<GenericBattleCutscene>());
    }

    public override void executeTrigger(int triggerIndex)
    {
        BC.GBCTexecutions(triggerIndex);
    }

    void Start()
    {
        // setFrame(frames[0]);
        setDialogueLocation();
    }

    void Update()
    {
        buttonCheck();
        setDialogueLocation();
    }
}
