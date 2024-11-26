using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.Collections;
using UnityEngine;

public enum CharacterAllegiance
{
    Controlled,
    Allied,
    Neutral,
    Enemey
}

//Unused at the moment
public enum CharacterState
{
    Normal,
    Stunned,
    Dead
}

public class BattleCharacterObject : MonoBehaviour
{
    ExWhyCellField fieldOfVision;

    static int nextID = 0;

    public static int getNextID()
    {
        int output = nextID;
        nextID++;
        return output;
    }

    //###MemberVariables###

    int id;

    CellBuff currentCellBuff;

    CharacterAllegiance allegiance;
    ExWhyCell occupying;    
    Character character;

    Dictionary<int, Ability> abilityList;

    Animator characterAnimator;

    BattleCharacterAI CharacterAI;
    float idleTrigger;
    float timer = 0;

    public GameObject characterObject;

    int manaFlow;
    int availableMovement;

    Ability abilityMove;

    int spawnX = 3;
    int spawnY = 3;

    int visionRange = 8;

    int nextTurn;
    CharacterState characterState = CharacterState.Normal;


    //###Getters###
    public ExWhyCellField getFieldOfVision()
    {
        return fieldOfVision;
    }

    public BattleCharacterAI getAI()
    {
        return CharacterAI;
    }

    public ExWhyCellField calculateFieldOfView(ExWhy map)
    {
        fieldOfVision = ExWhyCellField.simpleRange(map, occupying, visionRange);
        return fieldOfVision;
    }

    public CharacterAllegiance GetAllegiance()
    {
        return allegiance;
    }

    public int getAdjustedManaflow()
    {
        return 2 + Mathf.FloorToInt(getManaFlow() / 3);
    }

    public int getManaFlow()
    {
        return manaFlow;
    }

    public Ability getMovementAbility()
    {
        return abilityMove;
    }

    public int getMovement()
    {
        return availableMovement;
    }

    public int getCurrentHealthPoints()
    {
        return character.getCurrentHealth();
    }

    public int getMeleeBonus()
    {
        return character.getDerivedStat(derivedStat.meleeBonus);
    }

    public List<Ability> getBasicAbilities()
    {
        return character.GeneralAbilities;
    }

    public List<Ability> getSpecialAbilities()
    {
        return character.CharacterAbilities;
    }
    
    public int getID()
    {
        return id;
    }

    public List<Ability> getAllAbilities()
    {
        List<Ability> output = new List<Ability>();
        output.AddRange(getBasicAbilities());
        output.AddRange(getSpecialAbilities());
        return output;
    }

    public CharacterState getState()
    {
        return characterState;
    }
    public int getDodgeToHit()
    {
        return character.getDerivedStat(derivedStat.dodgeTH);
    }


    public int getDodge()
    {
        return character.getDerivedStat(derivedStat.dodge);
    }


    public List<reactionType> getAvailableReactions()
    {
        return character.reactionsAvailable;
    }

    public int getParryToHit()
    {
        return character.getDerivedStat(derivedStat.parryTH);
    }


    public int getParry()
    {
        return character.getDerivedStat(derivedStat.parry);
    }


    public int getBlockToHit()
    {
        return character.getDerivedStat(derivedStat.blockTH);
    }


    public int getBlock()
    {
        return character.getDerivedStat(derivedStat.block);
    }

    public void startTurn()
    {
        character.tickBuffs();
    }

    public string getName()
    {
        return character.CharacterName;
    }

    public string getNameID()
    {
        return character.CharacterName + id;
    }

    public Character getCharacter()
    {
        return character;
    }

    public reactionType selectReact()
    {
        return CharacterAI.getReaction();
    }

    public ExWhyCell getOccupying()
    {
        return occupying;
    }
    public int getCurrentMana()
    {
        return character.getCurrentMana();
    }

    //###Setters###
    public void setSpawnCords(int nSpwanX, int nSpwanY)
    {
        spawnX = nSpwanX;
        spawnY = nSpwanY;
    }

    //###Utilities###

    public void makeDormant()
    {
        nextTurn = -1;
    }

    public void alert(int  nNextTrun)
    {
        nextTurn = nNextTrun;
    }

    public void spawnCharacter(ExWhy grid)
    {
        characterObject = this.gameObject;
        occupying = grid.gridCells[spawnX, spawnY];
        grid.gridCells[spawnX, spawnY].occupier = this;
        setGraphicalPosition();
        currentCellBuff = grid.gridCells[spawnX, spawnY].getCellBuff().Clone(character);
        character.addBuff(currentCellBuff);
    }

    public void spendMana(int cost)
    {
        manaFlow = manaFlow - cost;
    }

    public void heal(int healing)
    {
        character.HealthPoints = character.HealthPoints + healing;
        if(character.HealthPoints >= character.getDerivedStat(derivedStat.maxHealthPoints)) { character.HealthPoints = character.getDerivedStat(derivedStat.maxHealthPoints); }
    }

