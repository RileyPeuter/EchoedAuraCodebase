using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SiegeCutscene1 : Cutscene
{
    public override void endCutscene()
    {
    }

    public override void executeTrigger(int triggerIndex)
    {
        switch (triggerIndex)
        {
            case 1:
                foreach(CutsceneActorController CAC in actorControllers)
                {
                    CAC.SetMove(Vector2.right, 1f, 0.5f);
                }
            break;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
        actorSpawnLocations.Add(new Vector2(0, 2));
        actorSpawnLocations.Add(new Vector2(0, 4));
        actorSpawnLocations.Add(new Vector2(0, 6));
        actorSpawnLocations.Add(new Vector2(0, 8));

        spawnActors(loadActorsFromStoreCharacters());

        
        
         addSprite("Leigh", "Leigh");


        addFrame("", "The party walk through the halls of the small fort, \npassing the occasional busy soldier.", "");
        addFrame("Leigh", "SOMEONE GET ME AN ESTIMATE ON REINFORCEMENTS", "Leigh", 1);


        if (containsSprite("Ruless"))
        {
            addFrame("Ruless", "Ruless, of House Frost Peak, Cirilia, Acting on behalf of the Magus Gradius \nReporting for service.", "Ruless");
            addFrame("Leigh", "I've little patients for formalities even when I have the energy for them.", "Leigh");

        }
        else if (containsSprite("Morthred"))
        {
            addFrame("Morthred", "General Leigh. We attempted to ge there sooner. Our appologies", "Morthred");
            addFrame("Leigh", "You weren't late, these rebels were early", "Leigh");
        }

        else if (containsSprite("Fray"))
        {
            addFrame("Fray", "General. We're the scouts you ordered.", "Fray");
            addFrame("Leigh", "Sorry we couldn't exactly roll the carpet out. Good to have you", "Leigh");
        }

        else if (containsSprite("Iraden"))
        {
            addFrame("Iraden", "General. Magus Scouts reporting", "Iraden");
            addFrame("Iraden", "I hope we didn't get here late", "Iraden");
            addFrame("Leigh", "Nah, I should be the one appologising ", "Leigh");
        }


        else if (containsSprite("Kim"))
        {
            addFrame("Kim", "Kim! I have arrived", "Kim");
            addFrame("Leigh", "You look like this riverland inbreds we're putting down. I would be more specific next time", "Leigh");
            addFrame("Kim", "Oh yes. Sorry. I'm the scout. Asa told you we're coming, right?", "Kim");
        }


        if (containsSprite("Kim"))
        {
            addFrame("Kim", "General. These so called fighters are tired, ill-fed and ill-trained", "Kim");
            addFrame("Kim", "I'd like the opporutnity to engage them in combat and distinguish myself in battle", "Kim");
            addFrame("Kim", "I could route them in minutes", "Kim");
            addFrame("Leigh", "I could go out now and send them running for their lives, singlehandidly", "Leigh");
            addFrame("Leigh", "They have a tendency to group in the most inconvinient of places, in a strangely organized manner", "Leigh");

        }

        else if (containsSprite("Ruless")){
            addFrame("Ruless", "If you'd permit me to suggest, I don't believe reinforcements are necessairy", "Ruless");
            addFrame("Ruless", "I could march out and break these lines up. I don't believe they'd put up much of a fight", "Ruless");
            addFrame("Leigh", "I could go out now and send them running for their lives, singlehandidly", "Leigh");
            addFrame("Leigh", "They have a tendency to group in the most inconvinient of places, in a strangely organized manner within minutes", "Leigh");
        }

        if (containsSprite("Morthred"))
        {
            addFrame("Morthred", "We have noticed some strangely well equiped archers in the lines of the enemy on our way here", "Morthred");
            addFrame("Leigh", "Yes, something to add the list of strange occurances here", "Leigh");
        }

        else if (containsSprite("Fray"))
        {
            addFrame("Fray", "Our info said that it was a peasant uprising. Not something i'd see Friland archers at ", "Fray");
            addFrame("Leigh", "This fight is strange. It's been a few weeks and i'm not able to get a decent read on anything", "Leigh");
        }

        addFrame("Leigh", "Now, I need you to go out on the battlements. Survery the oncomoing forces.", "Leigh");
        addFrame("Leigh", "Don't sneak around, make yourself known, but don't put yourself into too much danger.", "Leigh");
        addFrame("Leigh", "I expect the north side to hold, but my expectations aren't worth much, so be quick.", "Leigh");

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void spawnVisuals()
    {
        Instantiate(Resources.Load<GameObject>("CutsceneAssets/LeighMeetCutscene"));

    }
}
