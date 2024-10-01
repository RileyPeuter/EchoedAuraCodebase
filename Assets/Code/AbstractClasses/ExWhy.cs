using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public abstract class ExWhy
{
    //Now, fam, this isn't necessarily the best way. We could use a hypotenuse, but for now, we doing this. 
    public static int getDistanceBetweenCells(ExWhyCell cell1, ExWhyCell cell2)
    {
        return (Math.Abs(cell1.xPosition - cell2.xPosition) + Math.Abs(cell1.yPosition - cell2.yPosition));
    }

    //This is used as a shortcut. Would be better to just pass it the dependants
    public static ExWhy activeExWhy;

    //###MemberVariables###
    //The dimensions of the grid
    public int xMax;
    public int yMax;

    //These hold the gameObject PRefabs
    GameObject[] tilePrefabs;
    public GameObject[] iconPrefabs;


    //This variable holds the basic cell or "tile" data for a world. It's stored it chars, so to make it easy to create and edit maps with little to know coding knowledge
    protected char[,] worldData;
    //This variable holds events. Each event, such as non random battle, shops or story events have a number. 0 is used to denote no event. 
    //This should be the same dimensions as world data. 
    protected int[,] eventMask;

    //The this holds references to the grid cells one they are created.
    public ExWhyCell[,] gridCells;

    //These variables are used to find and load sprites.
    public Sprite[,] spriteSheet;
    public string resourceName;

    public Sprite[,] altSpriteSheet;
    public GameObject animatedTiles;


    //###Utilities###

    //This bit of code may look complicated, but it's simple. It gets a cell in a grid, then checks the surrounding cells to see whether they're the same type. 
    // If you want some grass next to some water, then it'll show a shoreline, instead of the two sprites next to each other
    public static int getConsolidationID(ExWhy gs, int x, int y)
    {
        char tileChar = gs.worldData[x, y];
        //Likes
        bool up;
        bool right;
        bool down;
        bool left;

        int output = 0;

        if (x == 0){left = true;}
        else{left = (gs.worldData[x - 1, y] == tileChar);}

        if (y == 0){down = true;}
        else{down = (gs.worldData[x, y - 1] == tileChar);}

        if (x + 1 == gs.xMax){right = true;}
        else{right = (gs.worldData[x + 1, y] == tileChar);}

        if (y + 1 == gs.yMax){up = true;}
        else
        {up = (gs.worldData[x, y + 1] == tileChar);}

        if (!up){output += 1;}
        if (!down){output += 2;}
        if (!left){output += 4;}
        if (!right){output += 8;}
        
        return output;
    }

    //Check's to see if the coordinates are valid
    public bool checkIfCordsAreValid(int x, int y)
    {
        if (x < 0 || y < 0)
        {
            return false;
        }

        if (x >= xMax || y >= yMax)
        {
            return false;
        }

        return true;
    }

    //Simple Utility for checking if coordinates are walkable
    public bool checkIfCordsAreWalkable(int x, int y)
    {
        if(!checkIfCordsAreValid (x, y))
        {
            return false;
        }
        ExWhyCell targetMoveCell = ExWhy.activeExWhy.gridCells[x, y];

        return (targetMoveCell.occupier == null && targetMoveCell.walkable);
    }

    public ExWhyCell getClosestAvailableCell(ExWhyCell epicentre)
    {
        bool flag = true;
        int x = 0;
        int y = 0;
        ExWhyCell output = null;
        while (flag)
        {
            for (int a = -x; a < x; a++)
            {
                for (int b = -y; b < y; b++)
                {
                    if (checkIfCordsAreValid(epicentre.xPosition + a, epicentre.yPosition + b))
                    {
                        ExWhyCell potential = gridCells[epicentre.xPosition + a, epicentre.yPosition + b];
                        if (potential.walkable && potential.occupier is null)
                        {
                            return potential;
                        }
                    }
                }
            }
            x++;
            y++;

            if (y == 10) { return null; }
        }
        return output;
    }

    //Function to load the sprites of the grid from Unity's "resource" folder into memory
    //Each tile type should have 16 sprites
    void loadSpriteMap()
    {
        int y = 0;
        foreach (GameObject tp in tilePrefabs)
        {
            for (int x = 0; x < 16; x++)
            {
                //GameObject.Find("globalGameController").GetComponent<globalGameController>().notPrint("OverworldTiles/sprites/" + resourceName + "/" + tp.name + (x + 1));
                //OverworldTiles / sprites / arena / floorSprites1.png
                spriteSheet[y, x] = Resources.Load<Sprite>("TileSprites/" + tp.name + (x + 1));//+ (x + 1));        
                //OverworldTiles/sprites/arena/floorSpritesfloorSprites.png
            }

            for (int x = 16; x < 32; x++)
            {
                //GameObject.Find("globalGameController").GetComponent<globalGameController>().notPrint("OverworldTiles/sprites/" + resourceName + "/" + tp.name + (x + 1));
                //OverworldTiles / sprites / arena / floorSprites1.png
                altSpriteSheet[y, x - 16] = Resources.Load<Sprite>("TileSprites/" + tp.name + (x + 1));//+ (x + 1));                                                                                                             //OverworldTiles/sprites/arena/floorSpritesfloorSprites.png
            }
            y = y + 1;
        }
    }

    void loadPrefabs()
    {
        tilePrefabs = Resources.LoadAll<GameObject>("MapTiles/Prefabs/" + resourceName);
    }
    //Place Holder. This should be overriden in the implementation
    protected virtual void initiateCells()
    {
        for (int x = 0; x < xMax; x = x + 1)
        {
            for (int y = 0; y < yMax; y = y + 1)
            {
                gridCells[x, y] = new ExWhyCell(x, y, worldData[x, y], 0);//eventMask[y, x]);
            }
        }
    }

    public ExWhyCellField getMapAsField()
    {
        ExWhyCellField output = new ExWhyCellField();
        foreach(ExWhyCell cell in gridCells)
        {
            output.Add(cell);
        }
        return output;
    }

    protected void instantiateCell(ExWhyCell cell, bool walkable, int prefabIndex, int spriteID)
    {

        cell.cellGO = GameObject.Instantiate(tilePrefabs[prefabIndex], new Vector3(cell.xPosition, cell.yPosition, 1) * 4, new Quaternion());
        cell.walkable = walkable;
        cell.spriteID = spriteID;

    }

    //This code looks a little messy
    //This creates a mirrored image and a black fadeout around the map. 
    void createBoarderCells()
    {
        GameObject fade = Resources.Load<GameObject>("OutterTileFade");

        for (int x = 0; x < xMax; x = x + 1)
        {
            GameObject target = gridCells[x, 0].cellGO;
            Vector3 position = target.transform.position + new Vector3(0, -4);
            target = GameObject.Instantiate(target);
            GameObject.Instantiate(fade, position, Quaternion.Euler(0, 0, 270));
            target.transform.position = position;
            target.transform.localScale = new Vector2(1, -1);

            target = gridCells[x, yMax - 1].cellGO;
            position = target.transform.position + new Vector3(0, 4);
            target = GameObject.Instantiate(target);
            GameObject.Instantiate(fade, position, Quaternion.Euler(0, 0, 90));
            target.transform.position = position;
            target.transform.localScale = new Vector2(1, -1);
        }


        for (int y = 0; y < yMax; y = y + 1)
        {
            GameObject target = gridCells[0, y].cellGO;
            Vector3 position = target.transform.position + new Vector3(-4, 0);
            target = GameObject.Instantiate(target);
            GameObject.Instantiate(fade, position, Quaternion.Euler(0, 0, 180));
            target.transform.position = position;
            target.transform.localScale = new Vector2(-1, 1);

            target = gridCells[xMax - 1, y].cellGO;
            position = target.transform.position + new Vector3(4, 0);
            target = GameObject.Instantiate(target);
            GameObject.Instantiate(fade, position, Quaternion.Euler(0, 0, 0));
            target.transform.position = position;

            target.transform.localScale = new Vector2(-1, 1);
        }
    }


    //This is the script to instantiate the grid in the game world
    //It reads each cell in a grid, then "draws" it in the game world
    public void drawGrid()
    {
        instantiateCells();
        //        instantiateEvents();
        //This section gets the consolidation ID. In other words, checks the cell type around each cell, and gives it a different sprite depending. 
        //For instance, if you have a water cell next to a grass cell, it shows a shore line on the water sprite. 
        foreach (ExWhyCell cell in gridCells)
        {   
            cell.colID = getConsolidationID(this, cell.xPosition, cell.yPosition);
            consolidate(cell);
        }
        createBoarderCells();
    }

    void consolidate(ExWhyCell cell)
    {
        if((cell.colID == 0 &&  cell.animatable) && UnityEngine.Random.Range(0, 100) < cell.animatableRate)
        {
            GameObject.Instantiate(Resources.Load<GameObject>("AnimationTiles/" + cell.cellType), cell.cellGO.transform);
            Debug.Log("fambulence");
            return;
        }

        if (cell.altable && (UnityEngine.Random.Range(0, 100) < cell.altRate))
        {
            cell.cellGO.GetComponent<SpriteRenderer>().sprite = altSpriteSheet[cell.spriteID, cell.colID];
            return;
        }  
        Debug.Log(cell.xPosition.ToString() + "  " +  cell.yPosition.ToString()); 
        cell.cellGO.GetComponent<SpriteRenderer>().sprite = spriteSheet[cell.spriteID, cell.colID]; 
    }

    //This function is used to trigger an event. 
    protected abstract void eventTest(int eventIndex);
    protected abstract void instantiateCells();
    protected abstract void instantiateEvents();
    
    //###Constructor###
    public ExWhy(int nXMax, int nYMax, string nResourceName)
    {
        xMax = nXMax;
        yMax = nYMax;
        resourceName = nResourceName;

        gridCells = new ExWhyCell[xMax, yMax];
        spriteSheet = new Sprite[16, 16];
        altSpriteSheet = new Sprite[16, 16];
    }

    public void initialize()
    {


        loadPrefabs();
        loadSpriteMap();
        activeExWhy = this;
    }
}