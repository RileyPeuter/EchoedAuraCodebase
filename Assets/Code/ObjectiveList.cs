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
    ObjectiveModifier modifier = ObjectiveModifier.None;
    int modifierThreshold;

    public string objectiveID;
    string description;
    string stringData;
    int maxCompletions;
    int currentCompletions = 0;

    BattleEventType objectiveType;
    
    string subject = null;
    string verb = null;
    string target = null;
    string result = null;

    //Non Visible objectives are used for triggers
    bool visible = true;

    public bool completed = false;

    GameObject parent;
    Text descriptionText;
    Text completedText;
    float yOffset;

    //###Utilities###
    public bool checkObjective(BattleEventType nBattleEventType, string nSubject = null, string nVerb = null, string nTarget = null, string nResult = null)
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
             
        return true;
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
        if (!visible) { return; }

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
    public void addObjective(Objective nObjectiveListElement)
    {
        nObjectiveListElement.addParent(GameObject.Instantiate(objectiveGameObject, this.gameObject.transform)).addOffset(yOffset);
        objectives.Add(nObjectiveListElement);
        nObjectiveListElement.setInfo(yOffset);
        nObjectiveListElement.setText();


        yOffset = yOffset - 80;
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
        Debug.Log("kam");
        foreach (Objective objective in objectives.ToArray())
        {
            Debug.Log("sam");
            if (objective.checkObjective(nBattleEvent.eventType, nBattleEvent.subject, nBattleEvent.verb, nBattleEvent.target, nBattleEvent.result))
            {
                Debug.Log("fam");
                if (objective.increment() && !objective.completed)
                {
                    Debug.Log("dam");
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