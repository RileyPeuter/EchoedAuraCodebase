using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GenericCellEffectController : CellEffectController
{
    public void initialize(GenericCellBuff buff) 
    {
        foreach (Text text in GetComponentsInChildren<Text>())
        {
            switch (text.name)
            {
                case "uI_Dodge_Text":
                    text.text = buff.dodgeBonus.ToString();
                    if(buff.dodgeBonus > 0) { text.color = Color.green;}
                    if (buff.dodgeBonus < 0) { text.color = Color.red; } 
                    break;

                case "uI_Block_Text":
                    text.text = buff.blockBonus.ToString();
                    if (buff.blockBonus > 0) { text.color = Color.green; }
                    if (buff.blockBonus < 0) { text.color = Color.red; }
                    break;

                case "uI_Parry_Text":
                    text.text = buff.parryBonus.ToString();
                    if (buff.parryBonus > 0) { text.color = Color.green; }
                    if (buff.parryBonus < 0) { text.color = Color.red; }
                    break;
            }
        }
    }

    public override void setData(CellBuff buff)
    {
        
    }
}
