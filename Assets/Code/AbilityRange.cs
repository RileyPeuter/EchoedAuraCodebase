using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum RangeMode
{
    Simple,
    Move, 
    Global
}

public class AbilityRange : MonoBehaviour
{
    //Riley, I know at some point you're gonna have to calculate walking range around objects, and all I can say is good luck. Let her help you.

    //###MemberVariables
    List<ExWhyCell> cellsInRange;

    List<GameObject> rangeVisuals;
    List<BattleCharacterObject> charactersInRange;

    int range;
    ExWhyCell epicentre;
    ExWhy grid;

    //The prefab used to show range
    GameObject visual;
    
    List<ExWhyCell> visited;

    //Finds all cells in  range. 
    //It just uses a two dimensional for loop adding a cell to each quandrant. 

    //###Utilities
    public List<ExWhyCell> findCellsInRange(RangeMode mode)
    {
        switch (mode)
        {
            case RangeMode.Move:
                return jumperRange(epicentre, range);
                break;
        }

        return simpleRange();
    }

    public List<ExWhyCell> simpleRange() { 
        int epiX = epicentre.xPosition;
        int epiY = epicentre.yPosition;

        for (int x = 0; x <= range; ++x)
        {
            for (int y = 0; y <= range; ++y)
            {
                ExWhyCell possibleCell;
                if (x + y <= range) {

                    if (!(epiX + x >= grid.xMax || (epiY + y >= grid.yMax))) {
                        possibleCell = grid.gridCells[epiX + x, epiY + y];
                        if (!cellsInRange.Contains(possibleCell))
                        {
                            cellsInRange.Add(grid.gridCells[epiX + x, epiY + y]);
                        }
                    }

                    if (!(epiX - x < 0 || (epiY - y < 0)))
                    {
                        possibleCell = grid.gridCells[epiX - x, epiY - y];
                        if (!cellsInRange.Contains(possibleCell))
                        {
                            cellsInRange.Add(grid.gridCells[epiX - x, epiY - y]);
                        }
                     }

                    if (!((epiX - x < 0)|| (epiY + y >= grid.yMax)))
                    {
                        possibleCell = grid.gridCells[epiX - x, epiY + y];
                        if (!cellsInRange.Contains(possibleCell))
                        {
                            cellsInRange.Add(grid.gridCells[epiX - x, epiY + y]);
                        }
                    }


                    if (!((epiX + x  >= grid.xMax) || (epiY - y < 0)))
                    {
                        possibleCell = grid.gridCells[epiX + x, epiY - y];
                        if (!cellsInRange.Contains(possibleCell))
                        {
                            cellsInRange.Add(grid.gridCells[epiX + x, epiY - y]);
                        }
                    }
                }
            }
        }
        return cellsInRange;
    }

    //This "Jumper Range" is a bredth-First search for finding the walkable range of an ability. 
    //Basically, a path finding algorithm
    //Todo : Add "visted" code to make more efficient
    public List<ExWhyCell> jumperRange(ExWhyCell currentCell, int jumpsLeft)
    {
        if (!cellsInRange.Contains(currentCell))
        {
            cellsInRange.Add(currentCell);

        }
        visited.Add(currentCell);

        if (jumpsLeft == 0 || !currentCell.walkable)
        {
            return cellsInRange;
        }

        //Up    
        if (currentCell.yPosition < (grid.yMax - 1)){
            if (grid.gridCells[currentCell.xPosition, currentCell.yPosition + 1].walkable) {
                jumperRange(grid.gridCells[currentCell.xPosition, currentCell.yPosition + 1], jumpsLeft - 1);
            }
        }

        //Down
        if (currentCell.yPosition != 0)
        {
            if (grid.gridCells[currentCell.xPosition, currentCell.yPosition - 1].walkable)
            {
                jumperRange(grid.gridCells[currentCell.xPosition, currentCell.yPosition - 1], jumpsLeft - 1);
            }
        }

        //Left
        if (currentCell.xPosition != 0)
        {
            if (grid.gridCells[currentCell.xPosition - 1, currentCell.yPosition].walkable)
            {
                jumperRange(grid.gridCells[currentCell.xPosition - 1 , currentCell.yPosition], jumpsLeft - 1);
            }
        }

        //Right
        if (currentCell.xPosition < (grid.xMax - 1))
        {
            if (grid.gridCells[currentCell.xPosition + 1, currentCell.yPosition].walkable)
            {
                jumperRange(grid.gridCells[currentCell.xPosition + 1, currentCell.yPosition], jumpsLeft - 1);
            }
        }
        return cellsInRange;
    }

    public void destroyVisual()
    {
        foreach (GameObject go in rangeVisuals)
        {
            Destroy(go);
        }
    }

    //Spawns a blue visial effect for the range of abilities/ 
    public void spawnVisuals()
    {
        GameObject visualGameObject;
        foreach(ExWhyCell cell in cellsInRange)
        {
            visualGameObject = Instantiate(visual, cell.cellGO.transform);
            rangeVisuals.Add(visualGameObject);

            if (cell.occupier != null){
                if(cell.occupier.GetAllegiance() != CharacterAllegiance.Controlled)
                {
                    visualGameObject.GetComponent<RangeVisualController>().setOffset((cell.xPosition + cell.yPosition) * 0.1f, Color.red);
                }
                else
                {
                    visualGameObject.GetComponent<RangeVisualController>().setOffset((cell.xPosition + cell.yPosition) * 0.1f, Color.green);
                }
            }
            visualGameObject.GetComponent<RangeVisualController>().setOffset((cell.xPosition + cell.yPosition) * 0.1f);
        }
    }

    //Finds the characters in already foudn cells. 
    public List<BattleCharacterObject> findCharactersInRange()
    {
        List<BattleCharacterObject> output = new List<BattleCharacterObject>();
        foreach (ExWhyCell cell in cellsInRange)
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

    //###initialize###
    public void initalize(int nRange, ExWhyCell nEpicentre, ExWhy nGrid)
    {
        range = nRange;
        epicentre = nEpicentre;
        visual = Resources.Load<GameObject>("RangeVisual");
        grid = nGrid;
        cellsInRange = new List<ExWhyCell>();
        rangeVisuals = new List<GameObject>();
        visited = new List<ExWhyCell>();
    }
}
