using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorVisualController : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    float colourVal;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    
    public void setColour(Color colour)
    {

    }

    // Update is called once per frame
    void Update()
    {
        colourVal = Mathf.Abs(Mathf.Sin(BattleUIController.UIBreatheTicker));
        spriteRenderer.color = new Color(colourVal, colourVal, colourVal);
    }
}
