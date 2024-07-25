using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniStatsController : Window
{
    GameObject buffBar;
    // Start is called before the first frame update
    void Start()
    {

        buffBar = GetComponentInChildren<BuffBarScript>().gameObject;
        setInfo();
    }

    public void setInfo()
    {


        foreach (Text text in GetComponentsInChildren<Text>())
        {
            switch (text.name)
            {
                case "uI_CurrentName_Text":
                    text.text = character.getName();
                    break;

                case "uI_CurrentHealthNumeric_Text":
                    text.text = character.getCharacter().getDerivedStat(derivedStat.maxHealthPoints).ToString() + " / " + character.getCharacter().getCurrentHealth();
                    break;

                case "uI_CurrentManaNumeric_Text":
                    text.text = character.getCharacter().getDerivedStat(derivedStat.maxManaPoints).ToString() + " / " + character.getCharacter().getCurrentMana();
                    break;

                case "uI_CurrentManaFlow_Text":
                    text.text = character.getManaFlow().ToString();
                    break;
                case "uI_CurrentMovement_Text":
                    text.text = character.getMovement().ToString();
                    break;

            }
        }

        foreach (Slider slider in GetComponentsInChildren<Slider>())
        {
            switch (slider.name)
            {
                case "uI_CurrentManaSlider_Slider":
                    slider.maxValue = character.getCharacter().getDerivedStat(derivedStat.maxManaPoints);
                    slider.value = character.getCurrentMana();
                    break;

                case "uI_CurrentHealthSlider_Slider":
                    slider.maxValue = character.getCharacter().getDerivedStat(derivedStat.maxHealthPoints);
                    slider.value = character.getCharacter().getCurrentHealth();
                    break;
            }
        }
        setBuffs();
    }


    public void setBuffs()
    {
        if(character.getCharacter().buffs.Count > 0)
        {
        }
        foreach (Buff buff in character.getCharacter().buffs)
        {
            buffBar.GetComponent<BuffBarScript>().tryAddBuff(buff);

        }
    }



    public void addBuff(Buff buff)
    {
        buffBar.GetComponent<BuffBarScript>().tryAddBuff(buff);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
