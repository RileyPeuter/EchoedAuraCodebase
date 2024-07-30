using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public enum ObjectiveType{
    Attack, Hit, Kill, Occupy, Interact, Time
    }

public enum ObjectiveModifier
{
    None, 
    GreaterThan
}

public class ObjectiveListElement
{
    //Broooo, why did i do this like this
    ObjectiveModifier modifier = ObjectiveModifier.None;
    int modifierThreshold;

    ObjectiveType objectiveType;
    public string objectiveID;
    public string description;
    public string stringData;
    public int maxCompletions;
    public int currentCompletions = 0;

    string subject = null;
    string eventType = null;
    string verb = null;
    string target = null;
    string result = null;

    bool visible = true;

    public bool completed = false;

    GameObject parent;
    Text descriptionText;
    Text completedText;
    float yOffset;

    public bool checkObjective(string ET, string S = null, string V = null, string T = null, string R = null)
    {
        
        //This is gonna be a horrible stack of if nesting, but I think it's the best way to go
        //^^^Don't listen to the other guy, probably just need to use interafces or inheritance or some shit
        if (subject == null || subject == S)
        {
           if (eventType == ET)
            {
                if (verb == null || V == verb)
                {
                    if (target == null || target == T) {
                           if(result == null || result  == R)
                        {
                           if (testModifier(modifier, R, modifierThreshold))
                              {
                                return true;
                          }
                        }
                    }
                }
            }
        }
        return false;
    }

    //This is going to be very Wack. Might be better if we use templates, but fuck that for now
    public bool testModifier(ObjectiveModifier OM, string result, int threshHold)
    {
        switch (OM)
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

    public ObjectiveListElement(string nID, int nMaxCompletion, ObjectiveType nObjectiveType)
    {
        objectiveID = nID;
        maxCompletions = nMaxCompletion;
        objectiveType = nObjectiveType;

        
        //setInfo(yOffset);
        //setText();
    }

    //These all return themselves so that you can optionally chain the methods together (look at how fancy I am)
    public ObjectiveListElement addSubject(string nSubject)
    {
        subject = nSubject;
        return this;
    }
    public ObjectiveListElement addEventType(string nEventType)
    {
        eventType = nEventType;
        return this;
    }
    public ObjectiveListElement addVerb(string nVerb)
    {
        verb = nVerb;
        return this;
    }
    public ObjectiveListElement addTarget(string nTarget)
    {
        target = nTarget;
        return this;
    }

    public ObjectiveListElement addParent(GameObject nParent)
    {
        parent = nParent;
        return this;
    }
    public ObjectiveListElement addOffset(float nOffset)
    {
        yOffset = nOffset;
        return this;
    }

    public ObjectiveListElement addDescription(string nDescription)
    {
        description = nDescription;
        return this;
    }

    public ObjectiveListElement(string id, string dsc, int maxComp, ObjectiveType ot, GameObject prnt, float offset, string sub = null, string evetyp = null,string ver = null, string tar = null, string res = null, int currentComp = 0, ObjectiveModifier OM = ObjectiveModifier.None, int modThresHold = 0)
    {
        subject = sub;
        eventType = evetyp;
        verb = ver;
        target = tar;
        result = res;

        modifier = OM;
        modifierThreshold = modThresHold;

        objectiveID = id;
        description = dsc;
        maxCompletions = maxComp;       
        objectiveType = ot;
        currentCompletions = currentComp;
        parent = prnt;

        setInfo(offset);
        setText();
    }

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
        descriptionText.text = description;
        completedText.text = currentCompletions.ToString() + "/" + maxCompletions.ToString();
        if(currentCompletions >= maxCompletions)
        {
            descriptionText.color = Color.green;
            completedText.color = Color.green;
        }
    }

    public bool increment(int amount = 1)
    {
        currentCompletions += amount;
        return (currentCompletions >= maxCompletions);
    }
}








public class ObjectiveList : MonoBehaviour, BattleEventListener
{

    float yOffset = 0;

