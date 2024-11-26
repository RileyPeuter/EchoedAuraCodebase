    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KimsTrainingBattleController : BattleController
{
    public override void endBattle()
    {
        GlobalGameController.GGC.getAgency().addCharacter(new Kim());
        GlobalGameController.GGC.completeMission();
        GlobalGameController.GGC.openManagment();
    }

    public override void GBCTexecutions(int index)
    {
        switch (index)
        {
            case 1:
                openEndWindow();
                break;

        }

    }
    public override void loadCharacters()
    {
        characters.Add(GameObject.Instantiate(Resources.Load<GameObject>("TestAssets/Iraden")).GetComponent<BattleCharacterObject>());
        characters[characters.Count - 1].initialize(4, 1, new Iraden(), CharacterAllegiance.Controlled, 0, -4);

        characters[characters.Count - 1].spawnCharacter(map.gridObject);



        characters.Add(GameObject.Instantiate(Resources.Load<GameObject>("TestAssets/Kim")).GetComponent<BattleCharacterObject>());
        characters[characters.Count - 1].initialize(2, 3, new Kim(), CharacterAllegiance.Enemey, 0, -3);

        characters[characters.Count - 1].spawnCharacter(map.gridObject);
    }
            
    public override void objectiveComplete(string id)
    {
        switch (id)
        {
            case "irdHit0":
                activeCutscene = gameObject.AddComponent<GenericBattleCutscene>();
                activeCutscene.initialize(this);

                activeCutscene.addSprite("Fray", "Fray");
                activeCutscene.addSprite("Kim", "Kim");
                activeCutscene.addSprite("Tutorial4", "Tutorial4");

                activeCutscene.addFrame("Fray", "Go for it!", "Fray");
                activeCutscene.addFrame("Tutorial", "You've been hit, but don't worry too much. \n You have a Tactic to use.", "Tutorial");
                activeCutscene.addFrame("Tutorial", "Substituion reveals that what the enemy last hit was a phantom all along", "Tutorial");
                activeCutscene.addFrame("Tutorial", "Tactics will often cost Tactical points (See next to the turn timer) \n,movement speed or Mana", "Tutorial");

                activeCutscene.createDialogueObject();
                activeCutscene.preInitializeTextbox();
                activeCutscene.setFrame(0);
                toggleCutscene(true);


                break;


            case "kmKill0":
                activeCutscene = gameObject.AddComponent<GenericBattleCutscene>();
                activeCutscene.initialize(this);

                activeCutscene.addSprite("Fray", "Fray");
                

                activeCutscene.addFrame("Fray", "Wow...", "Fray");

                activeCutscene.addFrame("Fray", "I didn't actually expect you to beat him.", "Fray");
                activeCutscene.addFrame("Fray", "I'm gonna take him inside and get him some rest. ", "Fray", 1);

                activeCutscene.createDialogueObject();
                activeCutscene.preInitializeTextbox();
                activeCutscene.setFrame(0);
                toggleCutscene(true);

                break;

            case "irKill0":
                activeCutscene = gameObject.AddComponent<GenericBattleCutscene>();
                activeCutscene.initialize(this);
                activeCutscene.addSprite("Kim", "Kim");
                activeCutscene.addSprite("Fray", "Fray");
                activeCutscene.addSprite("Iraden", "Iraden");

                activeCutscene.addFrame("Kim", "Victory!...", "Kim");
                activeCutscene.addFrame("Kim", "Oh...", "Kim");
                activeCutscene.addFrame("Fray", "Huh. I think you might have over did it a little", "Fray");
                activeCutscene.addFrame("Iraden", "Ugh...I'm good...", "Iraden");
                activeCutscene.addFrame("Iraden", "Or I will be", "Iraden");
                activeCutscene.addFrame("Fray", "Ahhh, that's the spirit. \nEven Ruless can only do that once every few days", "Fray", 1);

                activeCutscene.createDialogueObject();
                activeCutscene.preInitializeTextbox();
                activeCutscene.setFrame(0);
                toggleCutscene(true);
                break;

            case "kmReact0":
                activeCutscene = gameObject.AddComponent<GenericBattleCutscene>();
                activeCutscene.initialize(this);

                activeCutscene.addSprite("Fray", "Fray");
                activeCutscene.addSprite("Kim", "Kim");
                activeCutscene.addSprite("Tutorial4", "Tutorial4");

                activeCutscene.addFrame("Fray", "He's right where you want him", "Fray");
                activeCutscene.addFrame("Tutorial", "You missed. But Fray taught you a tactic.", "Tutorial");
                activeCutscene.addFrame("Tutorial", "Select 'just a distraction' and target Kim. This will reveal that your first \n attack was 'just a distraction'", "Tutorial");
                activeCutscene.addFrame("Tutorial", "You will then launch a melee attack, regardless of range, with their last used reaction disabled", "Fray");
                activeCutscene.addFrame("Tutorial", "This is best used against enemies that only have one strong reaction.", "Fray");


                activeCutscene.createDialogueObject();
                activeCutscene.preInitializeTextbox();
                activeCutscene.setFrame(0);
                toggleCutscene(true);

                break;
        }
    }

    private void Start()
    {
        spawnCells = new List<ExWhyCell>();

        initializeBattleController();

        tacticalPoints = 2;

        cursor.transform.position.Set(3, 3, 1);
        cursorCell = map.gridObject.gridCells[3, 3];

        activeCutscene = gameObject.AddComponent<GenericBattleCutscene>();
        activeCutscene.initialize(this);

        activeCutscene.addSprite("Fray", "Fray");
        activeCutscene.addSprite("Kim", "Kim");
        activeCutscene.addSprite("Iraden", "Iraden");


        activeCutscene.addFrame("Fray", "Hey Kimmi. You've been looking for someone more agile to spar with, right?", "Fray");
        activeCutscene.addFrame("Iraden", "Spar? you mean you want me to...", "Iraden");
        activeCutscene.addFrame("Kim", "Ooh, a new training partner!", "Kim");
        activeCutscene.addFrame("Kim", "What weapon do you use? How heavy is it? Do you use unarmed?", "Kim");
        activeCutscene.addFrame("Iraden", "Um...I'm mo...", "Iraden");

        activeCutscene.addFrame("Kim", "Wait, No!", "Kim");
        activeCutscene.addFrame("Kim", "Cria's been telling me that it's important to learn to discern your opponents \n weapon and fighting style. This is a good opportunity", "Kim");
        activeCutscene.addFrame("Kim", "Just don't hold back", "Kim");

        activeCutscene.addFrame("Fray", "Now, you don't need to break his bones to prove you're stronger. He's still a growing boy", "Fray");
        activeCutscene.addFrame("Fray", "And Iraden!", "Fray");
        activeCutscene.addFrame("Fray", "...", "Fray");
        activeCutscene.addFrame("Fray", "Remember the trick I showed you before", "Fray");

        activeCutscene.addFrame("Kim", "Ooh! I'm gonna pretend I didn't hear that.", "Kim");

        initializeGameState();


        initializeAdditionalElements();


        activeCutscene.createDialogueObject();
        activeCutscene.preInitializeTextbox();


        activeCutscene.setFrame(0);

        toggleCutscene(true);

        objList.addObjective(new VagueObjective("irdHit0", 1, BattleEventType.Hit).addDescription("Spar with Kim").addTarget("Kim"));

        objList.addObjective(new VagueObjective("kmReact0", 1, BattleEventType.React).makeInvisible().addReuslt(false.ToString()).addTarget("Iraden"));


        objList.addObjective(new VagueObjective("kmKill0", 1, BattleEventType.Kill).addSubject("Kim").makeInvisible());

        objList.addObjective(new VagueObjective("irKill0", 1, BattleEventType.Kill).addSubject("Iraden").makeInvisible());

        baseTacticalAbilities.Add(new Supposititious(this));
        baseTacticalAbilities.Add(new Distraction(this));

        lookForBattleEventListeners();

        
    }

    // Update is called once per frame
    void Update()
    {
        controls();

        if (getActiveCharacter() != null)
        {
            if (getActiveCharacter().GetAllegiance() != CharacterAllegiance.Controlled)
            {
                ticker();
            }
        }
    }
}
