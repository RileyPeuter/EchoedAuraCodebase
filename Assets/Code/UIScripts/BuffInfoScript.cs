using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuffInfoScript : MonoBehaviour
{
    Buff buff;
    RectTransform trans;

    public void initialize(Buff buf)
    {
        buff = buf;
        trans = GetComponent<RectTransform>();
    }
    // Start is called before the first frame update
    void Start()
    {
        foreach(Text text in GetComponentsInChildren<Text>())
        {
            switch (text.name)
            {
                case "uI_Name_Text":
                    text.text = buff.name;
                    break;


                case "uI_Description_Text":
                    text.text = buff.description;
                    break;

                case "uI_Intensity_Text":
                    text.text = buff.magnitude.ToString();
                    break;

                case "uI_Duration_Text":
                    text.text = buff.duration.ToString();
                    break;
                    
            }
            trans.position = Input.mousePosition + new Vector3(0, -60, 0);

        }


        foreach (Image image in GetComponentsInChildren<Image>())
        {
            if(image.name == "uI_Icon_Image")
            {
                image.sprite = Resources.Load<Sprite>("BuffIcons/" + buff.getResourceName());
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 mousePos = Input.mousePosition;
        if (mousePos.x < Screen.width / 2)
        {
            trans.position = Input.mousePosition + new Vector3( 100, -60, 0);
        }
        else
        {
            trans.position = Input.mousePosition + new Vector3(-100, -60, 0);
        }

    }
    }
