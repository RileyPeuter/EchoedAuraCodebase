using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSnippetController : Window
{

    MapCharacterListController MCLCController;
    CharacterSelectController CSCController;

    StoredCharacterObject storedCharacter;

    

    // Start is called before the first frame update
    public void initialize(Window nParent, StoredCharacterObject nCharacter, bool hasCharacterSheetButton)
    {
        initialize(nParent);

        if (!hasCharacterSheetButton)
        {
            Destroy(GetComponentsInChildren<Button>().ToList().Find(x => x.name == "uI_ShowCharacterSheet_Button"));
        }
        storedCharacter = nCharacter;
    }

    public void showCharacterSheet()
    {
        CharacterSheetScript CSS = MCLCController.getUIC().openWindow("uI_CharacterSheet_Panel").GetComponent<CharacterSheetScript>();
        CSS.initialize(UIController.HighestWindow);
        CSS.setCharacterObject(storedCharacter.GetCharacter());
    }

    void Start()
    {
        setText();
        GetComponentsInChildren<Image>().ToList().Find(x => x.gameObject.name == "uI_Emblem_Image").sprite = ResourceLoader.loadSprite("CharacterEmblems/" + storedCharacter.GetCharacter().resourceString);
        
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
