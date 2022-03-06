using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerState {

    public override void Enter(PlayerStateInput stateInput, CharacterStateTransitionInfo transitionInfo = null)
    {
        stateInput.anim.Play("Player_Idle");
    }

    public override void Update(PlayerStateInput stateInput)
    {
        stateInput.playerController.isGrounded = stateInput.playerController.checkIfGrounded();

        if (stateInput.playerController.canAct == false) {
            return;
        }

        if (stateInput.player.GetButtonDown("Jump") && stateInput.playerController.isGrounded)
        {
            stateInput.playerController.Jump();
            character.ChangeState<PlayerJumpingState>();
        }

        if (stateInput.player.GetButtonDown("Blast"))
        {
            stateInput.playerController.Blast();
            character.ChangeState<PlayerLaunchState>();
        }

        if (stateInput.playerController.tookDamage()) {
            stateInput.playerController.setDamaged(false);
            LaunchStateTransitionInfo transitionInfo = new LaunchStateTransitionInfo(stateInput.playerController.launchVelocity * 500, stateInput.playerController.moveAfterLaunchTime, true);
            character.ChangeState<PlayerLaunchState>(transitionInfo);
            return;
        }
    }


    public override void FixedUpdate(PlayerStateInput stateInput) {
        stateInput.playerController.HandleMovement();
    }

}
