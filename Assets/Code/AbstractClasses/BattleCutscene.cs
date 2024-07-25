using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BattleCutscene : Cutscene
{

    protected BattleController BC;
    public void initialize(BattleController BatCont)
    {
        BC = BatCont;
        speakerSprites = new Dictionary<string, Sprite>();
        frames = new List<CutsceneFrame>();

    }
    public void preInitializeTextbox()
    {
        bigTextBox.GetComponent<CutsceneTextBoxController>().initialize();
    }

}
