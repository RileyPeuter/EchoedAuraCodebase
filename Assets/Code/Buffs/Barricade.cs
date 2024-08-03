using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barricade : Buff
{

    public Barricade(Character nTarget, int hardness) : base(nTarget)
    {
        magnitude = hardness;
        duration = 999;
        resourceName = "Barricade";
        name = "Barricade";
        description = "Something to stop the advancement of enemies \nMay be possible to repair via a Tactic";

    }
    public override void cleanup()
    {

    }

    public override void start()
    {

    }

    public override void tick()
    {

    }
}
