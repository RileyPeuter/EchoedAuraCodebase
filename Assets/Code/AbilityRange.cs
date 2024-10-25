using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum RangeMode
{
    Simple,
    Move, 
    Global, 
    Custom
}

public class AbilityRange : ExWhyCellField
{
    
    //###MemberVariables
    
    List<BattleCharacterObject> charactersInRange;

    int range;
    ExWhyCell epicentre;


    //The prefab used to show range
    
    List<ExWhyCell> visited;

    public AbilityRange(List<ExWhyCellField> fields, ExWhy nMap) : base(fields, nMap)
    {
    }

    public AbilityRange(ExWhyCellField field, ExWhy nMap) : base(field, nMap)
    {
    }

    public AbilityRange(ExWhy nMap) : base(nMap)
    {
    }

    public AbilityRange(int nRange, ExWhyCell nEpicentre,  ExWhy nMap, RangeMode nRangeMode) : base(nMap)
    {
        epicentre = nEpicentre;
        range = nRange;
        visited = new List<ExWhyCell>();
    }


    //Finds all cells in  range. 
    //It just uses a two dimensional for loop adding a cell to each quandrant. 

    public void setCellsInRange(List<ExWhyCell> nCells)
    {
        cells = nCells;
    }

    //###Utilities
    public List<ExWhyCell> findCellsInRange(RangeMode mode)
    {
        if(mode == RangeMode.Custom)
        {
            return cells;
        }

        if(cells.Count > 0)
        {
            return cells;
        }

        switch (mode)
        {
            case RangeMode.Move:
                return jumperRange(epicentre, range);
            break;
        }

        return simpleRange();
    }

    public List<ExWhyCell> globalRange()
    {
        foreach (ExWhyCell cell in map.gridCells)
        {
            cells.Add(cell);
        }
        return cells;
    }

    //Todo: Use "checkValidCell" in ExWhy
    public List<ExWhyCell> simpleRange() { 
        int epiX = epicentre.xPosition;
        int epiY = epicentre.yPosition;

        for (int x = 0; x <= range; ++x)
        {
            for (int y = 0; y <= range; ++y)
            {
                ExWhyCell possibleCell;
                if (x + y <= range) {

                    if (!(epiX + x >= map.xMax || (epiY + y >= map.yMax))) {
                        possibleCell = map.gridCells[epiX + x, epiY + y];
                        if (!cells.Contains(possibleCell))
                        {
                            cells.Add(map.gridCells[epiX + x, epiY + y]);
                        }
                    }

                    if (!(epiX - x < 0 || (epiY - y < 0)))
                    {
                        possibleCell = map.gridCells[epiX - x, epiY - y];
                        if (!cells.Contains(possibleCell))
                        {
                            cells.Add(map.gridCells[epiX - x, epiY - y]);
                        }
                     }

                    if (!((epiX - x < 0)|| (epiY + y >= map.yMax)))
                    {
                        possibleCell = map.gridCells[epiX - x, epiY + y];
                        if (!cells.Contains(possibleCell))
                        {
                            cells.Add(map.gridCells[epiX - x, epiY + y]);
                        }
                    }


                    if (!((epiX + x  >= map.xMax) || (epiY - y < 0)))
                    {
                        possibleCell = map.gridCells[epiX + x, epiY - y];
                        if (!cells.Contains(possibleCell))
                        {
                            cells.Add(map.gridCells[epiX + x, epiY - y]);
                        }
                    }
                }
            }
        }
        return cells;
    }

    //This "Jumper Range" is a bredth-First search for finding the walkable range of an ability. 
    //Basically, a path finding algorithm
    //Todo : Add "visted" code to make more efficient
    public List<ExWhyCell> jumperRange(ExWhyCell currentCell, int jumpsLeft)
    {
        if (!cells.Contains(currentCell))
        {
            cells.Add(currentCell);

        }
        visited.Add(currentCell);

        if (jumpsLeft == 0 || !currentCell.walkable)
        {
            return cells;
        }

        //Up    
        if (currentCell.yPosition < (map.yMax - 1)){
            if (map.gridCells[currentCell.xPosition, currentCell.yPosition + 1].walkable && map.gridCells[currentCell.xPosition, currentCell.yPosition + 1].occupier == null) {
                jumperRange(map.gridCells[currentCell.xPosition, currentCell.yPosition + 1], jumpsLeft - 1);
            }
        }

        //Down
        if (currentCell.yPosition != 0)
        {
            if (map.gridCells[currentCell.xPosition, currentCell.yPosition - 1].walkable && map.gridCells[currentCell.xPosition, currentCell.yPosition - 1].occupier == null)
            {
                jumperRange(map.gridCells[currentCell.xPosition, currentCell.yPosition - 1], jumpsLeft - 1);
            }
        }

        //Left
        if (currentCell.xPosition != 0)
        {
            if (map.gridCells[currentCell.xPosition - 1, currentCell.yPosition].walkable && map.gridCells[currentCell.xPosition - 1, currentCell.yPosition].occupier == null)
            {
                jumperRange(map.gridCells[currentCell.xPosition - 1 , currentCell.yPosition], jumpsLeft - 1);
            }
        }

        //Right
        if (currentCell.xPosition < (map.xMax - 1))
        {
            if (map.gridCells[currentCell.xPosition + 1, currentCell.yPosition].walkable && map.gridCells[currentCell.xPosition + 1, currentCell.yPosition].occupier == null)
            {
                jumperRange(map.gridCells[currentCell.xPosition + 1, currentCell.yPosition], jumpsLeft - 1);
            }
        }
        return cells;
    }

    
    //Spawns a blue visial effect for the range of abilities/
    
    public override void spawnVisuals()
    {
        GameObject visualGameObject;
        foreach(ExWhyCell cell in cells)
        {
            visualGameObject = GameObject.Instantiate(visualPrefab, cell.cellGO.transform);
            visualObjects.Add(visualGameObject);
            visualGameObject.GetComponent<SpriteRenderer>().color = Color.blue;

            visualGameObject.GetComponent<SpriteRenderer>().sprite = visualSprites[consolidationInt(cell)];
            if (cell.occupier != null){ 
                if(cell.occupier.GetAllegiance() != CharacterAllegiance.Controlled)
                {
                    visualGameObject.GetComponent<SpriteRenderer>().color = Color.red;
                }
                else
                {
                    visualGameObject.GetComponent<SpriteRenderer>().color = Color.green;
                }
            }
        }
    }

    
    //Finds the characters in already foudn cells. 
    public List<BattleCharacterObject> findCharactersInRange()
    {
        List<BattleCharacterObject> output = new List<BattleCharacterObject>();
        foreach (ExWhyCell cell in cells)
        {
            if (cell.occupier)
            {
                output.Add(cell.occupier);
            }
            charactersInRange = output;
        }
            return output;
    }

    public static ExWhyCell getClosestCell(ExWhyCell target, List<ExWhyCell> availableCells, bool requiresAvailable = false)
    {
        ExWhyCell output = null;
        int outputDiff = 999;
        foreach (ExWhyCell cell in availableCells)
        {
            int cellDiff = Mathf.Abs(target.xPosition - cell.xPosition) + Mathf.Abs(target.yPosition - cell.yPosition);
            if ( cellDiff < outputDiff && (!requiresAvailable || (cell.occupier == null && cell.walkable)))
            {
                output = cell;
                outputDiff = cellDiff;
            }
        }
        return output;
    }
}
