using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChunkController : MonoBehaviour
{
    Image image;

    // Start is called before the first frame update
    void Start()
    {
        GameObject.Destroy(gameObject, 1f);
        image = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        image.color -= new Color(0.2f * Time.deltaTime, 0, 0, 0.2f * Time.deltaTime);
        gameObject.transform.position += new Vector3(0, 60 * Time.deltaTime, 0);
    }
}
