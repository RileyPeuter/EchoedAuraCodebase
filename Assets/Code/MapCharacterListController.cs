using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCharacterListController : Window
{
    // Start is called before the first frame update
    List<StoredCharacterObject> characters = new List<StoredCharacterObject>();
    BattleUIController BUIC;
    OverworldMapController OWMC;

    List<GameObject> characterButtons = new List<GameObject>();

    void Start()
    {
        populateList();
    }

    public void addCharacters(List<StoredCharacterObject> n_characters)
    {
        characters = n_characters;
    }

    public void deselectCharacter(StoredCharacterObject characterObject)
    {
        foreach (GameObject character in characterButtons)
        {
            if(character.GetComponent<CharacterSnippetController>().getCharacter() == characterObject)
            {
                character.GetComponent<CharacterSnippetController>().reenable();
            }
        }
    }

    public bool selectCharacter(StoredCharacterObject character)
    {
        return OWMC.selectCharacter(character);
    }
    public void setOWMC(OverworldMapController nOWMC)
    {
        OWMC = nOWMC;
    }

    void populateList()
    {
        int yOffset = 0;

        foreach (StoredCharacterObject character in characters)
        {
            GameObject GO = GameObject.Instantiate(Resources.Load<GameObject>("UIElements/uI_Character_Button"), this.transform);

            GO.GetComponent<CharacterSnippetController>().initialize(this, character);
            GO.GetComponent<CharacterSnippetController>().setListener(this);
            GO.GetComponent<RectTransform>().localPosition += new Vector3(0, yOffset);
            yOffset = yOffset - 50;

            characterButtons.Add(GO);
        }
    }
}