using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroCutscene : Cutscene
{

    public override void spawnVisuals()
    {
        GameObject.Instantiate(Resources.Load<GameObject>("CutsceneAssets/CutsceneVisualObjectIntro"));
    }

    // Start is called before the first frame update
    void Start()
    {
        frames = new List<CutsceneFrame>();
        speakerSprites = new Dictionary<string, Sprite>();

        resourceString = "Intro";
        CutsceneLoader.loadCutscene(this);
        GlobalGameController.GGC.playMusic(Resources.Load<AudioClip>("Audio/Music/NomadOprhan"));

        /*
        speakerSprites.Add("Iraden", Resources.Load<Sprite>("CutsceneSpeakerSprites/Iraden"));
        speakerSprites.Add("Jayf", Resources.Load<Sprite>("CutsceneSpeakerSprites/Jayf"));
        
        frames.Add(new CutsceneFrame("", "In a delapitated castle, in the quite country side, hosts \n a small band of specialized mercenaries"));


        frames.Add(new CutsceneFrame("Jayf", "Hmm? Who might you be?", getSprite("Jayf"), 1));

        frames.Add(new CutsceneFrame("Iraden", "I heard there was a...a vacancy of those who are...uhh \n Inclined towards that of military academia...ahh \n via magical tal...", getSprite("Iraden")));
        frames.Add(new CutsceneFrame("Jayf", "That's an impressive amount of big words, but perhaps try speaking plainly. \n Who are you?", getSprite("Jayf")));
        frames.Add(new CutsceneFrame("Iraden", "I want to enrol", getSprite("Iraden")));
        frames.Add(new CutsceneFrame("Jayf", "That's obvious, hence my question. Who are you? \n We aren't exactly advertising for new people.", getSprite("Jayf")));
 //       frames.Add(new CutsceneFrame("Iraden", "Iraden. Son of a Sergeant Greatswordsman on the Nomad Vanguard.", getSprite("Iraden")));
 //       frames.Add(new CutsceneFrame("Jayf", "'Son'? 'Orphan' you mean? Or I imagine you would be going elsewhere", getSprite("Jayf")));
//        frames.Add(new CutsceneFrame("Iraden", "Yes, since I was young. I had his sword reforged into a short sword.", getSprite("Iraden")));
//        frames.Add(new CutsceneFrame("Jayf", "More of a dagger than a sword. And we're not looking for \n any street rat that can stab something.", getSprite("Jayf")));
        frames.Add(new CutsceneFrame("Jayf", "We're hiring people of more peculiar, specialised and specific skills. \n Things that can't be gotten from anywhere else.", getSprite("Jayf")));
        frames.Add(new CutsceneFrame("Iraden", "I'm quiet . And I studied mana weaving.", getSprite("Iraden")));
        frames.Add(new CutsceneFrame("Jayf", "At what academy?", getSprite("Jayf")));
        frames.Add(new CutsceneFrame("Iraden", "I didn't need one. I bought books with what my father left me.", getSprite("Iraden")));
        frames.Add(new CutsceneFrame("Jayf", "'Self Taught' mages tend to be danger to themselves and those around", getSprite("Jayf")));
        frames.Add(new CutsceneFrame("Iraden", "I made the Cerilin ice saw lighter, so that it can be thrown.\n And I can do it really quickly", getSprite("Iraden")));
        frames.Add(new CutsceneFrame("Jayf", "hmm. Learning that by books alone would be an achievement. \n A mouse scurrying through the cracks, sticking people \n with ice could be useful to us.", getSprite("Jayf")));
        frames.Add(new CutsceneFrame("Jayf", "If what you say is true...", getSprite("Jayf")));
        frames.Add(new CutsceneFrame("Jayf", "Walk with me to the training ground and show me what you can do.", getSprite("Jayf")));
        */

        bigTextBox = GameObject.Instantiate(Resources.Load<GameObject>("UIElements/uI_Dialogue_GameObject"));

        spawnVisuals();
    }


    void Update()
    {
        if(phaseTime > 0)
        {
            testPhase();
            return;
        }

        buttonCheck();
    }

    

    public override void endCutscene()
    {
        GlobalGameController.GGC.mapToBeLoaded = new ExWhyTrainingGround();

        GlobalGameController.GGC.BCToBeLoaded = 0;
        GlobalGameController.GGC.startBattle();
    }

    public override void executeTrigger(int triggerIndex)
    {
        switch (triggerIndex)
        {
            case 1:
                GameObject.Find("iraden").GetComponent<CutsceneActorController>().SetMove(Vector2.left, 10, 1);
                phaseCutscene(3);
                break;
        }


    }
}
