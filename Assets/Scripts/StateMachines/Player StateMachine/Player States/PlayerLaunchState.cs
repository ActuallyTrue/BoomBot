﻿using System.Runtime.CompilerServices;
using System;
using System.Globalization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLaunchState : PlayerState
{
    private float launchTime = 0.5f;
    private float timer;


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
    }

    public override void Update(PlayerStateInput stateInput)
    {   
        if (stateInput.playerController.canAct == false) {
            return;
        }
        timer -= Time.deltaTime;   
        if (timer <= 0) {
            character.ChangeState<PlayerIdleState>();
        } 
    }
    public override void FixedUpdate(PlayerStateInput stateInput)
    {
        if (timer <= 0)
        {
            stateInput.playerController.HandleLerpMovement();
        }
    }

    public override void ForceCleanUp(PlayerStateInput stateInput)
    {
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
