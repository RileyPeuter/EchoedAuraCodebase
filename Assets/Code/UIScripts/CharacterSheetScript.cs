using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSheetScript : Window
{

    public Character characterObj;
    // Start is called before the first frame update
    void Start()
    {
        //characterObj = character.getCharacter();
      //  inputInformation(); 
    }

    public void setCharacterObject(Character chr)
    {
        characterObj = chr;
        inputInformation(); 
    }


    void inputInformation()
    {
        GameObject.Find("uI_Name_Text").GetComponent<Text>().text = characterObj.CharacterName;
        GameObject.Find("uI_StrengthAmount_Text").GetComponent<Text>().text = characterObj.getBasicStat(stat.strength).ToString();
        GameObject.Find("uI_VitalityAmount_Text").GetComponent<Text>().text = characterObj.getBasicStat(stat.vitality).ToString();
        GameObject.Find("uI_SpeedAmount_Text").GetComponent<Text>().text = characterObj.getBasicStat(stat.speed).ToString();
        GameObject.Find("uI_PrecisionAmount_Text").GetComponent<Text>().text = characterObj.getBasicStat(stat.precision).ToString();
        GameObject.Find("uI_FocusAmount_Text").GetComponent<Text>().text = characterObj.getBasicStat(stat.focus).ToString();
        GameObject.Find("uI_IngenuityAmount_Text").GetComponent<Text>().text = characterObj.getBasicStat(stat.ingenuity).ToString();


        GameObject.Find("uI_Health_Text").GetComponent<Text>().text = characterObj.getDerivedStat(derivedStat.maxHealthPoints).ToString() + "/" + characterObj.HealthPoints;
        GameObject.Find("uI_Mana_Text").GetComponent<Text>().text = characterObj.getDerivedStat(derivedStat.maxManaPoints).ToString() + "/" + characterObj.ManaPoints;

        GameObject.Find("uI_ToHitDodge_Text").GetComponent<Text>().text = characterObj.getDerivedStat(derivedStat.dodgeTH).ToString();
        GameObject.Find("uI_ToHitBlock_Text").GetComponent<Text>().text = characterObj.getDerivedStat(derivedStat.blockTH).ToString();
        GameObject.Find("uI_ToHitParry_Text").GetComponent<Text>().text = characterObj.getDerivedStat(derivedStat.parryTH).ToString();

        GameObject.Find("uI_ToReactDodge_Text").GetComponent<Text>().text = characterObj.getDerivedStat(derivedStat.dodge).ToString();
        GameObject.Find("uI_ToReactBlock_Text").GetComponent<Text>().text = characterObj.getDerivedStat(derivedStat.block).ToString();
        GameObject.Find("uI_ToReactParry_Text").GetComponent<Text>().text = characterObj.getDerivedStat(derivedStat.parry).ToString();

        GameObject.Find("uI_MeleeBonus_Text").GetComponent<Text>().text = characterObj.getDerivedStat(derivedStat.meleeBonus).ToString();
        GameObject.Find("uI_RangedBonus_Text").GetComponent<Text>().text = characterObj.getDerivedStat(derivedStat.rangedBonus).ToString();
        GameObject.Find("uI_MagicBonus_Text").GetComponent<Text>().text = characterObj.getDerivedStat(derivedStat.magicBonus).ToString();

        GameObject.Find("uI_TurnFrequency_Text").GetComponent<Text>().text = characterObj.getDerivedStat(derivedStat.turnFrequency).ToString();
        GameObject.Find("uI_Movement_Text").GetComponent<Text>().text = characterObj.getDerivedStat(derivedStat.movement).ToString();

    }

}
