using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscapeMenuButtonController : MonoBehaviour
{
    public void click()
    {
        GlobalGameController.GGC.escapeMenuCheck(true);
    }
}
