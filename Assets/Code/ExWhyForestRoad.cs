using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExWhyForestRoad : ExWhy
{
    
        public ExWhyForestRoad() :base(20, 13, "Bridge")
        {
        worldData = new char[20, 13]{
            {'t','g','g','g','g','d','d','d','g','g','t','t','t' },
            {'t','t','g','g','g','d','d','d','g','t','t','t','t' },
            {'t','t','g','g','g','g','d','g','g','t','t','t','t' },
            {'t','t','t','t','t','t','d','t','t','t','t','g','t' },
            {'t','t','t','t','t','t','d','t','t','t','t','g','t' },
            {'t','t','g','d','g','g','d','g','g','g','g','g','t' },
            {'t','t','g','d','d','d','d','d','g','g','g','g','t' },
            {'t','t','t','g','g','d','d','d','g','g','g','g','t' },
            {'t','t','t','g','g','d','d','d','g','g','g','t','t' },
            {'t','t','g','g','g','d','d','d','g','g','t','t','t' },
            {'t','t','g','g','g','d','d','d','g','g','g','g','t' },
            {'t','t','g','g','g','d','d','d','g','g','g','g','t' },
            {'t','t','g','g','g','d','d','d','g','g','g','g','t' },
            {'t','t','t','g','g','d','d','d','g','g','g','g','t' },
            {'t','t','t','g','g','d','d','d','g','g','g','g','t' },
            {'t','g','g','g','g','g','d','g','g','g','g','g','t' },
            {'t','g','t','t','t','t','d','t','t','t','t','t','t' },
            {'t','g','t','t','t','t','d','t','t','t','t','t','t' },
            {'t','g','g','g','g','g','d','g','g','d','g','g','t' },
            {'t','g','g','g','g','g','d','g','g','d','g','g','t' },
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

    protected override void eventTest(int eventIndex)
    {

    }

    protected override void instantiateCells()
    {
        foreach (ExWhyCell cell in gridCells)
        {
            switch (cell.cellType)
            {

                case 'g':
                    instantiateCell(cell, true, 3, 3);
                    cell.resourceName = "Grass";
                    cell.makeAltable();
                    cell.setAltRate(20);
                    cell.setCellBuff(new GenericCellBuff(0, 0, 0));
                    break;

                case 'd':
                    instantiateCell(cell, true, 1, 1);
                    cell.resourceName = "Dirt";
                    cell.makeAltable();
                    cell.setAltRate(20);
                    cell.setCellBuff(new GenericCellBuff(0, 0, 0));
                    break;

                case 't':
                    instantiateCell(cell, false, 2, 2);
                    cell.resourceName = "Forest";
                    cell.makeAltable();
                    cell.setAltRate(50);
                    cell.setCellBuff(new GenericCellBuff(0, 0, 0));
                    cell.animatable = true;
                    cell.animatableRate = 5;
                    break;
            }
        }

    }

    protected override void instantiateEvents()
    {
    
    }
}
