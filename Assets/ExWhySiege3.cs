using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExWhySiege3 : ExWhy
{
    public ExWhySiege3() : base(13, 17, "Siege")
    {        //t=Trees, , v = dark, b = battlement, s = scafholding
        worldData = new char[13, 17]
        {
            {'v','b','b', 'b', 'v' , 'v', 's', 'v', 't', 'g','g','g', 'g', 'w', 'w', 'w','b'},
            {'v','v','t', 't', 't' , 't', 't', 't', 't', 'g','g','g', 'g', 'w', 'w', 'w','b'},
            {'v','v','t', 'v', 'v' , 'v', 'v', 'v', 't', 'g','g','g', 'g', 'w', 'w', 'w','b'},
            {'v','v','v', 'v', 'v' , 'v', 'v', 'v', 't', 'g','g','g', 'g', 'w', 'w', 'w','b'},
            {'v','b','b', 'b', 's' , 's', 's', 'v', 'g', 'g','g','g', 'g', 'w', 'w', 'w','b'},
            {'v','b','b', 'b', 'b'  ,'b', 'b', 'b', 't','g','g','g', 'g', 'w', 'w', 'w','b'},
            {'v','b','b', 'b', 'v' , 'v', 'v', 'v', 'g', 'g','g','g', 'g', 'w', 'w', 'w','b'},
            {'v','b','b', 'b', 'v' , 'v', 'v', 'v', 'g', 'g','g','g', 'g', 'w', 'w', 'w','b'},
            {'v','b','b', 'b', 'v' , 'v', 'v', 'v', 'g', 'g','g','g', 'g', 'w', 'w', 'w','b'},
            {'v','v','v', 'v', 'v' , 't', 't', 't', 't', 'g','g','g', 'g', 'w', 'w', 'w','b'},
            {'v','b','b', 'b', 'v' , 'v', 'v', 'v', 't', 't','g','g', 'g', 'w', 'w', 'w','b'},
            {'v','b','b', 'b', 'v' , 'v', 'v', 'v', 't', 't','g','g', 'g', 'w', 'w', 'w','b'},
            {'v','b','b', 'b', 'v' , 'v', 'v', 'v', 't', 't','g','g', 'g', 'w', 'w', 'w','b'}
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

                case 'v':
                    instantiateCell(cell, false, 2, 2);
                    cell.resourceName = "Dark";
                    //cell.makeAltable();
                    //cell.setAltRate(30);
                    cell.setCellBuff(new GenericCellBuff(0, 0, 0));
                    break;

                case 'b':
                    instantiateCell(cell, true, 3, 3);
                    cell.setCellBuff(new GenericCellBuff(0, 8, 0));
                    cell.resourceName = "DarkBattlement";
                    break;


                case 's':
                    instantiateCell(cell, true, 7, 7);
                    cell.setCellBuff(new GenericCellBuff(-5, -5, -5));
                    cell.resourceName = "Scaffolding";
                    break;

            }
        }
    }

    protected override void instantiateEvents()
    {

    }
}