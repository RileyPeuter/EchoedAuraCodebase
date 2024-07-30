using System.Collections;
using System.Collections.Generic;
using System.Net.WebSockets;
using UnityEngine;
using UnityEngine.UI;

public class BattleEventLog : MonoBehaviour
{
    //###MemeberVariables###   
    List<BattleEvent> eventLog;
    Text listText;

    List<BattleEventListener> listeners;

    //###Utilities###
    public void addEvent(string S = "", string ET = "", string V = "", string T = "", string R = "")
    {
        BattleEvent BE = new BattleEvent(S, ET, V, T, R);
        
        foreach (BattleEventListener listener in listeners) { 
            listener.hearEvent(BE);
        }

        eventLog.Add(BE);
        appendText(BE);
    }

    public void appendText(BattleEvent BE)
    {
        listText.text += "\n \n" + BE.toString();
    }

    public void addListener(BattleEventListener nListener)
    {
        listeners.Add(nListener);
    }

    //###Initializer###
    public void initialze()
    {
        listeners = new List<BattleEventListener>();
    }

    //###UnityMessages###
    public void Start()
    {
        eventLog = new List<BattleEvent>();
        listText = GetComponentInChildren<Text>();  
    }    
}   