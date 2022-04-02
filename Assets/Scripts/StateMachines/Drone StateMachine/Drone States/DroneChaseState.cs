using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DroneChaseState : DroneState
{
    //float timer = 0f
    // Start is called before the first frame update
    public override void Enter(DroneStateInput stateInput, CharacterStateTransitionInfo transitionInfo = null)
    {
        //base.Enter(stateInput, transitionInfo);   
    }

    // Update is called once per frame
    public override void Update(DroneStateInput stateInput)
    {



        if (stateInput.enemy_controller.spotPlayerByDistance(stateInput.player.transform.position) == false)
        {
            character.ChangeState<DroneWanderState>();
        }
        else
        {
            //stateInput.gameobj.transform.position = stateInput.player.transform.position;
            float distance = Vector3.Distance(stateInput.drone.transform.position, stateInput.player.transform.position);
            Vector3 moveDirection = stateInput.player.transform.position - stateInput.drone.transform.position;
            moveDirection.Normalize();
            stateInput.rb.transform.rotation = Quaternion.LookRotation(moveDirection);
            moveDirection *= stateInput.drone.maxSpeed * 2.5f;
            stateInput.rb.velocity = moveDirection;
        }

    }
}
