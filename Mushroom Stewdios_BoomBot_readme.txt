i. Start scene file: storyScene


ii. How to play and what parts of the level to observe technology
WASD/joystick to move around
Space/bottom face button to jump
Mouse/right stick to move camera
Right click/right trigger to blast off (you will fly in the direction of the camera)


First Level: Tutorial Level. You will spawn in the level, and you need to explode/push a yellow box onto the glowing yellow button on the ground, then explode up onto the platforms to reach the door and proceed to the next level.
Second Level: Explode from platform to platform to reach the other side without falling into the lava.
Third Level: Maneuver a box onto the glowing yellow button to open the door while avoiding lava and destroying/avoiding enemies, then explode onto the platforms to reach the door and proceed to the next level.
Fourth Level: Explode from platform to platform while avoiding circular saws and destroying/avoiding enemies to reach each corner of the map. Each corner has a glowing pink button, which is activated permanently by the playing stepping on them. Activate all four purple buttons to open the door, and then proceed to the next level.
Fifth Level: This level consists of falling debris from the sky that deals damage to the player when the player is hit by it. Avoid the debris and explode from platform to platform to reach the other side of the level. Step through the door way and onto the outside platform to win the game.


Enemies are present in all levels except for the second level.


iii. Known problem areas


iv. Manifest of which files authored by each teammate:
1. Detail who on the team did what
2. For each team member, list each asset implemented. 
3. Make sure to list C# script files individually so we can confirm



# Matt Chen

