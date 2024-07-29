using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExWhyCell
{
    //###MemberVariables###
    public int xPosition;
    public int yPosition;
    public GameObject cellGO;
    public char cellType;
    public int eventIndex;
    public int colID;
    public int spriteID;
    public string resourceName;
    public BattleCharacterObject occupier;
    public bool altable = false;
    public int altRate = 50;

    public bool walkable;

    CellBuff cellBuff;
    
    

    
    //###Getters###
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
