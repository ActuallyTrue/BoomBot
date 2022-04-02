using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DroneWanderState : DroneState
{
    Transform targetedWaypoint;
    // Start is called before the first frame update
    public override void Enter(DroneStateInput stateInput, CharacterStateTransitionInfo transitionInfo = null)
    {
        //base.Enter(stateInput, transitionInfo);
        stateInput.rb.velocity = new Vector2(-stateInput.drone.maxSpeed, 0f);
        targetedWaypoint = stateInput.drone.waypoint1;
    }

    // Update is called once per frame
    public override void Update(DroneStateInput stateInput)
    {
    
            if (targetedWaypoint == stateInput.drone.waypoint2)
            {
                float distance = Vector3.Distance(stateInput.drone.transform.position, targetedWaypoint.position);
                Vector3 moveDirection = targetedWaypoint.position - stateInput.drone.transform.position;
                moveDirection.Normalize();
                stateInput.rb.transform.rotation = Quaternion.LookRotation(moveDirection);
                moveDirection *= stateInput.drone.maxSpeed;
                stateInput.rb.velocity = moveDirection;
                if (distance < 0.05f) {
                    targetedWaypoint = stateInput.drone.waypoint1;
                }
            }
            if (targetedWaypoint == stateInput.drone.waypoint1)
            {      
                float distance = Vector3.Distance(stateInput.drone.transform.position, targetedWaypoint.position);
                Vector3 moveDirection = targetedWaypoint.position - stateInput.drone.transform.position;
                moveDirection.Normalize();
                stateInput.rb.transform.rotation = Quaternion.LookRotation(moveDirection);
                moveDirection *= stateInput.drone.maxSpeed;
                stateInput.rb.velocity = moveDirection;
               if (distance < 0.05f) {
                    targetedWaypoint = stateInput.drone.waypoint2;
                }
            }
        
        if (stateInput.enemy_controller.spotPlayerByDistance(stateInput.player.transform.position)) {
             stateInput.rb.velocity = Vector2.zero;
             character.ChangeState<DroneChaseState>();
             return;
        } 
    }
}
