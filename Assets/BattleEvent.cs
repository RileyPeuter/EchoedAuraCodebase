using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleEvent
{
    //###MemberVariables###
    public string subject = null;
    public string eventType = null;
    public string verb = null;
    public string target = null;
    public string result = null;

    //###Utilities###
    public string toString()
    {
        return (subject + "  " + eventType + " " + verb + " " + target + " " + result);
    }

    //###Constructors###
    public BattleEvent()
    {

    }

    public BattleEvent(string S = "", string ET = "", string V = "", string T = "", string R = "")
    {
        subject = S;
        eventType = ET;
        verb = V;
        target = T;
        result = R;
    }
}
