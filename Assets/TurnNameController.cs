using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnNameController : MonoBehaviour
{

    public void initialize(string name)
    {
        GetComponentInChildren<Text>().text = name;
    }
    // Start is called before the first frame update
    void Start()
    {
        GameObject.Destroy(this.gameObject, 2);
    }

    // Update is called once per frame
    void Update()
    {
        foreach(Image image in GetComponentsInChildren<Image>())
        {
            image.color = (image.color - new Color(0, 0, 0, 0.5f * Time.deltaTime));
        }
        Text text = GetComponentInChildren<Text>();
        text.color = (text.color - new Color(0, 0, 0, 0.5f * Time.deltaTime));

    }
}
