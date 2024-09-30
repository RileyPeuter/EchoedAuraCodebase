using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverVisualController : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    float colourVal;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        colourVal = Mathf.Abs(Mathf.Sin(1 - UIController.UIBreatheTicker));
        spriteRenderer.color = new Color(colourVal, colourVal, colourVal);
    }
}
