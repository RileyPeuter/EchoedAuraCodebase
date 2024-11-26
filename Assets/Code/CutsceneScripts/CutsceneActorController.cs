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
        if (movement)
        {
            ActorAnimator.SetBool("moving", true);
        }
         moving = true;
    }

    public void setOverrideAnimation(int aniID)
    {
        ActorAnimator.SetInteger("aniOverrideInt", aniID);
        ActorAnimator.SetTrigger("aniOverride");
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

    public void initialize()
    {
        ActorAnimator = GetComponent<Animator>();
    }

    //###UnityMessages###

    private void Start()
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
