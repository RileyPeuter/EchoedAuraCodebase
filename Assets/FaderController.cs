using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaderController : MonoBehaviour
{
    Color initial;
    Color transition;
    float rate = 1;

    SpriteRenderer renderer;

    public void initialize(Color nInitial, Color nTransition, float nRate = 1)
    {

        initial = nInitial;
        transition = nTransition;
        rate = nRate;
        renderer = GetComponent<SpriteRenderer>();
        renderer.color = nInitial;
}

    public void Update()
    {
        renderer.color += transition * rate * Time.deltaTime;
    }

}
