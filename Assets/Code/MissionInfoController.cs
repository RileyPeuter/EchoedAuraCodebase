using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MissionInfoController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    GameObject childPanel;
    OverworldMapController OWMC;
    public void OnPointerEnter(PointerEventData eventData)
    {

        print("toy");
        childPanel.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        print("joy");
        childPanel.SetActive(false);
    }

    public void selectMission()
    {
        if (OWMC)
        {
            OWMC.selectMission(mission);
        }
    }

    Mission mission;
    // Start is called before the first frame update
    void Start()
    {
        setInfo();   
        foreach(Image image in GetComponentsInChildren<Image>())
        {
            if(image.name == "uI_MissionInformation_Panel")
            {
                childPanel = image.gameObject;
            }
        }



        if(gameObject.GetComponent<RectTransform>().localPosition.x > 0)
        {
            childPanel.GetComponent<RectTransform>().position += new Vector3(-300, 0, 0);
        }
        childPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void initialize(Mission miss, OverworldMapController OMC)
    {
        OWMC = OMC;
        mission = miss;
    }

    void setInfo()
    {
        foreach(Text text in GetComponentsInChildren<Text>())
        {
            switch (text.name)
            {
                case "uI_MissionName_Text":
                    text.text = mission.name;
                break;

                case "uI_MissionDescription_Text":
                    text.text = mission.description;
                break;

            }
        }

        GetComponent<Image>().sprite = mission.buttonImage;
    }
}
