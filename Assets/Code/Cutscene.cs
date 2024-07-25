using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneFrame{
    //###MemberVeriables###
    public string speakerName = "";
    public string speakerText = "...";
    public Sprite talker;
    public int frameTrigger = 0;

    //###Constructor###
    public CutsceneFrame(string nSpeakerName, string nSpeakerText, Sprite nTalker = null, int nFrameTrigger = 0)
    {
        speakerName = nSpeakerName;
        speakerText = nSpeakerText;
        talker = nTalker;
        frameTrigger = nFrameTrigger;
    }
}


public abstract class Cutscene : MonoBehaviour
{
    //###MemberVeriables###

    //Amount of time it the textbox is inactive
    protected float phaseTime;

    protected int currentFrameIndex = 0;
    protected List<CutsceneFrame> frames;
    protected GameObject GO;
    protected CutsceneFrame currentFrame;
    //These are references to objects that will display text
    TextMesh dialogueName;
    TextMesh dialogue;

    protected GameObject bigTextBox;

    protected Dictionary<string, Sprite> speakerSprites;

    //###Utilities###
    public void phaseCutscene(float time)
    {
        bigTextBox.SetActive(false);
        phaseTime = time;

    }

    public void testPhase()
    {
        phaseTime = phaseTime - Time.deltaTime;
        if(phaseTime <= 0)
        {
            bigTextBox.SetActive(true);
        }
    }

    public void addSprite(string speakerName, string location)
    {
        speakerSprites.Add(speakerName, Resources.Load<Sprite>("CutsceneSpeakerSprites/" + location));
    }

    public void addSprite(string speakerName, Sprite sprite)
    {
        speakerSprites.Add(speakerName, sprite);
    }

    public void addFrame(string speakerName, string dialogue, string spriteName, int frameTrigger = 0)
    {
        if (spriteName == null)
        {

        }
        frames.Add(new CutsceneFrame(speakerName, dialogue, getSprite(spriteName), frameTrigger));
    }

    protected Sprite getSprite(string key)
    {
        Sprite output;
        speakerSprites.TryGetValue(key, out output);

        return output;
    }

    public void buttonCheck()
    {
        if ((Input.GetKeyDown("space") || Input.GetMouseButtonDown(1)) || Input.GetKey("z"))
        {

            if (currentFrameIndex > frames.Count - 1)
            {
                endCutscene();
                return;
            }

            setFrame(frames[currentFrameIndex]);
            currentFrameIndex++;
        }
    }

    public void setFrame(CutsceneFrame frame)
    {
        print(frame.speakerName);
        bigTextBox.GetComponent<CutsceneTextBoxController>().setText(frame.speakerName, frame.speakerText, frame.talker);
        executeTrigger(frame.frameTrigger);
    }

    public void setFrame(int index)
    {
        setFrame(frames[index]);
        executeTrigger(frames[index].frameTrigger);
    }

    public void createDialogueObject()
    {
        bigTextBox = GameObject.Instantiate(Resources.Load<GameObject>("UIElements/uI_Dialogue_GameObject"));//, GameObject.Find("Main Camera").transform.position, new Quaternion());
        setDialogueLocation();
        bigTextBox.transform.localScale = bigTextBox.transform.localScale + new Vector3(3, 3, 0);
    }

    public void setDialogueLocation()
    {
        bigTextBox.transform.position = GameObject.Find("Main Camera").transform.position + new Vector3(0, -10, 1);
    }

    //###Initializer###
    public virtual void initialize()
    {
        speakerSprites = new Dictionary<string, Sprite>();
        frames = new List<CutsceneFrame>();
    }

    public abstract void endCutscene();

    public virtual void spawnVisuals(){}

    public abstract void executeTrigger(int triggerIndex);
}
