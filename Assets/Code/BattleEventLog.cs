using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleEventLog : MonoBehaviour
{
    //###MemeberVariables###   
    List<BattleEvent> eventLog;
    Text listText;

    //Subscribers
    List<BattleEventListener> listeners;

    //###Utilities###
    public void addEvent(BattleEventType nBattleEventType, string nSubject = "",  string nVerb = "", string nTarget = "", string nResult = "")
    {
        BattleEvent BE = new BattleEvent(nBattleEventType, nSubject, nVerb, nTarget, nResult);
        
        foreach (BattleEventListener listener in listeners) { 
            listener.hearEvent(BE);
        }
        eventLog.Add(BE);
        appendText(BE);
    }

    public void appendText(BattleEvent BE)
    {
        listText.text += "\n \n" + BE.ToString();
    }

    public void addListener(BattleEventListener nListener)
    {
        listeners.Add(nListener);
    }

    //###Initializer### 
    public void initialze()
    {
        listeners = new List<BattleEventListener>();
        eventLog = new List<BattleEvent>();
        listText = GetComponentInChildren<Text>();
    }
}   