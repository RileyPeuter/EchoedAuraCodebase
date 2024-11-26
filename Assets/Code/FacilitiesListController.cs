using System.Collections;
using System.Collections.Generic;
using UnityEditor.Purchasing;
using UnityEngine;

public class FacilitiesListController : MonoBehaviour
{

    List<Facility> facilities;
    List<FacilitySnippetController> facilitySnippetControllers;
    GameObject facilitySnippetPrefab;

    public void initialize(List<Facility> nFacilities)
    {
        facilitySnippetControllers = new List<FacilitySnippetController>(); 
        facilitySnippetPrefab = Resources.Load<GameObject>("UIElements/uI_FacilitySnippet_Panel");
        facilities = nFacilities;
        populateList();
    }

    public void purchaseEffects()
    {
        foreach (FacilitySnippetController FSC in facilitySnippetControllers) {
            FSC.checkForBuildable();
        }
    }

    public void populateList()
    {
        int xOffset = -1;
        int yOffset = 0;
        foreach (Facility facility in facilities)
        {
            GameObject GO = Instantiate(facilitySnippetPrefab, this.transform);
            GO.GetComponent<FacilitySnippetController>().initialize(facility, this);
            GO.GetComponent<RectTransform>().anchoredPosition *= new Vector2(xOffset, 1);
            GO.GetComponent<RectTransform>().anchoredPosition += new Vector2(0, yOffset);
            facilitySnippetControllers.Add(GO.GetComponent<FacilitySnippetController>());
            xOffset = xOffset * -1;
            if(xOffset < 0)
            {
                yOffset -= 70;
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
        
    }
}
