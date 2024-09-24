using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverworldMapController : MonoBehaviour
{
    Agency agency;
    GlobalGameController gameController;
    GameObject missionPrefab;
    Mission selectedMission;
    GameObject characterSelect;
    MapCharacterListController characterListController;


    public void initialize(Agency ag)
    {
        agency = ag;
    }

    public void selectMission(Mission mission)
    {
        if(characterSelect != null)
        {
            GameObject.Destroy(characterSelect);
        }

        selectedMission = mission;
        characterSelect = Instantiate(Resources.Load<GameObject>("UIElements/uI_SelectCharacters_Panel"), this.transform);
        characterSelect.GetComponent<CharacterSelectController>().initialize(BattleUIController.HighestWindow);
        characterSelect.GetComponent<CharacterSelectController>().initialize(null, false);

        characterSelect.GetComponent<CharacterSelectController>().setOWMC(this);
    }

    public void resetSelection()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        missionPrefab = Resources.Load<GameObject>("UIElements/uI_Mission_Button");
        gameController = GlobalGameController.GGC;
        initialize(gameController.getAgency());
        spawnMissions();
        createCharacterList();
    }

    public void createCharacterList()
    {
        characterListController = Instantiate(Resources.Load<GameObject>("UIElements/uI_CharacterList_Panel"), this.transform).GetComponent<MapCharacterListController>();
        characterListController.initialize(BattleUIController.HighestWindow);
        characterListController.setOWMC(this);
        characterListController.addCharacters(agency.getCharacters()); 
    }

    public bool selectCharacter(StoredCharacterObject SCO)
    {
        if (characterSelect != null)
        {
            characterSelect.GetComponent<CharacterSelectController>().addCharacter(SCO);
            return true;
        }
        return false;
    }

    
    public void startMission(List<StoredCharacterObject> characters)
    {

        gameController.mapToBeLoaded = selectedMission.map;
        gameController.BCToBeLoaded = selectedMission.BCID;
        gameController.setCharactersToBeLoaded(characters);

        print(characterSelect.GetComponent<CharacterSelectController>().getCharacterCount());

        GlobalGameController.GGC.activeMission = selectedMission;

        if (selectedMission.cutsceneInt == 0)
        {
            gameController.startBattle();
        }
        else
        {
            gameController.CutsceneToBeLoaded = selectedMission.cutsceneInt;
            gameController.startCutscene();
        }
    }

    public void deselctCharacter(StoredCharacterObject characterObject)
    {
        characterListController.deselectCharacter(characterObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void tickCharacters()
    {
        foreach (StoredCharacterObject SCO in agency.getCharacters())
        {
            SCO.tick();
        }
    }

    void spawnMissions()
    {
        foreach(Mission mission in gameController.available)
        {
            GameObject MissionGO = Instantiate(missionPrefab, this.transform);
            MissionGO.GetComponent<MissionInfoController>().initialize(mission, this);
            MissionGO.GetComponent<RectTransform>().localPosition = new Vector2(mission.xMarkerPosition, mission.yMarkerPosition);
        }
    }
}
