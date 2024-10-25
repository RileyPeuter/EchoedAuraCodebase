using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExWhySiege1 : ExWhy
{
    //He wrote this script on a pepsi max soda shop
    //Wishing on a rope, or to see that glizzy go pop
    public ExWhySiege1() : base(15, 12, "Siege")
    {
        //t=Trees, g = grass, d = dirt, b = battlement, w = wall
        worldData = new char[15, 12]
        {

            {'t','t','t', 't', 'g' , 'g', 'g', 'd', 'd', 't', 't','t'},
            {'t','t','t', 'g', 'g' , 'g', 'g', 'd', 'd', 't', 't','t'},
            {'t','t','t', 'g', 't' , 't', 't', 't', 'd', 'g', 't','t'},
            {'g','g','g', 'g', 't' , 't', 't', 'g', 'd', 'g', 'g','t'},
            {'d','d','g', 't', 't' , 't', 't', 'g', 'd', 't', 't','t'},
            {'d','t','t', 'g', 't' , 't', 'd', 'g', 'd', 't', 't','t'},
            {'g','d','d', 'g', 't' , 't', 't', 't', 'd', 't', 't','t'},
            {'t','t','t', 'g', 'g' , 't', 'g', 'd', 'd', 'g', 't','t'},
            {'g','g','g', 'g', 'g' , 'g', 'g', 'g', 'd', 'd', 'd','d'},
            {'g','g','g', 'g', 'g' , 'g', 'g', 'g', 'd', 'd', 'd','d'},
            {'g','g','d', 'g', 'd' , 'g', 'd', 'g', 'd', 'd', 'g','g'},
            {'g','g','g', 'g', 'd' , 'g', 'd', 'w', 'w', 'w', 'b','d'},
            {'g','g','d', 'w', 'w' , 'w', 'b', 'w', 'w', 'w', 'b','d'},
            {'g','t','d', 'w', 'w' , 'w', 'b', 'w', 'w', 'w', 'b','d'},
            {'g','g','d', 'w', 'w' , 'w', 'b', 'w', 'w', 'w', 'b','d'}

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

                case 'w':
                    instantiateCell(cell, false, 1, 1);
                    cell.resourceName = "BattlementWalls";
                    // cell.makeAltable();
                    // cell.setAltRate(50);
                    cell.setCellBuff(new GenericCellBuff(0, 0, 0));
                    break;

                case 't':
                    instantiateCell(cell, false, 5, 5);
                    cell.resourceName = "Forest";
                   // cell.makeAltable();
                   // cell.setAltRate(50);
                    cell.setCellBuff(new GenericCellBuff(0, 0, 0));
                break;

                case 'd':
                    instantiateCell(cell, true, 4, 4);
                    cell.resourceName = "Dirt";
                    //cell.makeAltable();
                    //cell.setAltRate(30);
                    cell.setCellBuff(new GenericCellBuff(0, 4, 0));
                    break;

                case 'g':
                    instantiateCell(cell, true, 6, 6);
                    cell.resourceName = "Grass";
                   // cell.makeAltable();
                   // cell.setAltRate(30);
                    cell.setCellBuff(new GenericCellBuff(3, 0, -2));
                    //cell.animatable = true;
                    break;

                case 'b':
                    instantiateCell(cell, false, 0, 0);
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
