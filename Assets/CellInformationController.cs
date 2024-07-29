using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CellInformationController : Window
{
    bool mainstay;
    public void initialize(ExWhyCell cell, bool nMainstay = false)
    {
        mainstay = nMainstay;
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
    }

    // Start is called before the first frame update
    void Start()
    {
        
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