    public void takeDamage(int damage)
    {
        character.HealthPoints = character.HealthPoints - damage;
        if (character.HealthPoints <= 0) { die(); }
    }

    public void idleAnimation()
    {

        if (characterAnimator != null)
        {
            characterAnimator.SetTrigger("pop");
            idleTrigger = Random.Range(5f, 10f);
        }
        timer = 0;
    }

    //Used to move the sprite to the current cell it occupies. 
    public void setGraphicalPosition()
    {
        characterObject.transform.position = occupying.getTransform().position;
    }


    public void spendMovement(int movementCost)
    {
        availableMovement = availableMovement - movementCost;
    }

    public void move(ExWhyCell newCell)
    {
        character.removeBuff(currentCellBuff);
        occupying.occupier = null;
        occupying = newCell;
        newCell.occupier = this;
        setGraphicalPosition();
        currentCellBuff = newCell.getCellBuff().Clone(character);
        character.addBuff(currentCellBuff);
        GetComponent<SpriteRenderer>().sortingOrder = -occupying.yPosition;
    }

    public void clearAniBools()
    {
        setAniAnticipate(false);
        setAniRun(false);
    }

    public void setAniRun(bool running)
    {
        if (characterAnimator != null)
        {
            characterAnimator.SetBool("running", running);
        }
    }

    public void setAniAnticipate(bool anticipate)
    {
        if (characterAnimator != null)
        {
            characterAnimator.SetBool("anticipating", anticipate);
        }
    }

    public void setAniDie()
    {
        if (characterAnimator != null)
        {
            clearAniBools();
            characterAnimator.SetTrigger("die");
        }
    }

    public bool isNextTurn(int turn)
    {
        if (nextTurn == turn) { return true; }
        return false;
    }

    public static int adjustManaFlow(int mf)
    {
        return 2 + Mathf.FloorToInt(mf / 3);
    }

    public void endTurn()
    {
        nextTurn += 20 - character.getDerivedStat(derivedStat.turnFrequency);
        character.cleanUpBuffs();

        int manaRegenerated = adjustManaFlow(character.getDerivedStat(derivedStat.manaFlow)) - manaFlow;

        if (character.getCurrentMana() < manaRegenerated)
        {
            manaFlow = manaFlow + getCurrentMana();
            character.ManaPoints = 0;
        }
        else
        {
            manaFlow = adjustManaFlow(character.getDerivedStat(derivedStat.manaFlow));
            character.ManaPoints -= manaRegenerated;
        }
        availableMovement = character.getDerivedStat(derivedStat.movement);
        character.tickBuffs();
    }

    public void die()
    {
        characterState = CharacterState.Dead;
        BattleController.ActiveBattleController.killCharacter(this);
    }

    //###Initializer###
    public void initialize(int nSpawnX, int nSpawnY, Character nCharacter, CharacterAllegiance nAllegiance, int nNextTurn = 0, int nID = -1)
    {
        
        if(nID == -1)
        {
            id = getNextID();
        }
        else
        {
            id = nID;
        }
       
        spawnX = nSpawnX;
        spawnY = nSpawnY;
        character = nCharacter;
        allegiance = nAllegiance;
        nextTurn = nNextTurn;
        if(character.getDefualtBCAI() is null)
        {
            CharacterAI = new DefaultAI();
        }
        else
        {   
            CharacterAI = character.getDefualtBCAI();
        }
        characterAnimator = GetComponent<Animator>();
        visionRange = character.defaultVision;
        getAI().initialize(this);
        abilityMove = new Move();

    }

    public void initialize(Character ch, CharacterAllegiance chAl, int nID = -1)
    {
        if (nID == -1)
        {
            id = getNextID();
        }
        else
        {
            id = nID;
        }

        character = ch;
        allegiance = chAl;
        if (character.getDefualtBCAI() is null)
        {
            CharacterAI = new DefaultAI();
        }
        else
        {
            CharacterAI = character.getDefualtBCAI();
        }
        characterAnimator = GetComponent<Animator>();
        abilityMove = new Move();

    }

    //###UnityMessages###
    void Start()
    {
        manaFlow = adjustManaFlow(character.getDerivedStat(derivedStat.manaFlow));
        availableMovement = character.getDerivedStat(derivedStat.movement);
        abilityMove = new Move();
        idleTrigger = Random.Range(5f, 10f);
    }

    void Update()
    {
        timer = timer + Time.deltaTime;
        if(timer > idleTrigger)
        {
            idleAnimation();
        }
    }

    //Unused
    public static GameObject getGOBCO(Character Ch, CharacterAllegiance Al)
    {
        GameObject output = Resources.Load<GameObject>("BCOAssets/GenericBCO");
        output.GetComponent<Animator>().runtimeAnimatorController = Resources.Load<AnimatorOverrideController>("BCOAssets/GenericBCO/AOCBCO" + Ch.CharacterName);
        output.GetComponent<BattleCharacterObject>().initialize(Ch, Al);
        return output;
    }  
}