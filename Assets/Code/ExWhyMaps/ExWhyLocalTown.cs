
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class  ExWhyLocalTown : ExWhy
{
    public ExWhyLocalTown() : base(20, 13, "Town")
    {
        //g =  grass, d = dirt, w = wood, s = shop1, h = shop2
        worldData = new char[20,13]{   
            {'g','g','g','g','w','d','d','d','w','g','g','g','g' },
            {'g','s','s','s','w','d','d','d','w','g','g','g','g' },
            {'g','s','s','s','w','d','d','d','w','g','g','g','g' },
            {'w','w','w','w','w','d','d','d','w','h','h','h','g' },
            {'d','d','d','d','d','d','d','d','w','h','h','h','g' },
            {'d','d','d','d','d','d','d','d','w','h','h','h','g' },
            {'w','w','w','w','w','d','d','d','w','s','s','s','g' },
            {'w','s','s','s','w','d','d','d','w','s','s','s','g' },
            {'w','s','s','s','w','d','d','d','w','s','s','s','g' },
            {'w','s','s','s','w','d','d','d','w','h','w','w','g' },
            {'g','h','h','h','w','d','d','d','w','s','s','s','g' },
            {'g','h','h','h','w','d','d','d','w','s','s','s','g' },
            {'g','h','h','h','w','d','d','d','w','s','s','s','g' },
            {'g','w','w','w','w','d','d','d','w','w','w','w','g' },
            {'g','s','s','s','w','d','d','d','w','w','w','w','g' },
            {'g','s','s','s','w','d','d','d','w','h','h','h','g' },
            {'g','s','s','s','w','d','d','d','w','h','h','h','g' },
            {'g','g','g','g','w','d','d','d','w','w','w','w','g' },
            {'g','g','g','g','g','d','d','d','w','g','g','s','g' },
            {'g','g','d','g','g','d','d','d','w','g','g','g','g' },
             };

        initiateCells();
    }

    protected override void instantiateCells()
    {
        foreach(ExWhyCell cell in gridCells)
        {
            switch (cell.cellType)
            {
                case 'd':
                    instantiateCell(cell, true, 0, 0);
                    cell.resourceName = "Dirt";
                    cell.makeAltable();
                    cell.setAltRate(30);
                    cell.setCellBuff(new GenericCellBuff(0, 0, 0));
                    break;

                case 'g':
                    instantiateCell(cell, true, 1, 1);
                    cell.resourceName = "Grass";
                    cell.makeAltable();
                    cell.setAltRate(30);
                    cell.setCellBuff(new GenericCellBuff(0, 0, 0));
                    cell.animatable = true;
                    break;

                case 's':
                    instantiateCell(cell, false, 2, 2);
                    cell.resourceName = "Plaster";
                    cell.setCellBuff(new GenericCellBuff(0, 0, 0));
                    break;

                case 'w':
                    instantiateCell(cell, true, 3, 3);
                    cell.resourceName = "WoodenBoard";
                    cell.setCellBuff(new GenericCellBuff(0, 0, 0));
                    break;

                case 'h':
                    instantiateCell(cell, false, 4, 4);
                    cell.setCellBuff(new GenericCellBuff(0, 0, 0));
                    cell.resourceName = "Shop";
                break;
            }
        }
    }

    protected override void instantiateEvents(){}

    protected override void eventTest(int eventIndex){}
}
