using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlowingScript : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        float updater = (UIController.UIBreatheTicker);
        spriteRenderer.color = new Color(255, 255, 255 , updater);
        transform.localScale = new Vector3(1 + (updater * 0.3f), 1 + (updater * 0.3f));

    }
}
