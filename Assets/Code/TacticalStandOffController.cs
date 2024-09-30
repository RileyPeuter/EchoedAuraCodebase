using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using UnityEngine;

public class TacticalStandOffController : StandOffController {


    StandOffSide side;
    BattleCharacterObject caster;

    ExWhyCell target = null;

    TacticalAbility tAbility;
    bool finished = false;

    public override void standOffUpdate()
    {
        if (timer > 1.5f && !finished)
        {
            cast();
            end();
            finished = true;
        }
    }

    protected override void cast()
    {
        tAbility.cast(target, caster);
    }

    public void initialize(TacticalAbility nAbility, BattleCharacterObject nCaster, string recName, UIController nBUIC, ExWhyCell nTarget = null)
    {
        tAbility = nAbility;

        BUIC = nBUIC;

        target = nTarget;

        SOS = StandOffStage.SoloCast;

        side = new StandOffSide(tAbility, GetComponentsInChildren<MsSuperSecretScriptThatImUsingAsAFlag>().ToList().Find(x => x.gameObject.name == "uI_StandOffAttackerSide_Object").gameObject);
        caster = nCaster;
    }

    private void Start()
    {
        initialize();
        GameObject.Destroy(GetComponentsInChildren<MsSuperSecretScriptThatImUsingAsAFlag>().ToList().Find(x => x.gameObject.name == "uI_StandOffAttackeeSide_Object"));
        this.transform.position = new Vector2(GameObject.Find("Main Camera").transform.position.x + 20, GameObject.Find("Main Camera").transform.position.y + 5);

    }

    // Update is called once per frame
    void Update()
    {

        standOffUpdate();
        timer = timer + Time.deltaTime;

    }
}