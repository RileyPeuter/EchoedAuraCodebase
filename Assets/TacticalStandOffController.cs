using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using UnityEngine;

public class TacticalStandOffController : StandOffController {


    StandOffSide side;
    BattleCharacterObject caster;
    TacticalAbility tAbility;
    public override void standOffUpdate()
    {
        throw new System.NotImplementedException();
    }

    protected override void cast()
    {
        ability.cast(null, caster);
    }

    public void initialize(TacticalAbility nAbility, BattleCharacterObject nCaster, string recName, BattleUIController nBUIC, BattleCharacterObject target = null)
    {
        tAbility = nAbility;

        BUIC = nBUIC;

        spawnAbilitySnippet("Canvas");
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

    }
}