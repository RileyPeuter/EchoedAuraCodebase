using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class ExWhyCellField 
{
    List<ExWhyCell>cells;
    List<GameObject> visualObjects;
    GameObject visualPrefab;
    public bool visible;

    public List<ExWhyCell> getCells()
    {
        return cells;
    }

    public void setPrefab(string prefabPath)
    {
        visualPrefab = ResourceLoader.loadGameObject(prefabPath);
    }

    public void spawnVisuals()
    {
        visible = true;
        foreach(ExWhyCell cell in cells)
        {
            visualObjects.Add(GameObject.Instantiate(visualPrefab, cell.cellGO.transform));
        }
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


    public ExWhyCellField(List<ExWhyCellField> fields)
    {
        cells = new List<ExWhyCell>();
        visualObjects = new List<GameObject>();
    
        foreach(ExWhyCellField field in fields)
        {
            cells.AddRange(field.getCells()); 
        }
    }


    public ExWhyCellField(ExWhyCellField field)
    {
        cells = new List<ExWhyCell>();
        visualObjects = new List<GameObject>();
        cells.AddRange(field.getCells());
    }

    public ExWhyCellField()
    {
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
            cells.Add(cell);
        }

 //       cells.AddRange(getCells());
    }


    public void Remove(ExWhyCellField nCells)
    {
        foreach (ExWhyCell cell in nCells.getCells())
        {
            cells.Remove(cell);
        }
    }

    public static ExWhyCellField simpleRange(ExWhy map, ExWhyCell epicentre, int range)
    {
        ExWhyCellField output = new ExWhyCellField();

        int epiX = epicentre.xPosition;
        int epiY = epicentre.yPosition;

        for (int x = 0; x <= range; ++x)
        {
            for (int y = 0; y <= range; ++y)
            {
                ExWhyCell possibleCell;
                if (x + y <= range)
                { 
                    if (map.checkIfCordsAreValid(epiX + x, epiY + y))   // !(epiX + x >= map.xMax || (epiY + y >= map.yMax)))
                    {
                        possibleCell = map.gridCells[epiX + x, epiY + y];
                        if (!output.getCells().Contains(possibleCell))
                        {
                            output.Add(possibleCell);
                        }
                    }

                    if (map.checkIfCordsAreValid(epiX - x, epiY - y))
                    {
                        possibleCell = map.gridCells[epiX - x, epiY - y];
                        if (!output.getCells().Contains(possibleCell))
                        {
                            output.Add(possibleCell);
                        }
                    }

                    if (map.checkIfCordsAreValid(epiX - x, epiY + y))
                    {
                        possibleCell = map.gridCells[epiX - x, epiY + y];
                        if (!output.getCells().Contains(possibleCell))
                        {
                            output.Add(possibleCell);
                        }
                    }

                    if (map.checkIfCordsAreValid(epiX + x, epiY - y))
                    {
                        possibleCell = map.gridCells[epiX + x, epiY - y];
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
