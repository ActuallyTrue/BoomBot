using System.Runtime.CompilerServices;
using System;
using System.Globalization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLaunchState : PlayerState
{
    private float launchTime = 0.5f;
    private float timer;
    
    private bool stopped;


    public override void Enter(PlayerStateInput stateInput, CharacterStateTransitionInfo transitionInfo = null)
    {
        if (transitionInfo == null) {
            timer = launchTime;
        } else {
            LaunchStateTransitionInfo launchTransition = (LaunchStateTransitionInfo) transitionInfo;
            timer = launchTransition.moveAfterLaunchTime;
            stateInput.rb.AddForce(launchTransition.launchVelocity, ForceMode.Force);
            stateInput.playerController.SetPlayerInvincibility(launchTransition.invincible);
        }
        //stateInput.anim.Play("Player_Jump");
        stateInput.anim.Play("Player_fall");
        stopped = false;
    }

    public override void Update(PlayerStateInput stateInput)
    {   
        stateInput.playerController.isGrounded = stateInput.playerController.checkIfGrounded();

        if (stateInput.playerController.canAct == false) {
            return;
        }
        timer -= Time.deltaTime;

        if (timer <= 0 && stateInput.rb.velocity.y < 0f && stateInput.rb.velocity.y > -5f)
        {
            stateInput.playerController.gravity = Physics.gravity * (stateInput.playerController.gravityScale / 5);
            if (stopped == false)
            {
                stopped = true;
                stateInput.playerController.rb.velocity = new Vector3(0, stateInput.playerController.rb.velocity.y, 0);
            }
        }
        else
        {
            stateInput.playerController.gravity = Physics.gravity * (stateInput.playerController.gravityScale);
        }
        if (timer <= 0 && stateInput.playerController.isGrounded) {
            character.ChangeState<PlayerIdleState>();
        } 
    }
    public override void FixedUpdate(PlayerStateInput stateInput)
    {
        stateInput.playerController.HandleMovementForce();
    }

    public override void ForceCleanUp(PlayerStateInput stateInput)
    {
        stateInput.playerController.gravity = Physics.gravity * (stateInput.playerController.gravityScale);
    }

}

public class LaunchStateTransitionInfo : CharacterStateTransitionInfo
{
    public LaunchStateTransitionInfo(Vector2 launchVelocity, float moveTime, bool invincible) {
        this.launchVelocity = launchVelocity;
        this.moveAfterLaunchTime = moveTime;
        this.invincible = invincible;
    }
    public Vector2 launchVelocity;
    public float moveAfterLaunchTime;
    public bool invincible;

}
