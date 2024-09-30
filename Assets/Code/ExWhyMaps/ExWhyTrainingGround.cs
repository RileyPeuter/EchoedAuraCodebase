using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExWhyTrainingGround : ExWhy
{
    public ExWhyTrainingGround() :base(10, 10, "TrainingGround")
    {
        worldData = new char[10, 10]
        {
            {'g','g','g','g','g','g','g','g','g','g' },
            {'g','f','f','f','f','f','f','f','f','g' },
            {'g','f','g','g','g','g','g','g','f','g' },
            {'g','f','g','g','d','d','g','g','f','g' },
            {'g','g','g','d','d','d','d','g','f','g' },
            {'g','g','g','d','d','d','d','g','f','g' },
            {'g','f','g','g','d','d','g','g','f','g' },
            {'g','f','g','g','g','g','g','g','f','g' },
            {'g','f','f','f','f','f','f','f','f','g' },
            {'g','g','g','g','g','g','g','g','g','g' },
        };
        
        eventMask = new int[10, 10]
        {
            {0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0}
        };
        initiateCells();
    }

    protected override void eventTest(int eventIndex){}

    //g = grass, d = dirt, f = fence
    protected override void instantiateCells()
    {
     foreach(ExWhyCell cell in gridCells)
        {
            //g = grass, f = fence, d = dirt
            switch (cell.cellType)
            {
                case 'd':
                    instantiateCell(cell, true, 0, 0);
                    cell.resourceName = "Dirt";
                    cell.makeAltable();
                    cell.setAltRate(20);
                    cell.setCellBuff(new GenericCellBuff(0, 4, 0));
                break;

                case 'f':
                    instantiateCell(cell, false, 1, 1);
                    cell.resourceName = "Fence";
                    cell.setCellBuff(new GenericCellBuff(0, 0, 0));
                    break;

                case 'g':
                    instantiateCell(cell, true, 2, 2);
                    cell.resourceName = "Grass";
                    cell.makeAltable();
                    cell.setAltRate(10);
                    cell.animatable = true;

                    cell.setCellBuff(new GenericCellBuff(3, 0, -2));
                    break;
            }
        }
    }
    protected override void instantiateEvents(){}
}
