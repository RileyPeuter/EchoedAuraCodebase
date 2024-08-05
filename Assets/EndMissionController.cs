using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndMissionController : MonoBehaviour
{
    BattleController battleController;
    public void initialize(BattleController nBattleController, int gold, int  casualties, int timeTaken)
    {
        foreach(Text text in GetComponentsInChildren<Text>())
        {
            switch (text.name)
            {
                case "uI_Casualties_Text":
                    text.text = casualties.ToString();
                    break;

                case "uI_TimeTaken_Text":
                    text.text = timeTaken.ToString();
                    break;

                case "uI_ContractGold_Text":
                    text.text = gold.ToString();
                    break;
            }
        }

        battleController = nBattleController;
    }

    public void endBattle()
    {
        battleController.endBattle();
    }

}
