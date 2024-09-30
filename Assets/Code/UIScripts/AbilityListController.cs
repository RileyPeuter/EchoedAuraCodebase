using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityListController : Window
{
    public  List<Ability> abilities;
    GameObject test;
    public  UIController BUIC;
    // Start is called before the first frame update
    void Start()
    {
        PopulateList();
    }

    public void initialize()
    {
        abilities = new List<Ability>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PopulateList()
    {
        int xOffset = 0;
        int yOffset = -20;
        bool scndFlag = false;
        foreach(Ability ability in abilities)
        {
            //This line creates a new panel, then tells it it's ability
            GameObject GO = GameObject.Find("MapController").GetComponent<UIController>().openWindow("uI_Ability_Panel", true, this.gameObject.name, false);
            GO.GetComponent<AbilitySnippetController>().initialize(UIController.HighestWindow, ability);
            GO.GetComponent<RectTransform>().anchoredPosition = new Vector3(GO.GetComponent<RectTransform>().anchoredPosition.x + xOffset, GO.GetComponent<RectTransform>().anchoredPosition.y + yOffset, 0f);

            if (!ability.isCastable(BattleController.ActiveBattleController.getActiveCharacter()))
            {
                GO.GetComponentInChildren<Button>().interactable = false;
                foreach(Image image in GO.GetComponentsInChildren<Image>())
                {
                    image.color = image.color - new Color(0, 0, 0, 0.8F);
                }

            }

            scndFlag = !scndFlag;
            
            if (scndFlag)
            {
                xOffset = 340;
            }
            else
            {
                xOffset = 0;
                yOffset += -70;
                test = GO;
            }
        }
    }

}
