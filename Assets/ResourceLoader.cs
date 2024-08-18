using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Why I wrote this?
//Unity has various ways to load resources that are not in the scene. 
//One of these is "Resources.Load" and methods.
//This is bad, however and slow, and i happen to use it a lot. 
//To make this a little less bad, I use this, which caches all loaded assets. 

//This has 2 trade offs. 
//1, it will keep things in memory that may be not be used often. 
//2 It may, in some cases, be slower to load objects that aren't in the cache. 

//This works for my game, because my assets are pixel art and often little more than a kbyte. 
//And my game often loads the same assets over and over. 

//If you use this, keep these things in mind, and check performance with profiller. 


public static class ResourceLoader
{
    static Dictionary<string, GameObject> gameObjectCache = new Dictionary<string, GameObject>();

    static Dictionary<string, Sprite> spriteCache = new Dictionary<string, Sprite>();

    public static GameObject loadGameObject(string path)
    {
        if(gameObjectCache.TryGetValue(path, out GameObject gameObject))
        {
            return gameObject;
        }
        
        GameObject output =  Resources.Load<GameObject>(path);
        gameObjectCache.Add(path, output);
        if(output is null) { Debug.Log("Hey, fam. The load you just tried to load was null");}
        return output;
    }

    public static Sprite loadSprite(string path)
    {
        if(spriteCache.TryGetValue(path,out Sprite sprite))
        {
            return sprite;
        }

        Sprite output = Resources.Load<Sprite>(path);
        if (output is null) { Debug.Log("Hey, fam. The load you just tried to load was null"); }
        spriteCache.Add(path, sprite);
        return output;
    }

}
