using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agency
{
    List<Facility> facilities = new List<Facility>();

    List<StoredCharacterObject> characters = new List<StoredCharacterObject>();

    int gold = 1000;

    public List<Facility> getFacilities()
    {
        return facilities;
    }

    public int getGold() { return gold; }

    public void addGold(int cost)
    {
        gold = gold + cost;
    }

    public Agency()
    {
        characters.Add(new StoredCharacterObject(new Iraden()));
        characters.Add(new StoredCharacterObject(new Fray()));
        characters.Add(new StoredCharacterObject(new Morthred()));

        facilities.Add(new Facility("Cobblers Workstation", "Work station for custom made footware [+1 Movement]", 350, derivedStat.movement, this));
        facilities.Add(new Facility("Loom", "Machine for producing lightweight padding[+1 Health]", 80, derivedStat.maxHealthPoints, this));
        facilities.Add(new Facility("Kitchen", "Equiptment for bringing as much nutrition as possible from food[+1 Max Mana]", 80, derivedStat.maxManaPoints, this));

    }

    public void addCharacter(Character nCharacter)
    {
        characters.Add(new StoredCharacterObject(nCharacter));
    }

    public List<StoredCharacterObject> getCharacters()
    {
        return characters;
    }
}
