# P3-TurnBased-AlexandreCrochetGibault

Dawson IVGD - Scripting 2 - Assignment 3 - Alexandre Crochet Gibault

Id :2545874



DESCRIPTION

Simple turn-based combat system

Game features a player and an AI-controlled enemy facing each other in a combat scenario.

Visual presentation is minimal, using placeholder assets, in order to prioritize system design and gameplay logic.



CONTROLS

Keyboard/Mouse

Click to choose which action to do this turn

Open SHAPEWAR Scene



PLAYER'S ACTIONS

2 available actions per turn

Attack

* Deals damage to the enemy
* Triggered via animation
* Damage is applied using an Animation Event for proper timing

Heal

* Restores a portion of the player’s health
* Ends the turn immediately after use

These actions were chosen to keep the gameplay simple while still demonstrating multiple decision paths



ENEMY AI LOGIC

Enemy is fully controlled by a rule-based AI system.

It makes decisions based on its current health:

If enemy health is below 30% it will Defend : Reduces incoming damage by 50% for the next hit

Otherwise it will attach Attack (Plays attack animation and deals damage via animation event)

This approach ensures the AI reacts dynamically to the game state rather than acting randomly.



LIMITATIONS

* AI logic is simple and rule-based (not adaptive)
* No advanced animations (blend trees, layered animations)
* No sound effects implemented

NB : This project is intentionally a Turn Manager focused proof of concept — gameplay is placeholder so the Turn-Base system is  the focus.

