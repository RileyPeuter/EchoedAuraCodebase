using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour
{

    public Character activeCharacter;

    AttackAttempt aatmp;

    GlobalGameController GGC;

BattleController battleController;

     public string GetResourceName()
    {
        return gridObject.resourceName;
    }

    public void initiateMap()
    {


    }

    public void SpawnCharacters()
    {

    }

    public void kapp(int mx, int my)
    {
        print(mx);
        print(my);

    }
    
    public ExWhy gridObject;

    public ExWhyCell getMouseCell(float cellSizeMultiplier = 4f)
    {
        int x;
        int y;
        Vector3 mousePoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        x = Mathf.RoundToInt((mousePoint.x) / cellSizeMultiplier);
        y = Mathf.RoundToInt((mousePoint.y) / cellSizeMultiplier);

        if(y < 0)
        {
            y = 0;
        }
        if(x < 0)
        {
            x = 0;
        }
        if (y >= gridObject.yMax)
        {
            y = gridObject.yMax - 1;
        }
        if(x >= gridObject.xMax)
        {
            x = gridObject.xMax - 1;
        }
  
        return gridObject.gridCells[x, y];
    }

    void Start()
    {
        GGC = GlobalGameController.GGC;
        gridObject = GGC.mapToBeLoaded;
        gridObject.drawGrid();
        setBattleController(GGC.BCToBeLoaded);
        initiateMap();
    }

    void setBattleController(int bcIn)
    {
        switch (bcIn)
        {
            case 0:
                battleController =  this.gameObject.AddComponent<TrainingGroundBattleController>();
                break;
            case 1:
                battleController = this.gameObject.AddComponent<TownBattleController>();
                break;
            case 2:
                battleController = this.gameObject.AddComponent<BridgeBattleController>();
                break;
            case 3:
                battleController = this.gameObject.AddComponent<RulessDeogBattleController>();
            break;
            case 4:
                battleController = this.gameObject.AddComponent<KimsTrainingBattleController>();
            break;
            case 5:
                battleController = this.gameObject.AddComponent<DorciaAssistanceBattleController>();
            break;
            case 6:
                battleController = this.gameObject.AddComponent<Siege1BattleController>();
             break;
            case 7:
                battleController = this.gameObject.AddComponent<Siege2BattleController>();
            break;


        }
        if(GGC.getMissionCharacters() != null)
        {
            List<StoredCharacterObject> SCOs = new List<StoredCharacterObject>();
            foreach (StoredCharacterObject character in GGC.getMissionCharacters())
            {
                if (character != null)
                {
                    SCOs.Add(character);
                }
            }
            battleController.addCharactersToLoad(SCOs);
        }
    }

    void Update()
    {

    }

    

}
