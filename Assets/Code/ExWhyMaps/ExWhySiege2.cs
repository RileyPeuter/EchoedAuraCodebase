using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExWhySiege2 : ExWhy
{
    public ExWhySiege2() : base(13 , 17, "Siege")
    {        //t=Trees, , v = dark, b = battlement, s = scafholding
        worldData = new char[13 ,17]
        {
            {'v','v','v', 'v', 'v' , 't', 't', 't', 't', 'v','v','v', 'v', 'v', 'v', 'v','v'},
            {'v','v','t', 't', 't' , 't', 't', 't', 't', 'v','v','v', 'v', 'v', 'v', 'v','v'},
            {'v','v','t', 'v', 'v' , 'v', 'v', 'v', 'v', 'v','v','v', 'v', 'v', 'v', 'v','v'},
            {'v','v','v', 'v', 'v' , 'v', 'v', 'v', 'v', 'v','v','v', 'v', 'v', 'v', 'v','v'},
            {'v','b','b', 'b', 'v' , 'v', 's', 'v', 'v', 's','v','v', 'b', 'b', 'b', 'b','v'},
            {'v','b','b', 'b', 'b'  ,'b', 'b', 'b'  ,'b','b','b','b', 'b', 'b', 'b', 'b','v'},
            {'v','b','b', 'b', 's' , 's', 's', 'v', 'v', 's','s','s', 'b', 'b', 'b', 'b','v'},
            {'v','b','b', 'b', 'v' , 'v', 'v', 'v', 'v', 'v','v','v', 'b', 'b', 'b', 'b','v'},
            {'v','b','b', 'b', 'v' , 'v', 'v', 'v', 'v', 'v','v','v', 'b', 'b', 'b', 'b','v'},
            {'v','b','b', 'b', 'v' , 'v', 'v', 'v', 'v', 'v','v','v', 'b', 'b', 'b', 'b','v'},
            {'v','b','b', 'b', 'v' , 'v', 'v', 'v', 'v', 'v','v','v', 'b', 'b', 'b', 'b','v'},
            {'v','b','b', 'b', 'v' , 'v', 'v', 'v', 'v', 'v','v','v', 'b', 'b', 'b', 'b','v'},
            {'v','b','b', 'b', 'v' , 'v', 'v', 'v', 'v', 'v','v','v', 'b', 'b', 'b', 'b','v'}
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

    // Start is called before the first frame update
    void Start()
    {
        

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
