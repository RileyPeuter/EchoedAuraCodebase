using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class CellInformationController : Window
{
    bool mainstay;
    GameObject effectObject;

    public void initialize(ExWhyCell cell, bool nMainstay = false)
    {

        if(effectObject != null)
        {
            Destroy(effectObject);
        }
        mainstay = nMainstay;

        effectObject = GameObject.Instantiate(cell.getCellBuff().getDisplayGameObject(), this.transform);
        effectObject.GetComponent<GenericCellEffectController>().initialize((GenericCellBuff)cell.getCellBuff());
        GetComponentsInChildren<Text>().ToList().Find(x => x.name == "uI_CellTypeName_Text").text = cell.resourceName + " [" + cell.xPosition + " " + cell.yPosition +"]";


        /*
        foreach(Text text in GetComponentsInChildren<Text>()){
            if(text.gameObject.name == "uI_CellTypeName_Text")
            {
                text.text = cell.resourceName;
            }
            if(text.gameObject.name == "uI_CellEffects_Text")
            {
                text.text = cell.getCellBuff().getDescriptionString();
            }
        }
        */
    }

    // Update is called once per frame
    void Update()
    {
        if(!Input.GetKey("left alt") && !mainstay)
        {
            Destroy(gameObject);
        }
    }

    public void setText(ExWhyCell cell)
    {
        foreach (Text text in GetComponentsInChildren<Text>())
        {
            if (text.gameObject.name == "uI_CellTypeName_Text")
            {
                text.text = cell.resourceName;
            }
            if (text.gameObject.name == "uI_CellEffects_Text")
            {
                text.text = cell.getCellBuff().getDescriptionString();
            }
        }
    }

}
