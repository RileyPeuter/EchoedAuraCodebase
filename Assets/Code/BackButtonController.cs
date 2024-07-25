using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackButtonController : MonoBehaviour
{
    //###MemberVariables###
    BattleUIController BUIC;
    Window assosiatedWinow;
    BattleController deselectAbilityListener;

    bool buttonPressed = false;

    //###Getter###
    public bool getButtonPressed()
    {
        return buttonPressed;
    }
    public void pressButton()
    {
        buttonPressed = true;
    }

    //###Setter###
    //This is used if the back button is for an ability that's selected
    public void setDeselectListener(BattleController nDeselectListener)
    {
        deselectAbilityListener = nDeselectListener;
    }

    //###Utilities###
    public bool checkBack()
    {
        if (Input.GetKeyDown("z"))
        {
            Back();
            return true;
        }
        return false;
    }

    public void Back()
    {
        BattleUIController.checkForDeleted();
        BUIC.releaseFocus();
        assosiatedWinow.closeWindow();

        if (deselectAbilityListener != null)
        {
            deselectAbilityListener.deSelectAbility();
        }
    }

    //###Initialize###
    public void initialize(BattleUIController BU)
    {
        BUIC = BU;
        assosiatedWinow = gameObject.GetComponentInParent<Window>();
    }
}
