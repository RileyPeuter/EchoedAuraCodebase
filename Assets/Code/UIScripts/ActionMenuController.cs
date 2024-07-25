using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ActionMenuController : Window
{
    GameObject quickSelectBar;
    public GameObject BasicMenu;
    BattleUIController BUIC;
    BattleController BC;
    List<QuickSelectButtonScript> quickSelectButtons;

    public void OpenMenu(int menuType)
    {
        BUIC.closeAllTransient();
        switch (menuType)
        {
            case 0:
                BC.selectAbility(character.getMovementAbility());
                break;

            case 1:
                BUIC.openWindow("uI_AbilityMenu_Panel", true, "uI_Actions_Panel(Clone)").GetComponent<AbilityListController>().abilities = character.getAllAbilities();
                break;

            case 2:
                BUIC.openWindow("uI_AbilityMenu_Panel", true, "uI_Actions_Panel(Clone)").GetComponent<AbilityListController>().abilities = character.getCharacter().CharacterAbilities;
                break;
        }
    }

    public void setBUIC(BattleUIController NBUIC)
    {
        BUIC = NBUIC;
    }

    public void checkHotkeys()
    {
        print("wam");
        int index = 0;
        
        if(quickSelectButtons is null) { return; }


        foreach (QuickSelectButtonScript QSBS in quickSelectButtons)
        {
            if (Input.GetKeyDown((index + 1).ToString()))
            {
                print("bam");
                QSBS.selectAbility();
            }
            index++;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        quickSelectButtons = new List<QuickSelectButtonScript>();

        //BattleController kappa = GameObject.Find("MapController").GetComponent<TrainingGroundBattleController>();
        //activeCharacter = GameObject.Find("MapController").GetComponent<TrainingGroundBattleController>().getActiveCharacter().getCharacter();
        BasicMenu = Resources.Load<GameObject>("UIElements/uI_AbilityMenu_Panel");
        BC = GameObject.Find("MapController").GetComponent<BattleController>();
        foreach(Image GO in this.GetComponentsInChildren<Image>())
        {
            if(GO.name == "uI_QuickSelect_Panel") { quickSelectBar = GO.gameObject; }
        }
        populateQuickButtons();
    }

    public void populateQuickButtons()
    {
        
        int xOffset = 115;
        int hotkeyNumber = 1;
        foreach(Ability ability in character.getAllAbilities())
        {
            if (!ability.isCastable(character)) { continue; }

            GameObject GO = Instantiate(Resources.Load<GameObject>("UIElements/uI_AbilityIcon_Button"), quickSelectBar.transform);
            GO.GetComponent<QuickSelectButtonScript>().initialize(ability, this, hotkeyNumber);
            quickSelectButtons.Add(GO.GetComponent<QuickSelectButtonScript>());
            GO.GetComponent<RectTransform>().localPosition += new Vector3(xOffset, 0, 0);
            xOffset += 78;
            hotkeyNumber++;
        }
    }

    void hotKeyCheck()
    {

    }
}