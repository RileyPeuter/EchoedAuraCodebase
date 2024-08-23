# Echoed Aura

Here's my code base (And a whole lot of other assets) for my tactical RPG. 
Feel free to take whatever you'd like from here. 

My code isn't perfect. I'm always getting better. More importantly, getting cleaner and more consistent. 

Check rileysmachinations.com to play it. 

If you wanna roast my code, or give contrustive criticism(cringe) email me at riley@rileysmachinations.com


# Explanation

If you want to get an idea of how the code works...
The Global Game Controller is a class with a static instance. 

The "GGC" handles openning different scenes and storing date between them. 
It can open Battles, Cutscenes or Management. 

A battle requires an ExWhy(map) instance and a BattleController instance. The idea being that I can reuse maps for other missions. 

A cutscene simply requires a Cutscene int to be ready. As Monobehaviours can't be changed from scene to scene, an int is passed instead. I feel like this is bad practice, but functions for this purpose. 

The "Mission" class contains variables to start a battle. This is chosen at "management", set to the GGC, which will then be accessed from the Battle scene when it's opened. 
Management is still in its early stages, so there's not too many different "managements". The current one is a small map, that will expand to a large one in the future, but is currently basic. 

