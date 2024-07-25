using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BuffType
{
    Debuff, 
    Buff,
    OnHit,
    OnTakeHit,
    Misc, 
    Story
}

public abstract class Buff
{
    //Counter for buff IDs
    static int buffID = 0;

    public static int getID()
    {
        buffID += 1;
        return buffID;
    }


    //###Member Variables###

    protected Character target;
    public int magnitude;
    public int duration;
    protected int id;

    protected string resourceName;

    //###Getter###

    public int getDuration()
    {
        return duration;
    }
    //pop pop
    public int getMagnitude()
    {
        return magnitude;
    }

    public string getResourceName()
    {
        return resourceName;
    }

    public Sprite getSprite()
    {
        return Resources.Load<Sprite>("BuffIcons/" + resourceName);
    }

    public string name { get; set; }
    public string description { get; set; }

    //###Constructors###
    public Buff(Character nTarget)
    {
        target = nTarget;
    }
    public abstract void start();
    public abstract void tick();
    public abstract void cleanup();
}
