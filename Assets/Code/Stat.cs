using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum stat
{
    strength,
    vitality,
    speed,
    precision,
    focus,
    ingenuity
};


public class Stat
{
    public stat statType;
    int statBase;
    int additions = 0;
    int multiplications = 1;

    public Stat(stat statT, int sttBse)
    {
        statType = statT;
        statBase = sttBse;
    }

    public void incrementAddition()
    {
        additions++;
    }

    public Stat()
    {

    }

    
    public int calculateStat()
    {
        return statBase + additions * multiplications;
    }
}
