using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageTextController : MonoBehaviour
{
    
    public void initialize(string text, Sprite  image = null)
    {
        this.GetComponentInChildren<TextMesh>().text = text;
        this.GetComponentInChildren<SpriteRenderer>().sprite = image;
    }

    void Update()
            
    {
        gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 4 * Time.deltaTime, gameObject.transform.position.z);

    }
    void Start()
    {
        GameObject.Destroy(gameObject, 2f);

    }
}
