using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MissionType
{
    Milestone,
    Character, 
    Side
}
public class Mission 
{
    public Mission(string nm, string dscrption, string iconPath, MissionType misTy, int BC, float xPosition, float yPosition, ExWhy EWMap = null)
    {
        name = nm;
        description = dscrption;
        buttonImage = Resources.Load<Sprite>("MissionIcons/" + iconPath);
        type = misTy;
        BCID = BC;
        map = EWMap;

        xMarkerPosition = xPosition;
        yMarkerPosition = yPosition; 

    }

    public float xMarkerPosition;
    public float yMarkerPosition;

    public string name;
    public string description;
    public Sprite buttonImage;
    public MissionType type;
    public int BCID;
    public ExWhy map;
}
