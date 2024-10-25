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
        gameObject.transform.position += new Vector3(0, 4 * Time.deltaTime, 0);
    }

    void Start()
    {
      this.GetComponent<Renderer>().sortingLayerName = "WorldUI";
      GameObject.Destroy(gameObject, 2f);

    }
}
