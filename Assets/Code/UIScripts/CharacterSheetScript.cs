using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSheetScript : Window
{

    public Character characterObj;

    public void setCharacterObject(Character chr)
    {
        characterObj = chr;
        inputInformation(); 
    }

    void inputInformation()
    {
        foreach(Text text in GetComponentsInChildren<Text>(true))
        {
            switch (text.name)
            {
                case "uI_Name_Text":
                    text.text = characterObj.CharacterName;
                break;


                case "uI_StrengthAmount_Text":
                    text.text = characterObj.getBasicStat(stat.strength).ToString();
                    break;

                case "uI_VitalityAmount_Text":
                    text.text = characterObj.getBasicStat(stat.vitality).ToString();
                    break;

                case "uI_SpeedAmount_Text":
                    text.text = characterObj.getBasicStat(stat.speed).ToString();
                    break;

                case "uI_PrecisionAmount_Text":
                    text.text = characterObj.getBasicStat(stat.precision).ToString();
                    break;

                case "uI_FocusAmount_Text":
                    text.text = characterObj.getBasicStat(stat.focus).ToString();
                    break;

                case "uI_IngenuityAmount_Text":
                    text.text = characterObj.getBasicStat(stat.ingenuity).ToString();
                    break;

                case "uI_Health_Text":
                    text.text = characterObj.getDerivedStat(derivedStat.maxHealthPoints).ToString() + "/" + characterObj.HealthPoints;
                    break;

                case "uI_Mana_Text":
                    text.text = characterObj.getDerivedStat(derivedStat.maxManaPoints).ToString() + "/" + characterObj.ManaPoints;
                    break;

                case "uI_ToHitDodge_Text":
                    text.text = characterObj.getDerivedStat(derivedStat.dodgeTH).ToString();
                    break;

                case "uI_ToHitBlock_Text":
                    text.text = characterObj.getDerivedStat(derivedStat.blockTH).ToString();
                    break;

                case "uI_ToHitParry_Text":
                    text.text = characterObj.getDerivedStat(derivedStat.parryTH).ToString();
                    break;

                case "uI_ToReactDodge_Text":
                    text.text = characterObj.getDerivedStat(derivedStat.dodge).ToString();
                    break;

                case "uI_ToReactBlock_Text":
                    text.text = characterObj.getDerivedStat(derivedStat.block).ToString();
                    break;

                case "uI_ToReactParry_Text":
                    text.text = characterObj.getDerivedStat(derivedStat.parry).ToString();
                    break;

                case "uI_MeleeBonus_Text":
                    text.text = characterObj.getDerivedStat(derivedStat.meleeBonus).ToString();
                    break;

                case "uI_RangedBonus_Text":
                    text.text = characterObj.getDerivedStat(derivedStat.rangedBonus).ToString();
                    break;

                case "uI_MagicBonus_Text":
                    text.text = characterObj.getDerivedStat(derivedStat.magicBonus).ToString();
                    break;

                case "uI_TurnFrequency_Text":
                    text.text = characterObj.getDerivedStat(derivedStat.turnFrequency).ToString();
                    break;

                case "uI_Movement_Text":
                    text.text = characterObj.getDerivedStat(derivedStat.movement).ToString();
                    break;

                case "uI_ManaFlow_Text":
                    text.text = characterObj.getDerivedStat(derivedStat.manaFlow).ToString();
                    break;
            }
        }    
    }

}
