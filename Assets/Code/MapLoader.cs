using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapLoader : MonoBehaviour
{
    public List<GameObject> loadCharacters(List<string> characterNames){
        List<GameObject> output = new List<GameObject>();
        foreach (string name in characterNames)
        {
            output.Add(Resources.Load<GameObject>("MapCharacters/" + name));
        }
        return output;
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
