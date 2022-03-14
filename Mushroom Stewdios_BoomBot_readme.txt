i. Start scene file


ii. How to play and what parts of the level to observe technology


requirements
iii. Known problem areas


iv. Manifest of which files authored by each teammate:
1. Detail who on the team did what
2. For each team member, list each asset implemented. 
3. Make sure to list C# script files individually so we can confirm

# Matt Chen

I was in charge of the explosion physics system (i.e. managing how explosions from the boom bot's movement interact with the world at large) as well as creating the audio system.

For the explosions physics system, I created one function on the `/scripts/StateMachines/Player StateMachine/StateMachineController.cs` file and modified another to use this function.

For the audio and events manager, I made everything in the Events folder, and made two Prefabs, the EventManager as well as the 3DEventSound.

# Zehao Tan

I was in charge of the AI enemy development and functionality. For the visual design of the enemy, I imported an external asset from the Unity store (https://assetstore.unity.com/packages/3d/characters/robots/scifi-enemies-and-vehicles-15159).

I created the Animator and the control script for the AI enemy (in /scripts/EnemyAI.cs), as well as a projectile that the enemy fires that hurts the player (in /scripts/ShootProjectile.cs & in /scripts/MoveProjectile.cs).