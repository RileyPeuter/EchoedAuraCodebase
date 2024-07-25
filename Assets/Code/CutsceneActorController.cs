using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneActorController : MonoBehaviour
{
    //###MemberVariables###
    Animator ActorAnimator;
    Vector2 moveDirection;
    float moveSpeed;
    float moveDuration;
    bool moving;

    //###Utilities###
    public void SetMove(Vector2 direction, float speed, float duration, bool movement = true)
    {
        moveDirection = direction;
        moveSpeed = speed;
        moveDuration = duration;
        ActorAnimator.SetBool("moving", true);
        moving = true;
    }

    void move()
    {
        this.transform.Translate(moveDirection * moveSpeed * Time.deltaTime);        
        if (moveDuration < 0)
        {
            moving = false;

            ActorAnimator.SetBool("moving", false);
        }
    }

    //###UnityMessages###
    void Start()
    {
        ActorAnimator = GetComponent<Animator>(); 
    }

    void Update()
    {
        moveDuration = moveDuration - Time.deltaTime;
        if (moving)
        {
            move();
        }
    }
}
