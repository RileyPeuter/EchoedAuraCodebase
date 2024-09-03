using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class AttackAttemptUIController : Window
{


    // Start is called before the first frame update
    void Start()
    {

        setValues();
    }

    public void setValues()
    {
        int[,] hitMatrix = attackAttempt.getHitMatrix();
        int[] reactMultipliers = attackAttempt.getMultipliers();

        Text[] texts = GetComponentsInChildren<Text>();
        foreach (Text text in texts)
        {
            switch (text.name)
            {
                case "uI_DodgeToHit_Text":
                    text.text = hitMatrix[0, 0].ToString();
                    break;

                case "uI_DodgeToReact_Text":
                    text.text = hitMatrix[0, 1].ToString();
                    break;
                case "uI_DodgeDifference_Text":
                    text.text = hitMatrix[0, 2].ToString();
                    modifyDifferences(text);
                    break;
                case "uI_BlockToHit_Text":
                    text.text = hitMatrix[1, 0].ToString();
                    break;
                case "uI_BlockToReact_Text":
                    text.text = hitMatrix[1, 1].ToString();
                    break;
                case "uI_BlockDifference_Text":
                    text.text = hitMatrix[1, 2].ToString();
                    modifyDifferences(text);
                    break;
                case "uI_ParryToHit_Text":
                    text.text = hitMatrix[2, 0].ToString();
                    break;
                case "uI_ParryToReact_Text":
                    text.text = hitMatrix[2, 1].ToString();
                    break;
                case "uI_ParryDifference_Text":
                    text.text = hitMatrix[2, 2].ToString();
                    modifyDifferences(text);
                    break;

                case "uI_DodgeMultiplier_Text":
                    text.text = reactMultipliers[0].ToString();
                    break;

                case "uI_BlockMultiplier_Text":
                    text.text = reactMultipliers[1].ToString();
                    break;

                case "uI_ParryMultiplier_Text":
                    text.text = reactMultipliers[2].ToString();
                    break;
                case "uI_DamageRange_Text":
                    text.text = attackAttempt.getAbility().getDamage(attackAttempt.getAttackee().getCharacter()).ToString();
                    break;
                case "uI_DamageModifierType_Text":
                    text.text = attackAttempt.getAbility().getTypeForUI();
                    break;
                default:
                    break;

            }
        }


        if (!attackAttempt.reactionsAvailalbe.Contains(reactionType.Dodge))
        {
            gameObject.GetComponentsInChildren<Image>(true).ToList().Find(x => x.name == "uI_DodgeRowCross_Image").gameObject.SetActive(true);
        }

        if (!attackAttempt.reactionsAvailalbe.Contains(reactionType.Block))
        {
            gameObject.GetComponentsInChildren<Image>(true).ToList().Find(x => x.name == "uI_BlockRowCross_Image").gameObject.SetActive(true);
        }

        if (!attackAttempt.reactionsAvailalbe.Contains(reactionType.Parry))
        {
            gameObject.GetComponentsInChildren<Image>(true).ToList().Find(x => x.name == "uI_ParryRowCross_Image").gameObject.SetActive(true);
        }
    }

    public int attemptCast() {
        return attackAttempt.attemptCast();
    }

    void modifyDifferences(Text text)
    {
        int val = int.Parse(text.text);
            if(val <= 0)
            {
            text.GetComponentInParent<Image>().color = Color.red;
            text.color = new Color(0.8f, 0.4f,0.4f);    
            }
            else if (val <= 10)
            {

            text.GetComponentInParent<Image>().color = Color.yellow;
            text.color = new Color(0.6f, 0.6f, 0.4f);    
            }
            else
        {
            text.GetComponentInParent<Image>().color = Color.green;
            text.color = new Color(0.4f, 0.8f, 0.4f);
        }
    }
    

    // Update is called once per frame
    void Update()
    {
        
    }
}
