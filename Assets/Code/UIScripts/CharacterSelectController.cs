using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CharacterSelectController : Window
{
    StoredCharacterObject[] characters;
    GameObject characterInfoPrefab;
    OverworldMapController OWMC;
    List<GameObject> characterButtons;
    bool ATA = true;
    public int getCharacterSelectedCount()
    {
        return characterButtons.Count;
    }

    public bool addCharacter(StoredCharacterObject n_Character)
    {
        
        for (int i = 0; i < characters.Length; i++)
        {
            if (characters[i] == null)
            {
                characters[i] = n_Character;
                break;
            }
        }


            bool full = true;


        for (int i = 0; i < characters.Length; i++)
        {
            if (characters[i] == null)
            {
                full = false;
                break;
            }
        }

        displayCharacters();
        return full;
    }

    public void setOWMC(OverworldMapController nOWMC)
    {
        characterButtons = new List<GameObject>();
        OWMC = nOWMC;
    }

    public void deselectCharacter(StoredCharacterObject character)
    {
        OWMC.deselctCharacter(character);

        for (int i = 0; i < characters.Length; i++)
        {
            if (characters[i] == character)
            {
                characters[i] = null;
            }
        }

        displayCharacters();

    }

    void displayCharacters()
    {
        foreach (GameObject gameObject in characterButtons)
        {
            Destroy(gameObject);
        }
        characterButtons.Clear();

        int yOffset = 0;
        for (int i = 0; i < characters.Length; i++)
        {
            if (characters[i] != null)
            {
                GameObject GO = Instantiate(characterInfoPrefab, this.transform);
                GO.GetComponent<CharacterSnippetController>().initialize(this, characters[i]);
                GO.GetComponent<CharacterSnippetController>().setListener(this);
                GO.GetComponent<RectTransform>().localPosition = new Vector3(0, 80 + yOffset);
                characterButtons.Add(GO);
            }
            yOffset = yOffset - 55;
        }
    }

    public void confirmCharacters()
    {
        if (getCharacterCount() > 0 || ATA == false)
        {
            OWMC.startMission(characters.ToList());
        }


    }

    // Start is called before the first frame update
    void Start()
    {
        characterInfoPrefab = Resources.Load<GameObject>("UIElements/uI_Character_Button");
        characters = new StoredCharacterObject[4];

    }

    public int getCharacterCount()
    {
        int output = 0;

        for(int i = 0; i < 4; i++) { if (characters[i] != null) { output++; } }

        return output;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void initialize(List<StoredCharacterObject> forcedCharacters = null, bool ableToAdd = true)
    {
        ATA = ableToAdd;
    }
}
