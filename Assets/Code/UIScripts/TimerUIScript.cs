using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

    public void initialize(BattleController BatCont)
    {
        BC = BatCont;
    }

    public void updateTacticalPoints(int nTacticalPoints)
    {

        GetComponentsInChildren<Text>().ToList().Find(x => x.name == "uI_TacticsPoints_Text").text = (":  " + nTacticalPoints.ToString());
    }

    public void updateTime(int nTime)
    {
        GetComponentsInChildren<Text>().ToList().Find(x => x.name == "uI_Timer_Text").text = nTime.ToString();
    }

    public void endTurn()
    {
        if (BC.hasControl)
        {
            BC.endTurn();
        }
    }
}
