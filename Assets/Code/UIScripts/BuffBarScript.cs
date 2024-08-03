using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuffBarScript : MonoBehaviour
{
    //###MemberVariables###
    int xOffset = 0;
    GameObject buffPrefab;
    Dictionary<GameObject, Buff> buffObjects;

    //###Utlities###
    public void addBuff(Buff buff)
    {
        if (!buff.visible)
        {
            return;
        }

        GameObject go = GameObject.Instantiate(buffPrefab, this.gameObject.transform);
        go.GetComponent<Image>().sprite = buff.getSprite();
        buffObjects.Add(go , buff);

        foreach(Text text in gameObject.GetComponentsInChildren<Text>())
        {
            if(text.name == "uI_Duration_Text")
            {
                text.text = buff.getDuration().ToString();
            }
            if(text.name == "uI_Magnitude_Text")
            {
                text.text = buff.getMagnitude().ToString();
            }
        }
    }

    public void updateBuffList()
    {
        foreach (KeyValuePair<GameObject,Buff> go in buffObjects)
        {
            go.Key.GetComponent<RectTransform>().anchoredPosition = new Vector3(25 + xOffset, 0, 0);
            xOffset = xOffset + 40;
            go.Key.GetComponent<BuffDisplayScript>().initialize(go.Value);
        }
        xOffset = 0;
    }

    public void tryAddBuff(Buff buff)
    {
        if(buffObjects is null)
        {
            buffObjects = new Dictionary<GameObject, Buff>();
            buffPrefab = Resources.Load<GameObject>("UIElements/uI_BuffIcon_Image");

        }

        foreach (Buff bff in buffObjects.Values)
        {
            if(buff == bff)
            {
                return;
            }
        }
        addBuff(buff);
        updateBuffList();
    }
}
