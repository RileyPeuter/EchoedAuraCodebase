using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtraManagemeentMenuController : MonoBehaviour
{
    Agency agency;
    GameObject trainingWindow;
    GameObject facilitiesWindow;
    GameObject livingQuatersWindow;
    GameObject statisticsWindow;
    GameObject characterSheet;
    public void initialize(Agency nAgency)
    {
        agency = nAgency;
    } 

    // Start is called before the first frame update
    void Start()
    {
        //        trainingWindow = Instantiate(Resources.Load<GameObject>("")) 
        facilitiesWindow = Instantiate(Resources.Load<GameObject>("UIElements/uI_FacilityList_Panel"), GameObject.Find("Canvas").transform);
        facilitiesWindow.GetComponent<FacilitiesListController>().initialize(agency.getFacilities());
        facilitiesWindow.gameObject.SetActive(false);

        //        trainingWindow = Instantiate(Resources.Load<GameObject>("")) 
        //        trainingWindow = Instantiate(Resources.Load<GameObject>("")) 

    }

    public void toggleFacilities()
    {
        facilitiesWindow.SetActive(!facilitiesWindow.activeInHierarchy);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
