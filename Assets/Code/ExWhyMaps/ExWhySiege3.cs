using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExWhySiege3 : ExWhy
{
    public ExWhySiege3() : base(13, 28, "Siege")
    {        //t=Trees, , v = dark, b = battlement, s = scafholding
        worldData = new char[13, 28]
        {
            {'t','t','t','t','t','t','t','g','t','t','t','t','t','t','g','t','t','g','t','t','g','g','g','g','w','w','w','b'},
            {'t','t','t','t','t','t','g','g','t','t','t','t','g','t','g','t','g','g','t','t','g','g','g','g','w','w','w','b'},
            {'t','t','t','t','t','t','g','t','t','t','t','t','g','g','g','g','g','g','t','t','g','g','g','g','w','w','w','b'},
            {'t','t','t','t','t','t','g','t','g','g','g','g','g','g','g','g','g','g','g','t','g','g','g','g','w','w','w','b'},
            {'t','t','t','t','t','t','g','g','g','t','t','g','g','g','g','g','t','t','g','g','g','g','g','g','w','w','w','b'},
            {'t','t','t','t','t','t','g','g','t','t','t','t','g','t','g','t','t','t','g','t','g','g','g','g','w','w','w','b'},
            {'t','t','t','t','t','g','g','t','t','t','t','t','g','t','g','g','t','t','g','g','g','g','g','g','w','w','w','b'},
            {'t','g','g','g','g','g','t','t','t','t','t','t','t','t','g','g','g','g','g','g','g','g','g','g','w','w','w','b'},
            {'t','t','t','t','g','t','t','t','t','t','t','t','t','t','g','g','t','t','g','g','g','g','g','g','w','w','w','b'},
            {'t','t','t','t','g','g','t','t','t','t','t','t','t','g','g','g','t','t','g','t','g','g','g','g','w','w','w','b'},
            {'t','t','t','t','g','g','g','g','g','g','g','g','g','g','g','t','t','t','t','t','t','g','g','g','w','w','w','b'},
            {'t','t','t','t','t','t','t','t','t','t','t','t','g','t','g','g','g','g','t','t','t','g','g','g','w','w','w','b'},
            {'t','t','t','t','t','t','t','t','t','t','t','t','t','t','g','g','g','g','g','t','t','g','g','g','w','w','w','b'}
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


                case 't':
                    instantiateCell(cell, false, 5, 5);
                    cell.resourceName = "Forest";
                    // cell.makeAltable();
                    // cell.setAltRate(50);
                    cell.setCellBuff(new GenericCellBuff(0, 0, 0));
                    break;

                case 'b':
                    instantiateCell(cell, true, 3, 3);
                    cell.setCellBuff(new GenericCellBuff(0, 8, 0));
                    cell.resourceName = "Battlement";
                    break;

                case 'w':
                    instantiateCell(cell, false, 1, 1);
                    cell.resourceName = "BattlementWalls";
                    // cell.makeAltable();
                    // cell.setAltRate(50);
                    cell.setCellBuff(new GenericCellBuff(0, 0, 0));
                    break;

                case 'g':
                    instantiateCell(cell, true, 6, 6);
                    cell.resourceName = "Grass";
                    // cell.makeAltable();
                    // cell.setAltRate(30);
                    cell.setCellBuff(new GenericCellBuff(3, 0, -2));
                    //cell.animatable = true;
                    break;
            }
        }
    }

    protected override void instantiateEvents()
    {

    }
}