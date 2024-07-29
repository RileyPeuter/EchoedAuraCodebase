using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerUIScript : MonoBehaviour
{

    GameObject endTurnObject;
    BattleController BC;

    // Start is called before the first frame update

    void Start()
    {
        foreach (Button GE in GetComponentsInChildren<Button>())
        {
            if (GE.name == "uI_EndTurn_Button")
            {
                endTurnObject = GE.gameObject;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void initialize(BattleController BatCont)
    {
        BC = BatCont;
    }

    public void endTurn()
    {
        if (BC.hasControl)
        {
            BC.endTurn();
        }
    }
}
