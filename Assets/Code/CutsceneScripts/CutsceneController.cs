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

            case 3:
                this.gameObject.AddComponent<DorciaMeetCutscene>();
                break;

            case 4:
                this.gameObject.AddComponent<PostPrologueCutscene>();
                break;

            case 5:
                this.gameObject.AddComponent<Siege1BattleController>();
            break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
