using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeVisualController : MonoBehaviour
{
    SpriteRenderer spriteRednerer;
    float colourVal;
    float offset;

    float baseR = 0;
    float baseG = 0.15f;
    float baseB = 0.6f;

    // Start is called before the first frame update

    public void setOffset(float os, Color color)
    {
       baseR = color.r - 0.1f;
       baseG = color.g - 0.1f;
       baseB = color.b - 0.1f;


        offset = os;
    }

    public void setOffset(float os)
    {
        offset = os;
    }

    void Start()
    {
        spriteRednerer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        colourVal = Mathf.Abs(Mathf.Sin(UIController.UIBreatheTicker +  offset)) * 0.5f;
        spriteRednerer.color = new Color(baseR, baseG, baseB, colourVal + 0.2f);
    }
}
