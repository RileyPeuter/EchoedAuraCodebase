using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
        if (speakerSprite is null)
        {
            speakerSprite = GameObject.Find("uI_Speaker_Sprite").GetComponent<SpriteRenderer>();
            if(speakerSprite is null) { return; }
        }

        speakerSprite.sprite = speakerSpr;
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

        speakerSprite = GetComponentsInChildren<SpriteRenderer>().ToList().Find (x => x.gameObject.name == "uI_Speaker_Sprite"); 
    }

    public void alterSize(float amount)
    {
        //foreach(Transform trans in GetComponentsInChildren<Transform>())
        //{
        //  trans.localScale += new Vector3(amount, amount, 0); 
        //}

        this.transform.localScale = new Vector3(0.6f, 0.6f, 0);
        this.transform.position += new Vector3(0, 1, 0);
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
