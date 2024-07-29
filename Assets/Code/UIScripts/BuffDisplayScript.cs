using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BuffDisplayScript : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    Buff buff;
    GameObject hoverObject = null;




    public void OnPointerExit(PointerEventData eventData)
    {

        Destroy(hoverObject);
        hoverObject = null;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (hoverObject is null)
        {
            hoverObject = GameObject.Instantiate(Resources.Load<GameObject>("UIElements/uI_BuffInfo_Panel"), GameObject.Find("Canvas").transform);
            hoverObject.GetComponent<BuffInfoScript>().initialize(buff);
        }
    }


    public void initialize(Buff buf)
    {
        buff = buf;
    }
    // Start is called before the first frame update
    void Start()
    {
            
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
