using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileAnimationLooper : MonoBehaviour
{
    public float reset = 0;
    float timer = 1f;
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        timer = timer - Time.deltaTime;

        if (timer < 0)
        {
            timer = UnityEngine.Random.Range(3, 5) + reset;
            animator.SetTrigger("Trigger");
        }
    }
}
