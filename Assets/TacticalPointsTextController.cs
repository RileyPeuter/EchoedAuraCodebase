using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TacticalPointsTextController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<AudioSource>().Play();
        GameObject.Destroy(gameObject, 2f);        
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position += new Vector3(0, -4 * Time.deltaTime, 0);
    }
}
