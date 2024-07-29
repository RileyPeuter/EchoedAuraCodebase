using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StoredCharacterState { 
    Idle, 
    Resting,
    Infiltrating,
    Training,
    Incapacitated,
    OnMission
}
public class StoredCharacterObject
{

    StoredCharacterState storedState;
    Sprite characterSigil;
    Character character;
    int mana;

    public StoredCharacterObject(Character n_Character)
    {
        character = n_Character;
        storedState = StoredCharacterState.Idle;
        mana = 0;
    }

    public int getMana()
    {
        return mana;
    }
    
    public int getMaxMana()
    {
        return character.getDerivedStat(derivedStat.maxManaPoints); 
    }

    public Character GetCharacter()
    {
        return character; 
    }

    public StoredCharacterState getStoredState()
    {
        return storedState;
    }


    public string getCharacterName()
    {
        return character.CharacterName;
    }

    
    public string getStateString()
    {
        string output = "";

        switch (storedState)
        {

            case StoredCharacterState.Idle:
                output = "Idle";
                break;

            case StoredCharacterState.Training:
                output = "Training";
                break;

            case StoredCharacterState.Resting:
                output = "Resting";
                break;

            case StoredCharacterState.Infiltrating:
                output = "Infiltrating";
                break;

            case StoredCharacterState.Incapacitated:
                output = "Incapacitated";
                break;

            case StoredCharacterState.OnMission:
                output = "On Mission";
                break;

        }
        return output;
    }
}
