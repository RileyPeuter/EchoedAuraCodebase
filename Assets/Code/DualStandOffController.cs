using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
public class DualStandOffController : StandOffController
{
    int storedDamage = -1;

    int reductionAmount = 0;
    int rollAmount;
    AttackAttempt attackAttempt;
    StandOffSide leftSide; //Attacker
    StandOffSide rightSide; //Reacter
    int toHitNumber;

    bool reactSelected = true;

    MiniStatsController targetController;

    

    public void initialize(AttackAttempt AA, BattleController BC, BattleUIController nBUIC)
    {

        BUIC = nBUIC;

        resourceName = BC.getMapResourceString();
        ability = AA.getAbility();
        BUIC = BC.GetBattleUIController();
        rollPanel = BUIC.openWindow("uI_StandOffRoll_Panel", true, "Canvas", false);
        spawnAbilitySnippet("uI_StandOffRoll_Panel(Clone)");
        
        attackAttempt = AA;
        leftSide = new StandOffSide(attackAttempt.getAttacker(), resourceName, GetComponentsInChildren<MsSuperSecretScriptThatImUsingAsAFlag>().ToList().Find(x => x.gameObject.name == "uI_StandOffAttackerSide_Object").gameObject);
        rightSide = new StandOffSide(attackAttempt.getAttackee(), resourceName, GetComponentsInChildren<MsSuperSecretScriptThatImUsingAsAFlag>().ToList().Find(x => x.gameObject.name == "uI_StandOffAttackeeSide_Object").gameObject);
        damageText = GameObject.Find("uI_Damage_Text").GetComponent<Text>();


        foreach (SpriteRenderer sr in gameObject.GetComponentsInChildren<SpriteRenderer>())
        {
            if (sr.gameObject.name == "backPanel")
            {
                 backPanel = sr.gameObject;
            }
        }

        attackAttempt = AA;

        //Chekcs to see if character is an active character, and propts player for reaction if it's controlled. 
        if (attackAttempt.getAttackee().GetAllegiance() == CharacterAllegiance.Controlled)
        {
            GameObject GO = BUIC.openWindow("uI_AttackAttampetReact_Panel", true, "Canvas", false);
            GO.GetComponent<AttackAttemptReactionScript>().initialize(BattleUIController.HighestWindow, attackAttempt);
        }
        else
        {
            attackAttempt.react(attackAttempt.askReaction());
        }

    }
    void Start()
    {
        initialize();
        targetController = GameObject.Instantiate(Resources.Load<GameObject>("UIElements/uI_StandOffTargetStat_Panel"), GameObject.Find("Canvas").transform).GetComponent<MiniStatsController>();
        targetController.initialize(BattleUIController.HighestWindow, rightSide.characterSide);
        rightSide.getAnimator().SetTrigger("anticipate");
        leftSide.getAnimator().SetInteger("ability", attackAttempt.getAbilityAniID());
        leftSide.getAnimator().SetTrigger("attack");
    }

    // Update is called once per frame
    void Update()
    {
        print(SOS);
        timer = timer + Time.deltaTime;
        messageTimer = messageTimer - Time.deltaTime;
        if (leftSide != null)
        {
            leftSide.tickMessage();
        }
        if (rightSide != null)
        {
            rightSide.tickMessage();
        }
        standOffUpdate();
    }

    public override void standOffUpdate()
    {
        switch (SOS)
        {
            case StandOffStage.OpenState:
                intialState();
            break;

            case StandOffStage.Attack:
                attackState();
            break;

            case StandOffStage.AttackSuccessful:
                attackSuccessful();
            break;

            case StandOffStage.WindDown:
                attackWindDown();
                break;

            case StandOffStage.EndAttack:
                Destroy(targetController.gameObject, 0.7f);

                end();
            break;

            case StandOffStage.EmptyState:

            break;



            default:
                Debug.Log("Error in StandOffState");
             break;
        }
    }

    void attackState()
    {
        if ((timer > 0.5f && sideFinished >= 2)|| timer > 3 )
        {
            resetAnimationListeners();
            roll();
            timer = 0;
        }
    }

