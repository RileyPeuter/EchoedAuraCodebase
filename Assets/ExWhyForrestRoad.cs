using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExWhyForrestRoad : ExWhy
{
    
        public ExWhyForrestRoad() :base(10, 10, "TrainingGround")
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

    protected override void eventTest(int eventIndex)
    {

    }

    protected override void instantiateCells()
    {

    }

    protected override void instantiateEvents()
    {
    
    }
}
