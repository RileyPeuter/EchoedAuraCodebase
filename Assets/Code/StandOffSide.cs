using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
public class StandOffSide
{

    public BattleCharacterObject characterSide;
    Sprite frontTileSprite;
    Sprite backTileSprite;
    Sprite backgroundSprite;
    Animator characterAnimator;
    Animation abilityAnimation;
    public GameObject sideGO;
    Dictionary<string, Sprite> messageQueue;
    StandOffAnimationListener SOAL;
    float messageTimer = 0;

    public void tickMessage()
    {
        if (messageQueue.Count != 0)
        {

            if (messageTimer <= 0)
            {
                sendMessage();
            }
            messageTimer = 0.5f;
        }
    }

    public bool checkIfMessagesInQueue()
    {
        if(messageQueue.Count == 0)
        {
            return false;
        }
        return true;
    }

    public void setReaction(int reaction)
    {
        characterAnimator.SetInteger("reactionType", reaction);
    }
    public void sendMessage()
    {

        GameObject go = GameObject.Instantiate(Resources.Load<GameObject>("floatingText"));
        if (sideGO.name == "uI_StandOffAttackeeSide_Object")
        {
            go.transform.position = new Vector3(sideGO.transform.position.x + 10, sideGO.transform.position.y, sideGO.transform.position.z);
        }
        else
        {
            go.transform.position = new Vector3(sideGO.transform.position.x - 10, sideGO.transform.position.y, sideGO.transform.position.z);

        }
        go.GetComponent<DamageTextController>().initialize(messageQueue.Keys.First(), messageQueue.Values.First());
        messageQueue.Remove(messageQueue.First().Key);

    }

    public Animator getAnimator()
    {
        return characterAnimator;
    }

    public void effectMessage(string text, Sprite image = null)
    {
        int output;
        if (int.TryParse(text, out output))
        {
            if (output == 0) { return; }
        }

        messageQueue.Add(text, image);
    }

    public StandOffSide(BattleCharacterObject chara, string resString, GameObject go, Ability ab = null)
    {
        messageQueue = new Dictionary<string, Sprite>();
        sideGO = go;

        frontTileSprite = Resources.Load<Sprite>("MapTiles/Sprites/" + resString + "/" + chara.getOccupying().resourceName + "Front");
        backTileSprite = Resources.Load<Sprite>("MapTiles/Sprites/" + resString + "/" + chara.getOccupying().resourceName + "Back");

        backgroundSprite = Resources.Load<Sprite>("MapTiles/Sprites/" + resString + "/Background");

        characterSide = chara;

        setGraphics();
        SOAL = sideGO.GetComponentInChildren<StandOffAnimationListener>();
        SOAL.initialize();
    }

    public StandOffSide(TacticalAbility nAbility, GameObject nGameObject)
    {
        messageQueue = new Dictionary<string, Sprite>();
        sideGO = nGameObject;

        foreach (SpriteRenderer image in sideGO.GetComponentsInChildren<SpriteRenderer>())
        {
            switch (image.gameObject.name)
            {
                case "uI_StandOffFrontTile_Image":
                    image.sprite = frontTileSprite;
                    break;

                case "uI_StandOffBackTile_Image":
                    image.sprite = backTileSprite;
                    break;

                case "uI_StandOffBackGround_Image":
                    image.sprite = backgroundSprite;
                    break;
                case "uI_StandOffCharacter_Image":

                    //This is horrible, but I think it should work
                    Animator ani = image.gameObject.AddComponent<Animator>();
                                        ani.runtimeAnimatorController = Resources.Load<AnimatorOverrideController>("TacticalAbilityController");
                    AnimatorOverrideController AOC = new AnimatorOverrideController(ani.runtimeAnimatorController);
                    List<KeyValuePair<AnimationClip, AnimationClip>> aniClips = new List<KeyValuePair<AnimationClip, AnimationClip>>();
                    
                    aniClips.Add(new KeyValuePair<AnimationClip, AnimationClip>(AOC.animationClips[0], Resources.Load<AnimationClip>("TacticalAnimations/" + nAbility.getAniID().ToString())));
                    AnimatorOverrideController aoc = new AnimatorOverrideController(ani.runtimeAnimatorController);
                    AOC.ApplyOverrides(aniClips);

                    ani.runtimeAnimatorController = AOC;

                    break;
            }
        }
    }

    public void spawnEffectOnCharacter(string effectName)
    {
        GameObject.Instantiate(Resources.Load<GameObject>("StandAloneEffects/" + effectName), characterAnimator.transform);
    }
    void setGraphics(Ability ab = null)
    {
        foreach (SpriteRenderer image in sideGO.GetComponentsInChildren<SpriteRenderer>())
        {
            switch (image.gameObject.name)
            {
                case "uI_StandOffFrontTile_Image":
                    image.sprite = frontTileSprite;
                    break;

                case "uI_StandOffBackTile_Image":
                    image.sprite = backTileSprite;
                    break;

                case "uI_StandOffBackGround_Image":
                    image.sprite = backgroundSprite;
                    break;

                case "uI_StandOffCharacter_Image":
                    
                    characterAnimator = image.gameObject.AddComponent<Animator>();
                    StandOffController.print(characterAnimator.name);
                    AnimatorOverrideController AOC = Resources.Load<AnimatorOverrideController>("GeneralCharacterAssets/AOC" + characterSide.getCharacter().getResourceString());
                    characterAnimator.runtimeAnimatorController = AOC;
                    break;
            }
        }
    }
}