using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
public class SoloStandOffController : StandOffController
{
    StandOffSide side;
    BattleCharacterObject caster;
    public override void standOffUpdate()
    {
        switch (SOS)
        {
            case StandOffStage.SoloCast:
                soloCast();
            break;

            case StandOffStage.Movement:
                movement();
            break;

            case StandOffStage.EmptyState:

            break;
        }
    }

    void movement()
    {
        if (timer > 1)
        {   
            end();
        }
    }

    void soloCast()
    {
        if (left && timer > 1)
        {
            cast();
            end();
        }
    }

    public void initialize(Ability ab, BattleCharacterObject BCO, string recName, BattleUIController nBUIC)
    {
        ability = ab;

        BUIC = nBUIC;
        spawnAbilitySnippet("Canvas");

        SOS = StandOffStage.SoloCast;


        resourceName = recName;

        side = new StandOffSide(BCO, resourceName, GetComponentsInChildren<MsSuperSecretScriptThatImUsingAsAFlag>().ToList().Find(x => x.gameObject.name == "uI_StandOffAttackerSide_Object").gameObject);
        caster = BCO;

        if (ability.name == "Move")
        {
            side.getAnimator().SetTrigger("movement");
            SOS = StandOffStage.Movement;
        }
        else
        {
            side.getAnimator().SetTrigger("attack");
            side.getAnimator().SetInteger("ability", ab.getAniID());
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        initialize();
        if (SOS == StandOffStage.Movement)
        {
            AS.volume = 0.5f;
            playSound(Resources.Load<AudioClip>("Audio/SoundEffects/run"));
        }

        GameObject.Destroy(GameObject.Find("uI_StandOffAttackeeSide_Object"));
        this.transform.position = new Vector2(GameObject.Find("Main Camera").transform.position.x + 20, GameObject.Find("Main Camera").transform.position.y + 5);

    }

    // Update is called once per frame
    void Update()
    {
        standOffUpdate();
        print(ability.name);
        timer = timer + Time.deltaTime;
        print(caster.getName());
        print(caster.getOccupying());
    }

    protected override void cast()
    {
        ability.cast(caster.getOccupying(), caster, side);
    }
}
