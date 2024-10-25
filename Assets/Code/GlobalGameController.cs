using System.Collections;
using System.Collections.Generic;
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


    public List<Mission> completed;
    public List<Mission> available;
    public Mission activeMission = null;

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
        activeAgency = new Agency();

        completed = new List<Mission>();
        available = new List<Mission>();

        available.Add(new Mission("Kim's Training", "Assist the hand to hand specialist with his training", "Kimmi", MissionType.Character, 4, -217f, 89f, new ExWhyTrainingGround(), 0, true)); 
        available.Add(new Mission("Ruless' Standoff", "The swordswoman is beset by a group of beasts. Help her", "Ruless", MissionType.Character, 3, 165F, -94F, new ExWhyRulessDog(), 2));
        available.Add(new Mission("Dorcia Retrival", "A girl in a suit of armour is stranded", "Scarci", MissionType.Character, 5, 264F, 13.3F, new ExWhyForestRoad(), 3));
        available.Add(new Mission("First Contracted Mission", "End the day and recieve your first Contracted mission", "Agency", MissionType.Milestone, 6, -258.4F, -40F, new ExWhySiege1(), 4));
        available.Add(new Mission("Test", "Test", "Agency", MissionType.Milestone, 7, -258.4F, 40F, new ExWhySiege2()));


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

    public void playMusic(AudioClip AC)
    {
        musicPlayer.clip = AC;
        musicPlayer.Play();
    }


    public void test()
    {
        openManagment();
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



}
