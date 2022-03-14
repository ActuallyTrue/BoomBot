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

# Sukriti Bhardwaj

I was incharge of the GUI development. I created the UI and the scripting of the healthbar, the start menu, the pause menu and the checkpoints. 

For the heathbar, I imported the box border asset from https://weeklyhow.com/how-to-make-a-health-bar-in-unity/ and the heart asset from https://opengameart.org/content/heart-2. The scripts I created are /scripts/health.cs, /scripts/healthBar.cs.

For the checkpoints, I created /scripts/playerPosition.cs,  /scripts/checkPointDetect,  /scripts/checkPointSave and used the asset from https://assetstore.unity.com/packages/templates/packs/snaps-art-hd-buried-memories-volume-2-serekh-145780.

For the menu, I created /scripts/mainMenu.cs and /scripts/pause.cs. 

# Heather Zhu

I was in charge of the level design. I worked on building the physical environment that the player interacts with. I created 3 separate "levels" so far that the player must traverse through in order to win the game.

I implemented a lot of prefabs that were included in the asset pack that we used (Snaps Art HD | Buried Memories Volume 2: Serekh: https://assetstore.unity.com/packages/templates/packs/snaps-art-hd-buried-memories-volume-2-serekh-145780) to build the levels. I also worked on the sliding doors, where if either the player or a box sits on a red button, the door will slide open (in /scripts/OpenDoorButton.cs, /scripts/ActivateButton.cs, /scripts/CanActivateButton.cs), and I made the animator for the door. I also made the death condition when the player falls into the lava in the second (in /scripts/Lava.cs and /scripts/CanDieInLava.cs). 

