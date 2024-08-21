using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class QuickSelectButtonScript : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    GameObject abilitySnippet;
    GameObject abilitySnippetPrefab;
    Ability ability;
    ActionMenuController controller;
    int hotKey;

    public void OnPointerEnter(PointerEventData eventData)
    {
        abilitySnippet = Instantiate(abilitySnippetPrefab, this.transform);
        abilitySnippet.GetComponent<AbilitySnippetController>().initialize(BattleUIController.HighestWindow, ability);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Destroy(abilitySnippet);
        abilitySnippet = null;
    }

    
    public void initialize(Ability nAbility , ActionMenuController nController, int nHotKey)
    {
        controller = nController;
        ability = nAbility;
        abilitySnippetPrefab = Resources.Load<GameObject>("UIElements/uI_Ability_Panel");
        hotKey = nHotKey;
    }

    // Update is called once per frame
    void Update()
    {
        if (abilitySnippet != null) 
        {
            abilitySnippet.GetComponent<RectTransform>().position = Input.mousePosition + new Vector3(0, 50, 0);
        }
    }

    void Start()
    {
        setInfo();
    }

    void setInfo()
    {
        foreach(Image image in GetComponentsInChildren<Image>())
        {
            print(image.name);
            if(image.name == "uI_AbilityIcon_Button(Clone)")
            {
                image.sprite = ability.abilityIcon;
            }
        }

        foreach(Text text in GetComponentsInChildren<Text>())
        {
            if(text.name == "uI_HotkeyNumber_Text")
            {
                text.text = hotKey.ToString();
            }
        }


        }

    public void selectAbility()
    {
        if(abilitySnippet is null)
        {
            abilitySnippet = Instantiate(abilitySnippetPrefab);
            abilitySnippet.GetComponent<AbilitySnippetController>().initialize(BattleUIController.HighestWindow, ability);
        }
        abilitySnippet.GetComponent<AbilitySnippetController>().selectAbility();
        Destroy(abilitySnippet);
        abilitySnippet = null;
    }

}