    void attackWindDown()
    {
        if (timer > 0.2)
        {
            backPanel.GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 0.40f);
        }
        if((timer > 1 && right )|| timer > 3)
        {
            timer = 0;
            SOS = StandOffStage.EndAttack;
        }
    }

    void showDifference(int amount, bool sign)
    {
        Text txt = GameObject.Find("uI_Difference_Text").GetComponent<Text>();
        if (sign)
        {
            txt.color = Color.green;
        }
        else
        {
            txt.color = Color.red;
        }
        txt.text = amount.ToString();
    }

    void initiateAttack()
    {

        playSound(Resources.Load<AudioClip>("Audio/SoundEffects/slash"));
        attackAttempt.getAbility().spendMana(attackAttempt.getAttacker());
    }

    void attackSuccessful()
    {
        resetAnimationListeners();
        cast();
        backPanel.GetComponent<SpriteRenderer>().color = Color.white;
        rightSide.spawnEffectOnCharacter(ability.getEffectObjectName());
        rightSide.getAnimator().SetBool("hit", true);
        rightSide.getAnimator().SetTrigger("land");
        timer = 0;
        playSound(Resources.Load<AudioClip>("Audio/SoundEffects/hit"));
        SOS = StandOffStage.WindDown;
        targetController.setInfo();
    }
    public void roll()
    {
        int rollAmount = attackAttempt.getRandom();
        //   rollText.text = rollAmount.ToString();

        if ((rollAmount + toHitNumber) > 10)
        {
            showDifference(toHitNumber - rollAmount, true);
            SOS = StandOffStage.AttackSuccessful;
            switch (attackAttempt.getReactionUsed())
            {
                case reactionType.Dodge:
                    if ((rollAmount + toHitNumber + attackAttempt.getAttackee().getMovement() < 10))
                    {
                        attackAttempt.getAttackee().spendMovement((10 + toHitNumber) - rollAmount);

                        SOS = StandOffStage.EndAttack;
                        rightSide.effectMessage("Dodged " + (10 + toHitNumber - rollAmount).ToString());
                        playSound(Resources.Load<AudioClip>("Audio/SoundEffects/dodge"));
                        attackAttempt.getAttackee().getCharacter().addBuff(new DodgeFatigue(attackAttempt.getAttackee().getCharacter()));

                    }
                    break;

                case reactionType.Block:
                    reductionAmount = 2;
                    break;
            }
        }
        else
        {

            switch ((int)attackAttempt.getReactionUsed())
            {
                case 0:
                    rightSide.effectMessage("Dodged");

                    playSound(Resources.Load<AudioClip>("Audio/SoundEffects/dodge"));
                    attackAttempt.getAttackee().getCharacter().addBuff(new DodgeFatigue(attackAttempt.getAttackee().getCharacter()));

                    break;


                case 1:
                    rightSide.effectMessage("Blocked");
                    playSound(Resources.Load<AudioClip>("Audio/SoundEffects/block"));
                    attackAttempt.getAttackee().getCharacter().addBuff(new BlockFatigue(attackAttempt.getAttackee().getCharacter()));
                    break;


                case 2:
                    rightSide.effectMessage("Parried");
                    playSound(Resources.Load<AudioClip>("Audio/SoundEffects/parry"));
                    attackAttempt.getAttackee().getCharacter().addBuff(new ParryFatigue(attackAttempt.getAttackee().getCharacter()));
                    if (ability.getModType() == ModType.Melee)
                    {
                        attackAttempt.getAttacker().takeDamage(attackAttempt.getAttackee().getMeleeBonus());
                        leftSide.effectMessage(attackAttempt.getAttackee().getMeleeBonus().ToString());
                    }
                    break;


            }


            showDifference(toHitNumber - rollAmount, false);
            SOS = StandOffStage.EndAttack;

        }
    }

    void intialState()
    {
        if (timer > 1f)
        {
            if (attackAttempt.getAttackee().GetAllegiance() == CharacterAllegiance.Controlled)
            {
                reactSelected = false;
                if (attackAttempt.getReactionUsed() != reactionType.None)
                {
                    reactSelected = true;
                }
            }

            if (reactSelected)
            {
                resetAnimationListeners();
                initiateAttack();
                timer = 0;
                setUIValues();
                SOS = StandOffStage.Attack;
                rightSide.setReaction((int)attackAttempt.getReactionUsed());

            }
        }
    }

    public void setUIValues()
    {
        foreach (Text text in rollPanel.GetComponentsInChildren<Text>())
        {
            switch (text.name)
            {
                case "uI_ToHit_Text":
                    text.text = attackAttempt.getHitMatrix()[(int)attackAttempt.getReactionUsed(), 0].ToString();
                    break;

                case "uI_ToReact_Text":
                    text.text = attackAttempt.getHitMatrix()[(int)attackAttempt.getReactionUsed(), 1].ToString();
                    break;

                case "uI_Damage_Text":
                    text.text = attackAttempt.getDamage().ToString();
                    break;

                case "uI_ToHitNumber_Text":
                    toHitNumber = attackAttempt.getHitMatrix()[(int)attackAttempt.getReactionUsed(), 2];

                    if (toHitNumber < 0)
                    {
                        text.fontSize = 14;
                        text.text = "Impossible";
                    }
                    else if (toHitNumber > 11)
                    {
                        text.fontSize = 14;
                        text.text = "Overwhelming";
                    }
                    else
                    {
                        text.text = toHitNumber.ToString() + "0%";
                    }
                    break;

                case "uI_ReactionUsed_Text":
                    switch (attackAttempt.getReactionUsed())
                    {
                        case reactionType.Block:
                            text.text = "Block";
                            break;

                        case reactionType.Dodge:
                            text.text = "Dodge";
                            break;
                        case reactionType.Parry:
                            text.text = "Parry";
                            break;
                    }
                    break;
            }
        }
    }

    protected override void cast()
    {
        storedDamage = attackAttempt.cast(rightSide);
        damageText.text = storedDamage.ToString();
        BattleController.ActiveBattleController.BEL.addEvent(attackAttempt.getAttacker().getName(), "Hit", attackAttempt.getAbility().name, attackAttempt.getAttackee().getName(), storedDamage.ToString());

        rightSide.effectMessage(damageText.text);
    }
}
