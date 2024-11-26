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
    //###MemberVariables###
    public float xMarkerPosition;
    public float yMarkerPosition;

    public string name;
    public string description;
    public Sprite buttonImage;
    public int cutsceneInt;
    public MissionType type;
    public int BCID;
    public ExWhy map;
    public bool hasSetCharacters = false;
    List <string> setCharacterNames = new List <string>();

    //###Constructor###
    public Mission(string nName, string nDescription, string nIconPath, MissionType nType, int BCID, float nXMarkerPosition, float nYMarkerPosition, ExWhy nMap = null, int nCutsceneInt = 0, bool hasSet = false)
    {
        name = nName;
        description = nDescription;
        buttonImage = Resources.Load<Sprite>("MissionIcons/" + nIconPath);
        type = nType;
        this.BCID = BCID;
        map = nMap;
        cutsceneInt = nCutsceneInt;
        xMarkerPosition = nXMarkerPosition;
        yMarkerPosition = nYMarkerPosition; 
        hasSetCharacters = hasSet;
    }

    public void addSetCharacters(List<string> characterNames)
    {
        foreach (string characterName in characterNames)
        {
            setCharacterNames.Add(characterName);
        }
    }

    public List<string> getSetCharacters()
    {
        return setCharacterNames;
    }
}