    //This constructor turned into something horrible and unweirdly. So i added one below, that's a little more usable. 
    public void addObjective(string id, string dsc, int maxComp, ObjectiveType ot, string sub,string eveTyp , string verb, string target, string result, int curComp = 0, ObjectiveModifier OM = ObjectiveModifier.None, int modThresh = 0)
    {
        ObjectiveListElement newOLE = new ObjectiveListElement(id, dsc, maxComp, ot, GameObject.Instantiate(objectiveGameObject, this.gameObject.transform), yOffset, sub, eveTyp, verb, target, result, curComp, OM, modThresh);
        objectives.Add(newOLE);
        yOffset = yOffset - 80;
    }

    public void addObjective(ObjectiveListElement OLE)
    {
        OLE.addParent(GameObject.Instantiate(objectiveGameObject, this.gameObject.transform)).addOffset(yOffset);
        objectives.Add(OLE);
        OLE.setInfo(yOffset);
        OLE.setText();


        yOffset = yOffset - 80;
    }

    public void updateList()
    {
        
        foreach(ObjectiveListElement objective in objectives)
        {
            objective.setText();
        }
    }

    public void displayList()
    {
        foreach(ObjectiveListElement objective in objectives)
        {

            
        }
    }

    List<ObjectiveListElement> objectives;
    GameObject objectiveGameObject;
    GameObject listObject;
    BattleController BC;


    public void initialize(BattleController BatCont) {
        objectives = new List<ObjectiveListElement>();
        BC = BatCont;
        objectiveGameObject = Resources.Load<GameObject>("UIElements/uI_Objective_Parent");

    }

    public void hearEvent(BattleEvent nBattleEvent)
    {
        foreach (ObjectiveListElement OLE in objectives.ToArray())
        {
            if (OLE.checkObjective(nBattleEvent.eventType, nBattleEvent.subject, nBattleEvent.verb, nBattleEvent.target, nBattleEvent.result))
            {
                if (OLE.increment() && !OLE.completed)
                {
                    OLE.completed = true;
                    BC.objectiveComplete(OLE.objectiveID);
                }
                updateList();
            }
        }
    }
}

