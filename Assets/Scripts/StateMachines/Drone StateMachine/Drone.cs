using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drone : Character<Drone, DroneState, DroneStateInput>
{
    public float maxSpeed = 3f;
    public Transform waypoint1;
    public Transform waypoint2;

    protected override void Init()
    {
        stateInput.drone = this;
        stateInput.spriteRenderer = GetComponent<SpriteRenderer>();
        stateInput.rb = GetComponent<Rigidbody>();
        stateInput.boxCollider = GetComponent<BoxCollider2D>();
        stateInput.stateChanged = false;
        stateInput.gameobj = gameObject;
        stateInput.enemy_controller = GetComponent<EnemyController>();
        stateInput.player = GameObject.FindGameObjectWithTag("Player");
    }
    protected override void SetInitialState()
    {
        ChangeState<DroneWanderState>();
    }

}

public abstract class DroneState : CharacterState<Drone, DroneState, DroneStateInput>
{

}

public class DroneStateInput : CharacterStateInput
{
    public SpriteRenderer spriteRenderer;
    public Rigidbody rb;
    public BoxCollider2D boxCollider;
    public bool stateChanged;
    public GameObject lastWall;
    public int lastXDir;
    public Drone drone;
    public GameObject gameobj;
    public EnemyController enemy_controller;
    public GameObject player;
}
