using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FacilitySnippetController : MonoBehaviour
{

    public Facility facility;
    FacilitiesListController facilitiesListController;

    public void initialize(Facility nFacility, FacilitiesListController nFacilityListController)
    {
        facility = nFacility;
        facilitiesListController = nFacilityListController;
        setInfo();

        if (!facility.getBuildable()) { makeDisabled(); } 
    }

    public void checkForBuildable() { if (!facility.getBuildable()) { makeDisabled(); } }

    public void makeDisabled()
    {
        GetComponent<Button>().enabled = false;
        GetComponent<Button>().interactable = false;

        foreach (Image image in GetComponentsInChildren<Image>())
        {
            image.color += new Color(-0.3f, -0.3f, -0.3f, -0.3f);
        }
    }


    void setInfo()
    {
        foreach(Text text in GetComponentsInChildren<Text>())
        {
            switch (text.name)
            {
                case "uI_Name_Text":
                    text.text = facility.getName();
                break;

                case "uI_Description_Text":
                    text.text = facility.getDescription();
                break;

                case "uI_Cost_Text":
                    text.text = facility.getCost().ToString();
                break;
            }
        }
    }


public void atteptBuild()
    {
        if (facility.attemptBuild())
        {
            makeDisabled();
        }
        facilitiesListController.purchaseEffects();
    }
}
