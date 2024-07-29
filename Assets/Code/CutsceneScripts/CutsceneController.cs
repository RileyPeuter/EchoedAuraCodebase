using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        loadCutscene(GlobalGameController.GGC.CutsceneToBeLoaded);
    }

    void loadCutscene(int cutSceneID)
    {
        switch (cutSceneID)
        {
            case 1:
                this.gameObject.AddComponent<IntroCutscene>();
            break;

            case 2:
                this.gameObject.AddComponent<RulessDogCutscene>();
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
