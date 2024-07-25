using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilitySnippetController : Window
{

    // Start is called before the first frame update
    void Start()
    {
        foreach( Image image in GetComponentsInChildren<Image>()){
            if(image.name == "uI_AbilityIcon_Image")
            {
                image.sprite = ability.abilityIcon;
            }
        }
            setText();
       // if(this.transform.parent.name == "Canvas")
       // {
          //  this.GetComponent<RectTransform>();
        //}

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Yo, this one might look a little wild, but you can't just use "GO.Find" because there'll be doubles. Same issue with GetComp. So we need to merge the two. 
    void setText()
    {
        Text[] x = GetComponentsInChildren<Text>();
        foreach(Text y in x)    
        {
            if (y.gameObject.name == "uI_AbilityName_Text")
            {
                y.text = ability.name;
            }
            else if (y.gameObject.name == "uI_AbilityType_Text")
            {
                y.text = ability.getTypeForUI();
            }
            else if (y.gameObject.name == "uI_AbilityCost_Text")
            {
                y.text = ability.getCostString();
            }
            else if (y.gameObject.name == "uI_AbilityDescription_Text")
            {
                y.text = ability.description;
            }

        }
    }

    public void selectAbility()
    {
        GameObject.Find("MapController").GetComponent<BattleController>().selectAbility(ability);
        GameObject.Find("MapController").GetComponent<BattleUIController>().closeWindow(this.gameObject);
        GameObject.Find("MapController").GetComponent<BattleUIController>().closeWindow(GameObject.Find("uI_AbilityMenu_Panel(Clone)"));
    }

    public override void  closeWindow()
    {
        base.closeWindow();
        GameObject.Find("MapController").GetComponent<BattleController>().destroyARVisual();

    }

}
