using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PostPrologueCutscene : Cutscene
{
    CutsceneActorController asa;
    CutsceneActorController iraden;

    GameObject currentRoom;

    GameObject fader;
    public override void endCutscene()
    {
        GlobalGameController.GGC.startBattle();
    }

    public override void executeTrigger(int triggerIndex)
    {
        switch (triggerIndex)
        {
            //Iraden Run
            case 1:
                iraden.SetMove(Vector2.right, 6, 1);
                phaseCutscene(3);
                break;

            //Asa explode
            case 2:
                asa.SetMove(Vector2.left, 2, 1, false);
                asa.setOverrideAnimation(1);
                Destroy(asa.gameObject, 1);
                phaseCutscene(2);
                break;


            //Camera Pan
            case 3:
                phaseCutscene(1);
                panCamera(new Vector2(-8, 0));
                asa = spawnActor("Asa", new Vector3(-16, -2));
                break;


            //Iraden Run off
            case 4:
                iraden.SetMove(Vector2.right, 8, 2);
                phaseCutscene(2);
                fader = fadeout(Color.clear, Color.black);
                break;


            //Change scene
            case 5:
                Destroy(fader);
                Destroy(currentRoom);
                Destroy(iraden.gameObject);
                Destroy(asa.gameObject);

                iraden = spawnActor("Iraden", new Vector3(-2.7f, -1.4f));
                panCamera(new Vector2(8, 0));   
                currentRoom = Instantiate(Resources.Load<GameObject>("CutsceneAssets/CastleRooms1"));
                phaseCutscene(2);
                break;

            case 6:
                phaseCutscene(2);
                iraden.SetMove(Vector2.right, 4, 1);
                fader = fadeout(Color.clear, Color.black);
                break;

            case 7:
                Destroy(currentRoom);
                Destroy(iraden.gameObject);
                Destroy(fader);
                fader = fadeout(Color.black, new Color(-1,-1,-1,-1), 0.4f);

                phaseCutscene(3);
                currentRoom = Instantiate(Resources.Load<GameObject>("CutsceneAssets/CastleRoomsMourning"));

                break;

            //Change scene
            case 8:
                Destroy(currentRoom);
                iraden = spawnActor("Iraden", new Vector3(-4.4f, -0.7f));
                iraden.SetMove(Vector2.right, 1, 0.5f);
                currentRoom = Instantiate(Resources.Load<GameObject>("CutsceneAssets/CastleRooms2"));
                phaseCutscene(2);
                break;

        }

    }

    void Start()
    {
        frames = new List<CutsceneFrame>();
        speakerSprites = new Dictionary<string, Sprite>();

        resourceString = "PostPrelude";

        CutsceneLoader.loadCutscene(this);

        bigTextBox = GameObject.Instantiate(Resources.Load<GameObject>("UIElements/uI_Dialogue_GameObject"));

        spawnVisuals();

        asa = GameObject.Find("Asa(Clone)").GetComponent<CutsceneActorController>();
        iraden = GameObject.Find("Iraden(Clone)").GetComponent<CutsceneActorController>();
        asa.gameObject.GetComponent<SpriteRenderer>().flipX = true; 
    }
    void Update()
    {
        if (phaseTime > 0)
        {
            testPhase();
            return;
        }

        buttonCheck();
    }

    public override void spawnVisuals()
    {
        currentRoom = Instantiate(Resources.Load<GameObject>("CutsceneAssets/CastleInterior"));
        Instantiate(Resources.Load<GameObject>("CutsceneActors/Iraden")).transform.Translate(new Vector3(-8,-3));
        Instantiate(Resources.Load<GameObject>("CutsceneActors/Asa")).transform.Translate(new Vector3(6, -1.4f));

    }

}
