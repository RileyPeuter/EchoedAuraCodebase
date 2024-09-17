using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExWhyRulessDog : ExWhy
{
    public ExWhyRulessDog() : base(20, 13, "Forest")
    {
        worldData = new char[20, 13]{
            {'t','t','t','t','t','t','t','t','t','g','t','t','t' },
            {'t','t','t','t','t','t','t','t','t','g','t','t','t' },
            {'t','t','t','t','t','t','t','t','t','g','t','t','t' },
            {'t','t','t','d','g','g','g','g','g','g','t','t','t' },
            {'t','t','g','d','d','d','d','d','d','d','t','t','t' },
            {'t','t','g','d','w','t','t','t','t','t','t','t','t' },
            {'t','t','g','d','d','t','t','t','t','t','t','t','t' },
            {'t','t','d','d','d','t','t','t','t','t','t','t','t' },
            {'t','g','d','g','d','t','t','d','d','t','t','t','t' },
            {'t','g','d','g','d','d','d','d','d','t','t','t','t' },
            {'t','g','d','g','d','t','t','d','g','t','t','t','t' },
            {'t','t','d','d','d','t','t','t','t','t','t','t','t' },
            {'t','t','g','d','t','t','t','t','t','t','t','t','t' },
            {'t','t','g','d','t','t','t','t','t','t','t','t','t' },
            {'t','t','t','w','t','t','t','t','t','t','t','t','t' },
            {'t','t','t','g','t','t','t','t','t','t','t','t','t' },
            {'t','t','t','d','g','d','d','d','d','d','t','t','t' },
            {'t','t','t','t','t','t','t','g','t','d','t','t','t' },
            {'t','t','t','t','t','t','t','t','t','d','t','t','t' },
            {'t','t','t','t','t','t','t','t','t','d','t','t','t' },
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
                    cell.setAltRate(10);
                    cell.setCellBuff(new GenericCellBuff(3, 0, -2));
                    break;

                case 'd':
                    instantiateCell(cell, true, 1, 1);
                    cell.resourceName = "Dirt";
                    cell.makeAltable();
                    cell.setAltRate(20);
                    cell.setCellBuff(new GenericCellBuff(0, 4, 0));
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

                case 'w':
                    instantiateCell(cell, true, 5, 5);
                    cell.resourceName = "WoodenBoards";
                    cell.setCellBuff(new GenericCellBuff(-1, -2, 5));


                break;
            }
        }
    }

    protected override void instantiateEvents()
    {
    
    }
}
