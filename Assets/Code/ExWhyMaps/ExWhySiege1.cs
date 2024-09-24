using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExWhySiege1 : ExWhy
{
    //He wrote this script on a pepsi max soda shop
    //Wishing on a rope, or to see that glizzy go pop
    public ExWhySiege1() : base(10, 10, "Siege")
    {
        //t=Trees, g = grass, d = dirt, b = battlement, w = wall
        worldData = new char[10, 10]
        {
            {'t', 't', 't' , 'g', 'g', 'g', 'd', 'd', 'd','d'},
            {'t', 't', 't' , 'g', 'g', 'g', 'd', 'b', 'b','b'},
            {'t', 't', 't' , 'g', 'g', 'g', 'd', 'w', 'w','w'},
            {'t', 't', 't' , 'g', 'g', 'g', 'd', 'w', 'w','w'},
            {'t', 't', 't' , 'g', 'g', 'g', 'd', 'w', 'w','w'},
            {'t', 't', 't' , 'g', 'g', 'g', 'd', 'd', 'b','b'},
            {'t', 't', 't' , 'g', 'g', 'g', 'd', 'd', 'w','w'},
            {'t', 't', 't' , 'g', 'g', 'g', 'd', 'd', 'w','w'},
            {'t', 't', 't' , 'g', 'g', 'g', 'd', 'd', 'w','w'},
            {'t', 't', 't' , 'g', 'g', 'g', 'd', 'd', 'd','d'}

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
                    instantiateCell(cell, false, 2, 2);
                    cell.resourceName = "Forest";
                    cell.makeAltable();
                    cell.setAltRate(50);
                    cell.setCellBuff(new GenericCellBuff(0, 0, 0));
                break;

                case 'd':
                    instantiateCell(cell, true, 0, 0);
                    cell.resourceName = "Dirt";
                    cell.makeAltable();
                    cell.setAltRate(30);
                    cell.setCellBuff(new GenericCellBuff(0, 4, 0));
                    break;

                case 'g':
                    instantiateCell(cell, true, 1, 1);
                    cell.resourceName = "Grass";
                    cell.makeAltable();
                    cell.setAltRate(30);
                    cell.setCellBuff(new GenericCellBuff(3, 0, -2));
                    cell.animatable = true;
                    break;

                case 'w':
                    instantiateCell(cell, true, 2, 2);
                    cell.resourceName = "Wall";
                    cell.setCellBuff(new GenericCellBuff(1, 2, 5));
                    break;

                case 'b':
                    instantiateCell(cell, false, 3, 3);
                    cell.setCellBuff(new GenericCellBuff(0, 8, 0));
                    cell.resourceName = "Battlement";
                    break;
            }
        }
    }
    protected override void instantiateEvents()
    {
        
    }
}
