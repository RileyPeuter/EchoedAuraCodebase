using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class StandOffAnimationListener : MonoBehaviour
{
    StandOffController SOC;
    public bool left = false;
    public void test()
    {
        
        SOC.animationFinished(left);
    }

    public void initialize()
    {

        SOC = GameObject.Find("S(Clone)").GetComponent<StandOffController>();
        print(SOC.name);
        Animator ani = gameObject.GetComponent<Animator>();

        foreach (AnimationClip aniClip in ani.runtimeAnimatorController.animationClips)
        {
            if (aniClip.events.Length == 0)
            {
                AnimationEvent kappa = new AnimationEvent();
                kappa.time = aniClip.length;
                kappa.functionName = "test";
                aniClip.AddEvent(kappa);
            }


        }
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
