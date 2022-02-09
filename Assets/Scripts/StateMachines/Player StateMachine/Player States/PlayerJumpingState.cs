using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpingState : PlayerState {
    public override void Enter(PlayerStateInput stateInput, CharacterStateTransitionInfo transitionInfo = null)
    {
        //stateInput.anim.Play("Player_jump");
        //stateInput.lastXDir = 0;
    }

    public override void Update(PlayerStateInput stateInput)
    {
        stateInput.playerController.isGrounded = stateInput.playerController.checkIfGrounded();

        if (stateInput.playerController.tookDamage()) {
            stateInput.playerController.setDamaged(false);
            LaunchStateTransitionInfo transitionInfo = new LaunchStateTransitionInfo(stateInput.playerController.launchVelocity, stateInput.playerController.moveAfterLaunchTime, true);
            character.ChangeState<PlayerLaunchState>(transitionInfo);
            return;
        }

        if (stateInput.rb.velocity.y <= 0)
        {
            character.ChangeState<PlayerFallingState>(new PlayerFallingTransitionInfo(false));
        }
        else if (stateInput.player.GetButtonUp("Jump"))
        {
            stateInput.playerController.JumpRelease();
            character.ChangeState<PlayerFallingState>(new PlayerFallingTransitionInfo(false));
        }
    }


    public override void FixedUpdate(PlayerStateInput stateInput)
    {
        stateInput.playerController.HandleMovement();
    }
}
