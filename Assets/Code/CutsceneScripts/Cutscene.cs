using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

    protected List<Vector2> actorSpawnLocations;

    public string resourceString;

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

    protected List<CutsceneActorController> actorControllers;

    //###Utilities###
    public void phaseCutscene(float time)
    {
        bigTextBox.SetActive(false);
        phaseTime = time;

    }

    public GameObject fadeout(Color initial, Color transition, float rate = 1)
    {
        GameObject GO = Instantiate(ResourceLoader.loadGameObject("Fader"));
        GO.GetComponent<FaderController>().initialize(initial, transition, rate);
        return GO;
    }

    public void zoomCamera(float zoom)
    {
        Camera.main.orthographicSize = Camera.main.orthographicSize + zoom;

    }

    public void panCamera(Vector2 translation)
    {
        Camera.main.transform.Translate(translation);
        bigTextBox.transform.position = Camera.main.transform.position + new Vector3(0, -2, 1);

    }

    public void testPhase()
    {
        phaseTime = phaseTime - Time.deltaTime;
        if(phaseTime <= 0)
        {
            bigTextBox.SetActive(true);
        }
    }

    public bool containsSprite(string nSpriteName)
    {
        if (speakerSprites.Keys.Contains(nSpriteName)) { return true;}
            return false;
    }

    public void loadSpritesFromStoredCharacters(List<StoredCharacterObject> nSCOs = null)
    {
        //List<Sprite> output = new List<Sprite>();

        List<StoredCharacterObject> SCOs = nSCOs;
        if(SCOs == null)
        {
            SCOs = GlobalGameController.GGC.getMissionCharacters();
        }

        foreach(StoredCharacterObject SCO in SCOs)
        {
            //output.Add(ResourceLoader.loadSprite("CutsceneSpeakerSprites/" + SCO.getResourceString()));
            addSprite(SCO.getCharacterName(),ResourceLoader.loadSprite("CutsceneSpeakerSprites/" + SCO.getResourceString()));
        }

//        return output;
    }

    public List<GameObject> loadActorsFromStoreCharacters(List<StoredCharacterObject> nSCOs = null)
    {
        List<GameObject> output = new List<GameObject>();

        List<StoredCharacterObject> SCOs = nSCOs;
        if (SCOs == null)
        {
            SCOs = GlobalGameController.GGC.getMissionCharacters();
        }

        foreach(StoredCharacterObject SCO in SCOs)
        {
            output.Add(ResourceLoader.loadGameObject("CutsceneActors/" +  SCO.getResourceString()));
        }
        return output;
    }

    public void addSprite(string spriteName, string location)
    {
        speakerSprites.Add(spriteName, Resources.Load<Sprite>("CutsceneSpeakerSprites/" + location));
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
        executeTrigger(frame.frameTrigger);
        bigTextBox.GetComponent<CutsceneTextBoxController>().setText(frame.speakerName, frame.speakerText, frame.talker);
    }

    public void setFrame(int index)
    {
        //executeTrigger(frames[index].frameTrigger);
        setFrame(frames[index]);
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
        actorSpawnLocations = new List<Vector2>();
        speakerSprites = new Dictionary<string, Sprite>();
        frames = new List<CutsceneFrame>();
        actorControllers = new List<CutsceneActorController>();
    }

    public virtual void spawnActors(List<GameObject> actors)
    {
        int spawnIndex = 0;
        foreach(GameObject actor in actors)
        {
            GameObject GO =  Instantiate(actor); 
            GO.transform.position = actorSpawnLocations[spawnIndex];
            actorControllers.Add(GO.GetComponent<CutsceneActorController>());
            spawnIndex++;
        }
    }

    public CutsceneActorController spawnActor(string name, Vector3 location, bool flip = false)
    {
        GameObject GO = Instantiate(Resources.Load<GameObject>("CutsceneActors/" + name));
        GO.transform.Translate(location);
        if (flip)
        {
            GO.GetComponent<SpriteRenderer>().flipX = true;
        }
        GO.GetComponent<CutsceneActorController>().initialize();
        return GO.GetComponent<CutsceneActorController>();
    }

    public void panCamera()
    {

    }

    public abstract void endCutscene();

    public virtual void spawnVisuals(){}

    public abstract void executeTrigger(int triggerIndex);
}
