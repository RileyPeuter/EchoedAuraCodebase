using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agency
{

    List<StoredCharacterObject> characters = new List<StoredCharacterObject>();


    public Agency()
    {
        characters.Add(new StoredCharacterObject(new Iraden()));
        characters.Add(new StoredCharacterObject(new Fray()));
        characters.Add(new StoredCharacterObject(new Morthred()));
    }

    public List<StoredCharacterObject> getCharacters()
    {
        return characters;
    }
}
