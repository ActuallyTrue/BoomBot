using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFallingState : PlayerState {

    private float jumpGraceTimer;

    public override void Enter(PlayerStateInput stateInput, CharacterStateTransitionInfo transitionInfo = null)
    {
        //stateInput.anim.Play("Player_fall");
        if (transitionInfo != null) {
            PlayerFallingTransitionInfo fallingTransitionInfo = (PlayerFallingTransitionInfo) transitionInfo;
            if (fallingTransitionInfo.wasGrounded) {
                jumpGraceTimer = stateInput.playerController.jumpGraceTime;
            } else {
                jumpGraceTimer = -1f;
            }
        }
        
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

        if (jumpGraceTimer >= 0) {

            if (stateInput.player.GetButtonDown("Jump"))
            {
                stateInput.playerController.Jump();
                character.ChangeState<PlayerJumpingState>();
            }
            
            jumpGraceTimer -= Time.deltaTime;
        }

        if (stateInput.playerController.isGrounded)
        {
            character.ChangeState<PlayerIdleState>();
        }
    }


    public override void FixedUpdate(PlayerStateInput stateInput)
    {
        stateInput.playerController.HandleMovement();
    }
}

public class PlayerFallingTransitionInfo : CharacterStateTransitionInfo {

    public bool wasGrounded;
    public PlayerFallingTransitionInfo(bool wasGrounded) {
        this.wasGrounded = wasGrounded;
    }

}
