using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class PlayerManager : Character<PlayerManager, PlayerState, PlayerStateInput> {

	override protected void Init()
    {
        stateInput.stateMachine = this;
        stateInput.playerController = GetComponent<StatePlayerController>();
        stateInput.anim = stateInput.playerController.anim;
        stateInput.rb = GetComponent<Rigidbody>();
        stateInput.boxCollider = GetComponent<BoxCollider>();
        stateInput.stateChanged = false;
        stateInput.player = ReInput.players.GetPlayer(0);
        stateInput.playerController.player = stateInput.player;
    }

    override protected void SetInitialState()
    {
        ChangeState<PlayerIdleState>();
    }

    public PlayerStateInput GetStateInput() {
        return stateInput;
    }

    public PlayerState GetState() {
        return state;
    }
}

public abstract class PlayerState : CharacterState<PlayerManager, PlayerState, PlayerStateInput>
{

}

public class PlayerStateInput : CharacterStateInput
{
    
    public Animator anim;
    public Rigidbody rb;
    public BoxCollider boxCollider;
    public StatePlayerController playerController;
    public bool stateChanged;
    public Player player;
    public PlayerManager stateMachine;

}
