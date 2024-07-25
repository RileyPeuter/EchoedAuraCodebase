using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackAttemptReactionScript : AttackAttemptUIController
{
    StandOffController SOF;
    // Start is called before the first frame update
    void Start()
    {
        setValues();
    }

    public void setSOF(StandOffController StndOfCon)
    {
        SOF = StndOfCon;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GetReaction(int reUsed)
    {
        Destroy(this.gameObject, 0.5f);
        switch (reUsed)
        {
            case 0:
                attackAttempt.react(reactionType.Dodge);
                break;

            case 1:
                attackAttempt.react(reactionType.Block);
                break;
            case 2:
                attackAttempt.react(reactionType.Parry);
                break;
        }
        return;
    }
}
