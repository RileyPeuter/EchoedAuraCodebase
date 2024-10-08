using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public enum StandOffStage
{
    OpenState,
    Attack,
    AttackSuccessful,
    EndAttack,
    Movement,
    SoloCast, 
    EmptyState, 
    WindDown
    
}

public abstract class StandOffController : MonoBehaviour
{
    protected Ability ability;

    protected float timer = 0;
    protected float messageTimer = 0;
    protected StandOffStage SOS;
    protected string resourceName;
    protected bool isCutscene = false;
    Sprite mapBackground;
    protected UIController BUIC;
    Text rollText;
    protected Text damageText;
    protected GameObject rollPanel;
    protected GameObject backPanel;
    protected AudioSource AS;

    protected int sideFinished = 0;

    protected BattleEventLog BEL;

    protected bool left = false;
    protected bool right = false;
    protected void initialize()
    {
        AS = GetComponent<AudioSource>();

        this.transform.position = new Vector2(GameObject.Find("Main Camera").transform.position.x, GameObject.Find("Main Camera").transform.position.y + 5);
    }

    public void animationFinished(bool side) {
        if (side && !left)
        {
            sideFinished++;
            left = true;
        }
        if(!side && !right)
        {
            sideFinished++;
            right = true;
        }

    }

    public abstract void standOffUpdate();

    protected void resetAnimationListeners()
    {
        sideFinished = 0;
        left = false;
        right = false;
    }

    public void playSound(AudioClip sound)
    {
        AS.clip = sound;
        AS.Play();
    }

    protected void spawnAbilitySnippet(string parentName)
    {
        GameObject GO =  BUIC.openWindow("uI_Ability_Panel", true, parentName, false);
        
        GO.GetComponent<RectTransform>().anchorMax = new Vector2(0.5f, 0.5f);
        GO.GetComponent<RectTransform>().anchorMin = new Vector2(0.5f, 0.5f);
        
        GO.GetComponent<AbilitySnippetController>().initialize(UIController.HighestWindow, ability);
        GO.GetComponent<RectTransform>().anchoredPosition = new Vector2(-400, 200);
    }
    
    public void setResourceName(string recName)
    {
        resourceName = recName;
        print(resourceName);
    }

    //We probably need to change this
    protected abstract void cast();

    protected virtual bool checkMessageInQueue() { return false; }

    protected void end()
    {
        if (checkMessageInQueue())
        {
            return;
        }

        Destroy(rollPanel, 0.8f);
        Destroy(this.gameObject, 0.8f);
        GameObject.Find("MapController").GetComponent<BattleController>().finishMove(1);
        SOS = StandOffStage.EmptyState;
        
    }
}