I was in charge of the explosion physics system (i.e. managing how explosions from the boom bot's movement interact with the world at large) as well as creating the audio system.

For the explosions physics system, I created one function on the `/scripts/StateMachines/Player StateMachine/StateMachineController.cs` file and modified another to use this function.

For the audio and events manager, I made everything in the Events folder, and made two Prefabs, the EventManager as well as the 3DEventSound.

C# Scripts I created/edited:
Assets/Scripts/StateMachines/Player StateMachine/StatePlayerController.cs
Assets/Events/Audio/AudioManager.cs
Assets/Events/EventManager.cs


Minor changes mostly dealing with sound:

- Assets/Scripts/health.cs
- Assets/Scripts/EnemyAI.cs
- Assets/ShootProjectile.cs
- Assets/Scripts/ActivateButton.cs
- Assets/Scripts/OpenDoorButton.cs


# Zehao Tan

I was in charge of the AI enemy development and functionality. For the visual design of the enemy, I imported an external asset from the Unity store (https://assetstore.unity.com/packages/3d/characters/robots/scifi-enemies-and-vehicles-15159).

I created the Animator and the control script for the AI enemy (in /scripts/EnemyAI.cs), as well as a projectile that the enemy fires that hurts the player (in /scripts/ShootProjectile.cs & in /scripts/MoveProjectile.cs).

C# Scripts I created/edited:
/scripts/EnemyAI.cs
/scripts/ShootProjectile.cs
/scripts/MoveProjectile.cs



# Sukriti Bhardwaj

I was in charge of the GUI development. I created the UI and the scripting of the healthbar, the start menu, the pause menu and the checkpoints. 

For the healthbar, I imported the box border asset from https://weeklyhow.com/how-to-make-a-health-bar-in-unity/ and the heart asset from https://opengameart.org/content/heart-2. The scripts I created are /scripts/health.cs, /scripts/healthBar.cs.

For the checkpoints, I created /scripts/playerPosition.cs,  /scripts/checkPointDetect.cs,  /scripts/checkPointSave.cs and used the asset from https://assetstore.unity.com/packages/templates/packs/snaps-art-hd-buried-memories-volume-2-serekh-145780.

For the menu, I created /scripts/mainMenu.cs and /scripts/pause.cs. 

Since the Alpha, I worked on making the story scene for which I created a new story scene where text object is shown at different intervals. I created the Assets/Scripts/storyNextScene.cs to take the user to the start menu after the story scene. I also created a menu which pops up when the player dies in Assets/Scripts/death.cs which gives them the option to restart at checkpoint or quit the game. I also added the credit functionality to the menu.

C# Scripts I created/edited:
/scripts/health.cs
/scripts/healthBar.cs
/scripts/playerPosition.cs
/scripts/checkPointDetect.cs
/scripts/mainMenu.cs
/scripts/pause.cs
/scripts/storyNextScene.cs
/scripts/death.cs


# Heather Zhu

I was in charge of the level design. I worked on building the physical environment that the player interacts with. I created 5 separate "levels", each with its own puzzles and/or obstacles, that the player must traverse through in order to win the game.

I implemented a lot of prefabs that were included in the asset pack that we used (Snaps Art HD | Buried Memories Volume 2: Serekh: https://assetstore.unity.com/packages/templates/packs/snaps-art-hd-buried-memories-volume-2-serekh-145780) to build the levels. I also worked on the sliding doors, where if either the player or a box sits on a yellow button, the door will slide open (in /scripts/OpenDoorButton.cs, /scripts/ActivateButton.cs, /scripts/CanActivateButton.cs), and I made the animator for the door. I also made the death condition when the player falls into the lava in the second (in /scripts/Lava.cs). I also created spawners for both the boxes in the third level (boxes de-spawn when hitting lava and respawn in their original position) and spawners for the falling debris in the last level (/scripts/BoxSpawner.cs, /scripts/SpawnDebris.cs, /scripts/Debris.cs). I also implemented the circular saws in the fourth level and animated them and made them deal damage to the player (/scripts/Saw.cs).

I also implemented the button/door interaction in the fourth level (which is different from previous levels because the previous yellow buttons required constant contact with either the player or a yellow box to keep the door open, and there is only one button needed to be activated in order to keep the door open, while in the fourth level, there are 4 buttons that need to be activated, and they only need to be activated once when the player steps on the button, and it'll stay activated). This is in /scripts/SingleActivateButton.cs and /scripts/SingleActivateDoor.cs. I also gave the player a 1 second invulnerability period after taking damage to avoid constant damage when colliding with an object just once (in /scripts/health.cs). I also made prefabs for both types of buttons and both types of spawners.

C# Scripts I created/edited:
/scripts/OpenDoorButton.cs
/scripts/ActivateButton.cs
/scripts/CanActivateButton.cs
/scripts/Lava.cs
/scripts/BoxSpawner.cs
/scripts/SpawnDebris.cs
/scripts/Debris.cs
/scripts/Saw.cs
/scripts/SingleActivateButton.cs
/scripts/SingleActivateDoor.cs
/scripts/health.cs



# Daniel Otaigbe

I was in charge of the player controller and camera.

For the player controller I created: Character.cs (a parent class to base state machines off of), Trigger.cs (to help with state changes), 
and everything in /scripts/StateMachines. StatePlayerController contains utilities for moving the player while everything in /scripts/StateMachines/PlayerStateMachine/Player States
contains scripts that has logic for specific character states (like Idle or Jumping). I used the Jammo-Character as well as animations imported
from mixamo to model and animate the character. I used the unity add-on Cinemachine to setup the camera. I used Rewired to make the game work
with both controllers and mouse and keyboard. I created GameEnder.cs to end the game and send the player back to the title
screen at the end of the level. I also made small edits and bug fixes to: ActivateButton.cs, OpenDoorButton.cs, EnemyAI.cs, health.cs, Lava.cs, and pause.cs

C# Scripts I created/edited:
/scripts/Character.cs
/scripts/Trigger.cs
/scripts/StateMachines (This is a folder, I created everything in it (10 Scripts))
/scripts/ActivateButton.cs
/scripts/OpenDoorButton.cs
/scripts/EnemyAI.cs
/scripts/health.cs
/scripts/Lava.cs
/scripts/pause.cs

Credits:

Asset Packs:
- Snaps Art HD | Buried Memories Volume 2: Serekh: https://assetstore.unity.com/packages/templates/packs/snaps-art-hd-buried-memories-volume-2-serekh-145780
- https://assetstore.unity.com/packages/3d/characters/robots/scifi-enemies-and-vehicles-15159
- https://assetstore.unity.com/packages/3d/characters/jammo-character-mix-and-jam-158456
- https://assetstore.unity.com/packages/2d/gui/icons/simple-modern-crosshairs-pack-1-79034
- https://assetstore.unity.com/packages/3d/characters/robots/sci-fi-drones-90326
- https://weeklyhow.com/how-to-make-a-health-bar-in-unity/
- https://opengameart.org/content/heart-2

Sounds:
- door close: https://freesound.org/people/primeval_polypod/sounds/156507/
- door open: https://freesound.org/people/NeoSpica/sounds/425090/
- button: https://freesound.org/people/JarredGibb/sounds/219477/
- enemy pew: https://freesound.org/people/SeanSecret/sounds/440661/
- BoomBot hit: https://freesound.org/people/jorickhoofd/sounds/160045/
- footstep sound: https://freesound.org/people/Snapper4298/sounds/178187/
- enemy death hiss: https://freesound.org/people/Zeval34/sounds/578484/
- explosion: from milestone template
