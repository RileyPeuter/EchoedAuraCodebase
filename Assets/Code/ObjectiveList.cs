using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;


public enum ObjectiveModifier
{
    None, 
    GreaterThan
}

public class Objective
{
    //###MemberVariables###
    protected ObjectiveModifier modifier = ObjectiveModifier.None;
    protected int modifierThreshold;

    public string objectiveID;
    protected string description;
    protected string stringData;
    protected int maxCompletions;
    protected int currentCompletions = 0;

    protected BattleEventType objectiveType;

    protected string subject = null;
    protected string verb = null;
    protected string target = null;
    protected string result = null;

    //Non Visible objectives are used for triggers
    public bool visible = true;

    public bool completed = false;

    GameObject parent;
    Text descriptionText;
    Text completedText;
    protected float yOffset;

    //###Utilities###
    public virtual bool checkObjective(BattleEventType nBattleEventType, string nSubject = null, string nVerb = null, string nTarget = null, string nResult = null)
    {

        //This block just checks if the event checks if something is equal to the objecitve
        if(objectiveType != nBattleEventType){return false;}
        if(subject != null && subject != nSubject){return false;}
        if(verb != null && verb != nVerb){return false;}
        if(target != null && target != nTarget) {return false;}
        if(result != null && result != nResult){return false;}
             
        if (testModifier(modifier, nResult, modifierThreshold))
        {
            return true;
        }
             
        return false;
    }


    //Added this to make it cleaner elsewhere
    public bool checkObjective(BattleEvent nBattleEvent)
    {
        return checkObjective(nBattleEvent.eventType, nBattleEvent.subject, nBattleEvent.verb, nBattleEvent.target, nBattleEvent.result);
    }

    //Checks modifiers like "Deal MORE than 6 damage"
    public bool testModifier(ObjectiveModifier objectiveModifier, string result, int threshHold)
    {
        switch (objectiveModifier)
        {
            case ObjectiveModifier.None:
                return true;

            case ObjectiveModifier.GreaterThan:
                if(int.Parse(result) >= threshHold){

                    return true;
                }
                break;
        }
        return false;
    }

    void checkVisible()
    {
        parent.SetActive(visible);
    }

    //###Setters###
    //These are writen like this so you can easily chain them together
    public Objective makeInvisible()
    {
        visible = false;
        return this;

    }

    public Objective addSubject(string nSubject)
    {
        subject = nSubject;
        return this;
    }

    public Objective addVerb(string nVerb)
    {
        verb = nVerb;
        return this;
    }
    public Objective addTarget(string nTarget)
    {
        target = nTarget;
        return this;
    }
    public Objective addReuslt(string nResult)
    {
        result = nResult;
        return this;
    }

    public Objective addParent(GameObject nParent)
    {
        parent = nParent;
        return this;
    }
    public Objective addOffset(float nOffset)
    {
        yOffset = nOffset;
        return this;
    }

    public Objective addDescription(string nDescription)
    {
        description = nDescription;
        return this;
    }

    public Objective addModifier(ObjectiveModifier nObjectiveModifer, int nThreshold)
    {
        modifier = nObjectiveModifer;
        modifierThreshold = nThreshold; 
        return this;
    }

    //###Utilities###
    public void setInfo(float offset)
    {
        if (!visible) { return;}
        parent.transform.Translate(0, offset, 0);
        foreach (Text text in parent.GetComponentsInChildren<Text>())
        {
            if (text.gameObject.name == "uI_ObjectiveDescription_Text")
            {
                descriptionText = text;
            }
            if (text.gameObject.name == "uI_ObjectiveAmount_Text")
            {
                completedText = text;
            }
        }
    }

    public void setText()
    {
        if (!visible) {
            //completedText.gameObject.SetActive()
            return; }

        descriptionText.text = description;
        completedText.text = currentCompletions.ToString() + "/" + maxCompletions.ToString();
        if (currentCompletions >= maxCompletions)
        {
            descriptionText.color = Color.green;
            completedText.color = Color.green;
        }
    }

    //Used for when you get closer to an objective, returns true if it's completed
    public bool increment(int amount = 1)
    {
        currentCompletions += amount;
        return (currentCompletions >= maxCompletions);
    }

    //###Constructors###
    public Objective(string nID, int nMaxCompletion, BattleEventType nObjectiveType)
    {
        objectiveID = nID;
        maxCompletions = nMaxCompletion;
        objectiveType = nObjectiveType;
    }

    public Objective(string nObjectiveID, string nDescription, int nMaxCompletion, BattleEventType nObjectiveType, GameObject nParent, float offset, int nCurrentCompletion = 0)
    {
        objectiveID = nObjectiveID;
        description = nDescription;
        maxCompletions = nMaxCompletion;       
        objectiveType = nObjectiveType;
        currentCompletions = nCurrentCompletion;
        parent = nParent;

        setInfo(offset);
        setText();
    }

    public GameObject getParent()
    {
        return parent;
    }
}

public class ObjectiveList : MonoBehaviour, BattleEventListener
{
    //###MemberVariables###
    List<Objective> objectives;
    GameObject objectiveGameObject;
    GameObject listObject;
    BattleController BC;

    float yOffset = 0;

    //###Utilities###

    public void removeObjective(string id)
    {
        Objective toRemove = objectives.Find(x => x.objectiveID == id);
        Destroy(toRemove.getParent());

        objectives.Remove(toRemove);


    }

    public void addObjective(Objective nObjectiveListElement)
    {
        objectives.Add(nObjectiveListElement);

        if (nObjectiveListElement.visible)
        {
            nObjectiveListElement.addParent(GameObject.Instantiate(objectiveGameObject, this.gameObject.transform)).addOffset(yOffset);
            nObjectiveListElement.setInfo(yOffset);
            nObjectiveListElement.setText();

            yOffset = yOffset - 80;
        }
    }

    public void updateList()
    {
        foreach(Objective objective in objectives)
        {
            objective.setText();
        }
    }

    public void hearEvent(BattleEvent nBattleEvent)
    {
        foreach (Objective objective in objectives.ToArray())
        {
            if (objective.checkObjective(nBattleEvent.eventType, nBattleEvent.subject, nBattleEvent.verb, nBattleEvent.target, nBattleEvent.result))
            {
                if (objective.increment() && !objective.completed)
                {
                    objective.completed = true;
                    BC.objectiveComplete(objective.objectiveID);
                }
                updateList();
            }
        }
    }

    //###Initializer###
    public void initialize(BattleController BatCont)
    {
        objectives = new List<Objective>();
        BC = BatCont;
        objectiveGameObject = Resources.Load<GameObject>("UIElements/uI_Objective_Parent");
    }

}