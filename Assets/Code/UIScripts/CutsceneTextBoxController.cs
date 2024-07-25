using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CutsceneTextBoxController : Window
{

    TextMesh speaker;
    TextMesh dialogue;
    SpriteRenderer speakerSprite;


    public void setText(string spkr, string dialge , Sprite speakerSpr)
    {
        speaker.text = spkr; 
        dialogue.text = dialge;
        if (speakerSprite != null)
        {
            speakerSprite.sprite = speakerSpr;
        }

    }
    
    public void initialize()
    {
        foreach (TextMesh x in gameObject.GetComponentsInChildren<TextMesh>())
        {
            if (x.name == "uI_DialgoueSpeaker_TextMesh")
            {
                speaker = x;
            }

            if (x.name == "uI_Dialogue_TextMesh")
            {
                dialogue = x;
            }
        }

        speakerSprite = GameObject.Find("uI_Speaker_Sprite").GetComponent<SpriteRenderer>();


    }

    // Start is called before the first frame update
    void Start()
    {
        initialize();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
