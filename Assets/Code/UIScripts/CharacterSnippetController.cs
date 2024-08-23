using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSnippetController : Window
{

    MapCharacterListController MCLCController;
    CharacterSelectController CSCController;
    // Start is called before the first frame update
    void Start()
    {
        setText();
        
    }

    public void disable()
    {
        this.GetComponent<Button>().interactable = false;   
    }

    public void reenable()
    {
        this.GetComponent<Button>().interactable = true;    
    }

    public void fireListenersMCLC()
    {
        if (MCLCController.selectCharacter(storedCharacter))
        {
            this.GetComponent<Button>().interactable = false;
        }
    }

    public void fireListenerCSCC()
    {
        CSCController.deselectCharacter(storedCharacter);
    }


    public StoredCharacterObject getCharacter()
    {
        return storedCharacter; 
    }


    public void setListener(CharacterSelectController nCSCController)
    {
        CSCController = nCSCController;
        this.GetComponent<Button>().onClick.AddListener(fireListenerCSCC);
    }

    public void setListener(MapCharacterListController MCLC)
    {
        MCLCController = MCLC;
        this.GetComponent<Button>().onClick.AddListener(fireListenersMCLC);

    }
    // Update is called once per frame
    void Update()
    {
        
    }

    void setText()
    {
        foreach (Text text in GetComponentsInChildren<Text>())
        {
            switch (text.name)
            {
                case "uI_Name_Text":
                    text.text = storedCharacter.getCharacterName();
                break;

                case "uI_Status_Text":
                    text.text = storedCharacter.getStateString();
                break;

                case "uI_Mana_Text":
                    text.text = storedCharacter.getMana().ToString() + "/" + storedCharacter.getMaxMana().ToString();
                    break;

            }
        }
    }
}