/*
 * 
 * 
 * 
//Dump for old code taht may still be needed
 



    public class AttackObjective : ObjectiveListElement
{
    public BattleCharacterObject target;
    public AttackObjective(BattleCharacterObject BCO, string id, string dsc, int maxComp, ObjectiveType ot, GameObject prnt, float offset, int currentComp = 0) : base(id, dsc, maxComp, ot, prnt, offset, currentComp)
    {
        target = BCO;
    }
}

public class OccupyObjective : ObjectiveListElement
{
    public OccupyObjective(string id, string dsc, int maxComp, ObjectiveType ot, GameObject prnt, float offset, int currentComp = 0) : base(id, dsc, maxComp, ot, prnt, offset, currentComp)
    {

    }
}

public class HitObjetive: ObjectiveListElement
{
    public BattleCharacterObject target;
    public HitObjetive(BattleCharacterObject BCO, string id, string dsc, int maxComp, ObjectiveType ot, GameObject prnt, float offset, int currentComp = 0) : base(id, dsc, maxComp, ot, prnt, offset, currentComp)
    {
        target = BCO;
    }
}

public class InteractObjective : ObjectiveListElement
{
    public InteractObjective(string id, string dsc, int maxComp, ObjectiveType ot, GameObject prnt, float offset, int currentComp = 0) : base(id, dsc, maxComp, ot, prnt, offset, currentComp)
    {

    }
}

public class TimeObjective : ObjectiveListElement
{
    public TimeObjective(string id, string dsc, int maxComp, ObjectiveType ot, GameObject prnt, float offset, int currentComp = 0) : base(id, dsc, maxComp, ot, prnt, offset, currentComp)
    {
       
    }
}

public class KillObjective : ObjectiveListElement
{
    public BattleCharacterObject target;
    public KillObjective(BattleCharacterObject BCO, string id, string dsc, int maxComp, ObjectiveType ot, GameObject prnt, float offset, int currentComp = 0) : base(id, dsc, maxComp, ot, prnt, offset, currentComp)
    {
        target = BCO;
    }
}
     * 
     * //These should be a better way of doing this. Maybe just bite the bullet and add another abstract class
    public void checkKillObjectives(BattleCharacterObject BCO)
    {
        foreach(KillObjective objective in objectives)
        {
            if(objective.target == BCO)
            {
                if (objective.increment())
                {
                    BC.objectiveComplete(objective.objectiveID);

                }
                objective.setText();
            }
        }
    }

    public void checkAttackObjectives(BattleCharacterObject BCO) {
        foreach (AttackObjective objective in objectives.ToArray())
        {
            if (objective.target == BCO)
            {
                if (objective.increment())
                {
                    BC.objectiveComplete(objective.objectiveID);
                }
                objective.setText();

            }
        }
    }

    public void checkHitObjectives(BattleCharacterObject BCO)
    {
        foreach (HitObjetive objective in objectives)
        {
            if (objective.target == BCO)
            {
                objective.increment();
                objective.setText();
            }
        }
    }

    public void addKillObjectives(BattleCharacterObject BCO, string id, string dsc, int maxComp, ObjectiveType ot, int currentComp = 0)
    {
        foreach(ObjectiveListElement OLE in objectives){if (OLE.objectiveID == id) { return; }}

        objectives.Add(new KillObjective(BCO, id, dsc, maxComp, ot, GameObject.Instantiate(objectiveGameObject, this.gameObject.transform), yOffset));
        yOffset = yOffset - 70;
    }

    public void addAttackObjectives(BattleCharacterObject BCO, string id, string dsc, int maxComp, ObjectiveType ot, int currentComp = 0)
    {
        foreach (ObjectiveListElement OLE in objectives) { if (OLE.objectiveID == id) { return; } }

        objectives.Add(new AttackObjective(BCO, id, dsc, maxComp, ot, GameObject.Instantiate(objectiveGameObject, this.gameObject.transform), yOffset));
        yOffset = yOffset - 70;
    }

    public void addOccupyObjectives(BattleCharacterObject BCO, string id, string dsc, int maxComp, ObjectiveType ot, int currentComp = 0)
    {
        foreach (ObjectiveListElement OLE in objectives) { if (OLE.objectiveID == id) { return; } }

        objectives.Add(new OccupyObjective(id, dsc, maxComp, ot, GameObject.Instantiate(objectiveGameObject, this.gameObject.transform), yOffset));
        yOffset = yOffset - 70;
    }

    public void addHitObjective(BattleCharacterObject BCO, string id, string dsc, int maxComp, ObjectiveType ot, int currentComp = 0)
    {
        foreach (ObjectiveListElement OLE in objectives) { if (OLE.objectiveID == id) { return; } }

        objectives.Add(new HitObjetive(BCO, id, dsc, maxComp, ot, GameObject.Instantiate(objectiveGameObject, this.gameObject.transform), yOffset));
        yOffset = yOffset - 70;
    }

    public void addTimeObjective(BattleCharacterObject BCO, string id, string dsc, int maxComp, ObjectiveType ot, int currentComp = 0)
    {
        foreach (ObjectiveListElement OLE in objectives) { if (OLE.objectiveID == id) { return; } }

        objectives.Add(new TimeObjective(id, dsc, maxComp, ot, GameObject.Instantiate(objectiveGameObject, this.gameObject.transform), yOffset));
        yOffset = yOffset - 70;
    }


    public void addInteratObjective(string id, string dsc, int maxComp, ObjectiveType ot, int currentComp = 0)
    {
        foreach (ObjectiveListElement OLE in objectives) { if (OLE.objectiveID == id) { return; } }

        objectives.Add(new InteractObjective(id, dsc, maxComp, ot, GameObject.Instantiate(objectiveGameObject, this.gameObject.transform), yOffset));
        yOffset = yOffset - 70;
    }

    
     * 
     * 
     * 
     * 
     * 
     * * 
     * /

    */

    


