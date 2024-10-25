using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class ExWhyCellField 
{
    protected List<ExWhyCell>cells;
    protected List<GameObject> visualObjects;

    protected GameObject visualPrefab;
    public bool visible;
    protected Color prefabColour;

    protected List<Sprite> visualSprites;

    protected ExWhy map;

    public List<ExWhyCell> getCells()
    {
        return cells;
    }

    public void setPrefab(string prefabPath)
    {
        visualPrefab = ResourceLoader.loadGameObject(prefabPath);
    }

    public void setPrefab()
    {
         visualSprites = new List<Sprite>();
        for (int i = 1; i <= 16; i++)
        {
            visualSprites.Add(Resources.Load<Sprite>("FieldTiles/Field" + i.ToString()));
        }

        visualPrefab = Resources.Load<GameObject>("FieldVisual");
    }

    public virtual void spawnVisuals()
    {
        visible = true;
        foreach(ExWhyCell cell in cells)
        {
            visualObjects.Add(GameObject.Instantiate(visualPrefab, cell.cellGO.transform));
        }
    }

    public virtual void spawnVisuals(Color colour)
    {
        visible = true;
        foreach (ExWhyCell cell in cells)
        {
            GameObject visualObject = GameObject.Instantiate(visualPrefab, cell.cellGO.transform);
            visualObject.GetComponent<SpriteRenderer>().color = colour;
            visualObject.GetComponent<SpriteRenderer>().sprite = visualSprites[consolidationInt(cell)];
            visualObjects.Add(visualObject);
        }
    }

    public int consolidationInt(ExWhyCell cell)
    {
        int output = 0;


        if (cells.Find(x => x.xPosition == cell.xPosition && x.yPosition == (cell.yPosition + 1)) == null) //&& !(cell.xPosition  == map.xMax))
        {
            output += 1;
        }

        if (cells.Find(x => x.xPosition == cell.xPosition && x.yPosition == (cell.yPosition - 1)) == null) //&& !(cell.yPosition == map.yMax))
        {
            output += 2;
        }

        if (cells.Find(x => x.xPosition == (cell.xPosition - 1) && x.yPosition == (cell.yPosition)) == null) // && !(cell.xPosition == 0))
        {
            output += 4;
        }

        if (cells.Find(x => x.xPosition == (cell.xPosition + 1) && x.yPosition == (cell.yPosition)) == null) //&& !(cell.yPosition == 0))
        {
            output += 8;
        }

       
        return output;
    }

    public void despawnVisuals()
    {
        foreach(GameObject visual in visualObjects)
        {
            GameObject.Destroy(visual);
        }
        visualObjects.Clear();
    }

    public void getInverseField()
    {

    }


    public ExWhyCellField(List<ExWhyCellField> fields, ExWhy nMap)
    {
        map = nMap;
        cells = new List<ExWhyCell>();
        visualObjects = new List<GameObject>();
    
        foreach(ExWhyCellField field in fields)
        {
            cells.AddRange(field.getCells()); 
        }
    }

    public ExWhyCellField(ExWhyCellField field, ExWhy nMap)
    {
        map = nMap;
        cells = new List<ExWhyCell>();
        visualObjects = new List<GameObject>();
        cells.AddRange(field.getCells());
    }

    public ExWhyCellField(ExWhy nMap)
    {
        map = nMap;
        cells = new List<ExWhyCell>();
        visualObjects = new List<GameObject>();
    }

     public void Add(ExWhyCell nCell)
    {
        cells.Add(nCell);
    }

    public bool contains(ExWhyCell cell)
    {
        if (cells.Contains(cell)) { return true; }
        return false;
    }

    public void Add(ExWhyCellField nCells)
    {
        foreach(ExWhyCell cell in nCells.getCells())
        {
            if (!cells.Contains(cell))
            {
                cells.Add(cell);
            }
        }
    }


    public void Remove(ExWhyCellField nCells)
    {
        foreach (ExWhyCell cell in nCells.getCells())
        {
            cells.Remove(cell);
        }
    }

    public static ExWhyCellField simpleRange(ExWhy nMap, ExWhyCell epicentre, int range)
    {
        ExWhyCellField output = new ExWhyCellField(nMap);

        int epiX = epicentre.xPosition;
        int epiY = epicentre.yPosition;

        for (int x = 0; x <= range; ++x)
        {
            for (int y = 0; y <= range; ++y)
            {
                ExWhyCell possibleCell;
                if (x + y <= range)
                { 
                    if (nMap.checkIfCordsAreValid(epiX + x, epiY + y))   // !(epiX + x >= map.xMax || (epiY + y >= map.yMax)))
                    {
                        possibleCell = nMap.gridCells[epiX + x, epiY + y];
                        if (!output.getCells().Contains(possibleCell))
                        {
                            output.Add(possibleCell);
                        }
                    }

                    if (nMap.checkIfCordsAreValid(epiX - x, epiY - y))
                    {
                        possibleCell = nMap.gridCells[epiX - x, epiY - y];
                        if (!output.getCells().Contains(possibleCell))
                        {
                            output.Add(possibleCell);
                        }
                    }

                    if (nMap.checkIfCordsAreValid(epiX - x, epiY + y))
                    {
                        possibleCell = nMap.gridCells[epiX - x, epiY + y];
                        if (!output.getCells().Contains(possibleCell))
                        {
                            output.Add(possibleCell);
                        }
                    }

                    if (nMap.checkIfCordsAreValid(epiX + x, epiY - y))
                    {
                        possibleCell = nMap.gridCells[epiX + x, epiY - y];
                        if (!output.getCells().Contains(possibleCell))
                        {
                            output.Add(possibleCell);
                        }
                    }
                }
            }
        }
        return output;
    }

}
