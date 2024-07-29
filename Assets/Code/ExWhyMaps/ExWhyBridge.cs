using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExWhyBridge : ExWhy
{
    public ExWhyBridge() : base(20 , 13, "Bridge")
    {
        worldData = new char[20, 13]{
            {'t','t','t','d','w','w','w','w','w','g','t','t','t' },
            {'t','t','t','d','w','w','w','w','w','g','t','t','t' },
            {'t','t','t','d','w','w','w','w','w','g','t','t','t' },
            {'d','d','d','d','w','w','w','w','w','g','d','d','d' },
            {'d','d','d','d','w','w','w','w','w','g','d','d','d' },
            {'g','g','g','g','w','w','w','w','w','g','g','g','g' },
            {'g','g','g','g','w','w','w','w','w','g','g','g','g' },
            {'d','d','d','g','w','w','w','w','w','g','g','g','g' },
            {'d','d','d','d','d','b','b','b','d','d','d','d','d' },
            {'d','d','d','d','d','b','b','b','d','d','d','d','d' },
            {'d','g','g','g','w','w','w','w','w','g','g','d','g' },
            {'g','g','g','g','w','w','w','w','w','g','g','g','g' },
            {'g','g','g','g','w','w','w','w','w','g','g','g','g' },
            {'g','g','g','g','w','w','w','w','w','g','g','g','g' },
            {'g','g','g','g','w','w','w','w','w','g','g','g','g' },
            {'g','g','g','g','w','w','w','w','w','g','g','g','g' },
            {'d','d','d','d','w','w','w','w','w','g','d','d','d' },
            {'t','t','t','d','w','w','w','w','w','g','t','t','t' },
            {'t','t','t','d','w','w','w','w','w','g','t','t','t' },
            {'t','t','t','d','w','w','w','w','w','g','t','t','t' },
             };

        initiateCells();
    }
    protected override void eventTest(int eventIndex){}

    protected override void instantiateCells()
    {
        foreach (ExWhyCell cell in gridCells)
        {
            switch (cell.cellType)
            {
                case 'w':
                    instantiateCell(cell, false, 4, 4);
                    cell.resourceName = "Water";
                    cell.makeAltable();
                    cell.setAltRate(70);
                    cell.setCellBuff(new GenericCellBuff(0, 0, 0));
                    break;
                    
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

                case 'b':
                    instantiateCell(cell, true, 0, 0);
                    cell.resourceName = "Bridge";
                    cell.setCellBuff(new GenericCellBuff(0, 0, 0));
                    break;

                case 't':
                    instantiateCell(cell, false, 2, 2);
                    cell.resourceName = "Forest";
                    cell.makeAltable();
                    cell.setAltRate(50);
                    cell.setCellBuff(new GenericCellBuff(0, 0, 0));
                    break;
            }
        }
    }
    protected override void instantiateEvents(){}
}
