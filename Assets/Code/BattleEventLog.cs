using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleEventLog : MonoBehaviour
{
    //###MemeberVariables###   
    List<BattleEvent> eventLog;
    Text listText;
    ObjectiveList OL;

    //###Utilities###
    public void addEvent(string S = "", string ET = "", string V = "", string T = "", string R = "")
    {
        BattleEvent BE = new BattleEvent(S, ET, V, T, R);
        eventLog.Add(BE);
        OL.checkObjecitve(BE);
        appendText(BE);
    }

    public void appendText(BattleEvent BE)
    {
        listText.text += "\n \n" + BE.toString();
    }

    //###Initializer###
    public void initialize(ObjectiveList ObLi)
    {
        OL = ObLi;
    }

    //###UnityMessages###
    public void Start()
    {
        eventLog = new List<BattleEvent>();
        listText = GetComponentInChildren<Text>();  

    }    
}   