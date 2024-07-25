using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MiscMenuController : MonoBehaviour
{

    BattleController BC;
    BattleUIController BUIC;

    GameObject objectiveListGameobject;
    GameObject combatLogGameobject;
    GameObject characterStatsGameobject;



    bool ObjectiveListFlag = true;
    bool combatLogFlag = true;
    bool characterStatsFlag = true;



    public void initialize(BattleUIController BU, BattleController BaCo, GameObject OLG, GameObject CLG, GameObject CSG)
    {
        BUIC = BU;
        BC = BaCo;

        objectiveListGameobject = OLG;
        combatLogGameobject = CLG;
        characterStatsGameobject = CSG;

    }

    public void setSheetCharacter(Character chara)
    {
        characterStatsGameobject.GetComponent<CharacterSheetScript>().setCharacterObject(chara);   
    }

    public void toggleObjectiveList()
    {
        ObjectiveListFlag =  !ObjectiveListFlag;
        toggleMenu(objectiveListGameobject, ObjectiveListFlag);
    }

    public void toggleCombatLog()
    {
        combatLogFlag = !combatLogFlag;
        toggleMenu(combatLogGameobject, combatLogFlag);
    }

    public void toggleCharacterStats()
    {
        characterStatsFlag = !characterStatsFlag;
        toggleMenu(characterStatsGameobject, characterStatsFlag);
    }

    // Start is called before the first frame update
    void Start()
    {

        toggleCharacterStats();
        toggleCombatLog();
    }   

    // Update is called once per frame
    void Update()
    {
        
    }


    public void toggleMenu(GameObject go, bool enable)
    {


        foreach (Text text in go.GetComponentsInChildren<Text>())
        {
            text.enabled = enable;
        }

        foreach (Image image in go.GetComponentsInChildren<Image>())
        {
            image.enabled = enable;
        }
    }
}
