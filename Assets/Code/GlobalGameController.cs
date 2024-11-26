using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class GlobalGameController : MonoBehaviour
{
    public static GlobalGameController GGC;

    //###MemberVariables###
    public ExWhy mapToBeLoaded; 
    public int  BCToBeLoaded;
    AudioSource musicPlayer;
    static AudioSource ticker;
    static AudioClip tickSound;
    public int CutsceneToBeLoaded;
    Agency activeAgency;

    List<StoredCharacterObject> charactersToBeLoaded;

    float timer = 0f;
    bool newGameFlag = false;

    public List<Mission> completed;
    public List<Mission> available;
    public Mission activeMission = null;

    GameObject escapeMenuPrefab;

    GameObject openEscapeMenu;

    //###Getters###
    public Agency getAgency()
    {
        return activeAgency;
    }


    public List<Mission> getAvailableMissions()
    {
        return available;
    }

    public List<StoredCharacterObject> getMissionCharacters()
    {
        return charactersToBeLoaded;
    }
    public static void tick()
    {
        ticker.Play();
    }

    public void completeMission()
    {
        completed.Add(activeMission);
        available.Remove(activeMission);
    }

    //###Setters
    public void setCharactersToBeLoaded(List<StoredCharacterObject> nCharactersToBeLoaded)
    {
        charactersToBeLoaded = nCharactersToBeLoaded;
    }   

    //###UnityMessages###
    void Start()
    {
        escapeMenuPrefab = Resources.Load<GameObject>("UIElements/uI_EscMenu_Panel");

        activeAgency = new Agency();

        completed = new List<Mission>();
        available = new List<Mission>();

        available.Add(new Mission("Kim's Training", "Assist the hand to hand specialist with his training", "Kimmi", MissionType.Character, 4, -217f, 89f, new ExWhyTrainingGround(), 0, true)); 
        available.Add(new Mission("Ruless' Standoff", "The swordswoman is beset by a group of beasts. Help her", "Ruless", MissionType.Character, 3, 165F, -94F, new ExWhyRulessDog(), 2));
        available.Add(new Mission("Dorcia Retrival", "A girl in a suit of armour is stranded", "Scarci", MissionType.Character, 5, 264F, 13.3F, new ExWhyForestRoad(), 3));
        available.Add(new Mission("First Contracted Mission", "End the day and recieve your first Contracted mission", "Agency", MissionType.Milestone, 6, -258.4F, -40F, new ExWhySiege1(), 4));
        available.Add(new Mission("Test", "Test", "Agency", MissionType.Milestone, 7, -258.4F, 40F, new ExWhySiege2(), 5));
        available.Add(new Mission("Testacles", "Testacles", "Agency", MissionType.Milestone, 8, -258.4F, -80F, new ExWhySiege3()));


        foreach (AudioSource AuSo in GetComponentsInChildren<AudioSource>())
        {
            if(AuSo.gameObject.name == "Ticker")
            {
                ticker = AuSo;
            }
        }

        tickSound = Resources.Load<AudioClip>("Audio/SoundEffects/tick");
        ticker.clip = tickSound;
        GlobalGameController.GGC = this;
        DontDestroyOnLoad(this.gameObject);
        musicPlayer = GetComponent<AudioSource>();   
    }

    public void Update() 
    {
        escapeMenuCheck();
        //Please, for the love of god, move these away from something that won't run literally every frame of the game
        if(newGameFlag)
        {
            GameObject.Find("asaMainMenu").GetComponent<SpriteRenderer>().color += new Color(0, 0, 0, -1 * Time.deltaTime);

            GameObject.Find("iradenMainMenu").GetComponent<SpriteRenderer>().color += new Color(0, 0, 0, -1 * Time.deltaTime);
            timer = timer - Time.deltaTime;
            if(timer < 0)
            {
                newGameFlag = false;
                newGame();
            }
        }
    }

    public void escapeMenuCheck(bool bypassFlag = false)
    {

        if (Input.GetKeyDown(KeyCode.Escape) || bypassFlag)
        {
            if (openEscapeMenu == null)
            {
                openEscapeMenu = Instantiate(escapeMenuPrefab, GameObject.Find("Canvas").transform);
            }
            else
            {
                Destroy(openEscapeMenu);
                openEscapeMenu = null;
            }
        }

    }

    public void playMusic(AudioClip AC)
    {
        musicPlayer.clip = AC;
        musicPlayer.Play();
    }


    public void test()
    {
        openManagment();
    }

    public void openExcapeMenu()
    {
        
    }

    public void newGameButton()
    {

        newGameFlag = true;
        timer = 0.5f;
    }

    public void newGame()
    {
        CutsceneToBeLoaded = 1;
        startCutscene();
    }

    public void startBattle()
    {
        mapToBeLoaded.initialize();
        SceneManager.LoadScene(2, LoadSceneMode.Single);
        
    }

    public void startCutscene()
    {
        SceneManager.LoadScene(3, LoadSceneMode.Single);
    }

    public void openManagment()
    {
        SceneManager.LoadScene(4, LoadSceneMode.Single);
    }


    public void openMainMenu(bool reset = false)
    {
        if (reset) { activeAgency = new Agency(); }
        SceneManager.LoadScene(0);
    }


}
