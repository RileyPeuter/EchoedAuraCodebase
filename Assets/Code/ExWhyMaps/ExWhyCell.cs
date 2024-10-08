using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ExWhyCell
{
    //###MemberVariables###
    public int xPosition;
    public int yPosition;
    public GameObject cellGO;
    public GameObject fogOfWarObject;
    public char cellType;
    public int eventIndex;
    public int colID;
    public int spriteID;
    public string resourceName;
    public BattleCharacterObject occupier;

    public static GameObject FOWPrefab;

    public GameObject fieldVisual;

    public bool altable = false;
    public int altRate = 50;

    public bool animatable = false;
    public int animatableRate = 50;

    public bool walkable;

    CellBuff cellBuff;

    public bool fogOfWar;

    public void toggleFOW(bool toggle)
    {
        fogOfWarObject.GetComponent<SpriteRenderer>().enabled = toggle;
    }

    //###Getters###
    public override string ToString()
    {
        return "X:" + xPosition.ToString() + ",Y:" + yPosition.ToString();
    }

    public Transform getTransform()
    {
        return cellGO.transform;
    }

    public CellBuff getCellBuff()
    {
        return cellBuff;
    }

    BattleCharacterObject getOccupied()
    {
        return occupier;
    }
    //###Setters###
    public void setAltRate(int rate)
    {
        altRate = rate;
    }
    
    public void setCellBuff(CellBuff nCellBuff)
    {
        cellBuff = nCellBuff;
    }
    public void makeAltable()
    {
        altable = true;
    }
    void makeOccupy(BattleCharacterObject chara)
    {
        occupier = chara;
    }



    //###Constructor###
    public ExWhyCell(int nXPosition, int nYPosition, char nCellType, int nEventIndex = 0)
    {
        cellType = nCellType;
        eventIndex = nEventIndex;
        walkable = true;
        xPosition = nXPosition;
        yPosition = nYPosition;
    }
}
