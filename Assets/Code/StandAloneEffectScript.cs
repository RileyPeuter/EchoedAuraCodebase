using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandAloneEffectScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameObject.Destroy(this.gameObject, 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
   //     this.transform.Translate(Vector3.right * Time.deltaTime);   
    }
}
