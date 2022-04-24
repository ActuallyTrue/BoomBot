using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Rewired;
using UnityEngine.SceneManagement;
using Cinemachine;
using UnityEngine.AI;

public class StatePlayerController : MonoBehaviour
{
    public float moveSpeed = 6f;
    public float forcePower = 3f;
    public float turnSpeed = 3f;
    public float blastForce = 15f;
    public float gravityScale;
    public float accelerationTime;
    private float velocityXSmoothing;
    private float velocityZSmoothing;
    public float moveAfterLaunchTime;
    private float moveAfterLaunchTimer;
    public float maxJumpVelocity;
    public float minJumpVelocity;
    public Vector2 launchVelocity;
    public float explosionForce = 10f;
    public float explosionRadius = 5f;
    public float explosionUpForce = 1f;

    public GameObject explosionEffect;

    [HideInInspector]
    public Vector2 moveInput;
    public Player player;
    public int playerId;

    //the player's rigidbody
    public Rigidbody rb;

    public GameObject groundChecker;
    public bool isGrounded;

    [HideInInspector]
    public Vector3 gravity;

    public float jumpGraceTime;

    public PlayerManager playerManager;

    public BoxCollider boxCollider;

    public Animator anim;
    public SpriteRenderer spriteRenderer;
    [HideInInspector]
    public bool invincible = false;
    [HideInInspector]
    private bool damaged = false;
    public float invincibilityTime;

    public bool canAct = true;

    public Camera camera;

    public health health;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        playerId = 0;
        player = ReInput.players.GetPlayer(playerId);
        player.controllers.hasKeyboard = true;
        rb = GetComponent<Rigidbody>();
        playerManager = GetComponent<PlayerManager>();
        boxCollider = GetComponent<BoxCollider>();
        anim = GetComponent<Animator>();
        camera = Camera.main;
        gravity = Physics.gravity * gravityScale;
        health = GetComponent<health>();
        SensitivitySaver sensSaver = FindObjectOfType<SensitivitySaver>();
        sensSaver.setSens();
    }

    public void FixedUpdate() {
        if (isGrounded == false)
        {
            rb.AddForce(gravity, ForceMode.Acceleration);
        }
    }

    //if you jump it changes your y velocity to the maxJumpVelocity
    public void Jump()
    {

        rb.velocity = new Vector2(rb.velocity.x, maxJumpVelocity);

    }

    public void Blast()
    {
        float scale = 1f;
        rb.AddForce(camera.transform.forward * blastForce, ForceMode.Impulse);
        Detonate(scale);
        Instantiate(explosionEffect, transform.position, Quaternion.identity);
        EventManager.TriggerEvent<Vector3>("explosionEvent", this.transform.position);
    }

    public void JumpRelease()
    {
        if (rb.velocity.y > minJumpVelocity)
        {
            rb.velocity = new Vector2(rb.velocity.x, minJumpVelocity);
        }
    }

    public Vector3 CalculatePlayerVelocity(Vector3 RBvelocity, Vector2 input, float moveSpeed, float velocityXSmoothing, float velocityZSmoothing, float accelerationTime)
    {
        //camera forward and right vectors:
        Vector3 forward = camera.transform.forward;
        Vector3 right = camera.transform.right;
 
        //project forward and right vectors on the horizontal plane (y = 0)
        forward.y = 0f;
        right.y = 0f;
        forward.Normalize();
        right.Normalize();
 
        //this is the direction in the world space we want to move:
        Vector3 targetVelocity = forward * (input.y * moveSpeed) + right * (input.x * moveSpeed);
        float xVelocity = Mathf.SmoothDamp(RBvelocity.x, targetVelocity.x, ref velocityXSmoothing, accelerationTime);
        float zVelocity = Mathf.SmoothDamp(RBvelocity.z, targetVelocity.z, ref velocityZSmoothing, accelerationTime);
        Vector3 output = new Vector3(xVelocity, RBvelocity.y, zVelocity);
        return output;
    }

    public float getSpeed()
    {
        Vector2 speed = new Vector2(rb.velocity.x, rb.velocity.z);
        return speed.magnitude;
    }

    public bool checkIfGrounded() {
        int layerMask = 1 << 6;
        //for later
        //Physics.Raycast(groundChecker.transform.position, Vector3.down, out hit, 1f, layerMask)
        Vector3 size = boxCollider.size;
        Vector3 center = new Vector3 (boxCollider.center.x, boxCollider.center.y, boxCollider.center.z);

        Vector3 vertex1 = new Vector3 (center.x + size.x / 2, center.y - size.y/3, center.z + size.z / 2);
        Vector3 vertex2 = new Vector3 (center.x - size.x / 2, center.y - size.y/3, center.z - size.z / 2);
        Vector3 vertex3 = new Vector3 (center.x + size.x / 2, center.y - size.y/3, center.z - size.z / 2);
        Vector3 vertex4 = new Vector3 (center.x - size.x / 2, center.y - size.y/3, center.z + size.z / 2);

        Vector3[] vertices = {vertex1, vertex2, vertex3, vertex4};

        foreach (Vector3 vertex in vertices)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.TransformPoint(vertex), Vector3.down, out hit, 0.7f, layerMask))
            {
                if (hit.collider.gameObject.layer == 6) { //the ground layer
                    return true;
                }
            }    
        }
        return false;
    }

    public bool tookDamage() {
        return damaged;
    }

    public void setDamaged(bool enable) {
        damaged = enable;
    }

    public void takeDamage(int damage = 10)
    {
        health.reduceHealth(damage);
    }


    public void HandleMovement()
    {
        moveInput = player.GetAxis2D("MoveHorizontal", "MoveVertical");
        if (rb != null) {
            Vector3 velocity = CalculatePlayerVelocity(rb.velocity, moveInput, moveSpeed, velocityXSmoothing, velocityZSmoothing, accelerationTime);
            rb.velocity = velocity;
        }
        if (Math.Abs(moveInput.x) > 0 || Math.Abs(moveInput.y) > 0)
        {
            HandleRotation(rb.velocity);
        }
        
    }

    public void HandleMovementForce()
    {
        moveInput = player.GetAxis2D("MoveHorizontal", "MoveVertical");
        if (rb != null) {
            //camera forward and right vectors:
            Vector3 forward = camera.transform.forward;
            Vector3 right = camera.transform.right;
    
            //project forward and right vectors on the horizontal plane (y = 0)
            forward.y = 0f;
            right.y = 0f;
            forward.Normalize();
            right.Normalize();
            Vector3 targetDir = forward * (moveInput.y * forcePower) + right * (moveInput.x * forcePower);

            rb.AddForce(targetDir, ForceMode.Acceleration);

            if (Math.Abs(moveInput.x) > 0 || Math.Abs(moveInput.y) > 0)
            {
                HandleRotation(targetDir);
            }
        }
        
    }

    public void HandleRotation(Vector3 movementDir)
    {
        if (rb.velocity != Vector3.zero)
        {
            movementDir = new Vector3(movementDir.x, 0, movementDir.z);
             Quaternion toRotation = Quaternion.LookRotation(movementDir, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, turnSpeed * Time.deltaTime);
        }
    }

    public void HandleLerpMovement()
    {
        moveInput = player.GetAxis2D("MoveHorizontal", "MoveVertical");
        Vector3 velocity = CalculatePlayerVelocity(rb.velocity, moveInput, moveSpeed, velocityXSmoothing, velocityZSmoothing, accelerationTime);
        rb.velocity = Vector3.Lerp(rb.velocity, velocity, 10f * Time.deltaTime);
    }

    public void EmitFootstep()
    {
        EventManager.TriggerEvent<Vector3>("footstepAudio", this.transform.position);
    }

    public Vector2 clampTo8Directions(Vector2 vectorToClamp) {
        if (vectorToClamp.x > 0.1f && (vectorToClamp.y < 0.1f && vectorToClamp.y > -0.1f))
        {
            //right
            return new Vector2(1,0);
        }
        if (vectorToClamp.x > 0.1f && vectorToClamp.y < -0.1f)
        {
            //down right
            return new Vector2(1,-1).normalized;
        }
        if ((vectorToClamp.x < 0.1f && vectorToClamp.x > -0.1) && vectorToClamp.y < -0.1f)
        {
            //down
            return new Vector2(0,-1);
        }
        if (vectorToClamp.x < -0.1f && vectorToClamp.y < -0.1f)
        {
            //down left
            return new Vector2(-1,-1).normalized;
        }
        if (vectorToClamp.x < -0.1f && (vectorToClamp.y < 0.1f && vectorToClamp.y > -0.1f))
        {
            //left
            return new Vector2(-1,0);
        }
        if (vectorToClamp.x < -0.1f && vectorToClamp.y > 0.1f)
        {
            //up left
            return new Vector2(-1,1).normalized;
        }
        if ((vectorToClamp.x < 0.1f && vectorToClamp.x > -0.1) && vectorToClamp.y > 0.1f)
        {
            //up
            return new Vector2(0,1);
        }
        if (vectorToClamp.x > 0.1f && vectorToClamp.y > 0.1f)
        {
            //up right
            return new Vector2(1,1).normalized;
        }

        return Vector2.zero;
    }

    public void setCanAct(bool enable) {
        canAct = enable;
    }

     void OnCollisionEnter(Collision collision)
     {
        if (collision.gameObject.CompareTag("Debris") && collision.impulse.magnitude > 10)
         {
            Debug.Log(collision.impulse.magnitude);
            takeDamage(5);
         }
     }

    //private void OnCollisionExit(Collision collision)
    //{
    //    transform.parent = null;
    //}

    // public void OnTriggerEnter(Collider collider) {
    //     if (collider.gameObject.layer == 12) { //if it's a hazard
            
    //     }
    // }

    public void SetPlayerInvincibility(bool enable) {
        invincible = enable;
        if (enable) {
            StartCoroutine(InvincibleTimer());
        }
    }

    IEnumerator InvincibleTimer() {
        yield return new WaitForSeconds(invincibilityTime);
        SetPlayerInvincibility(false);
    }

    public void pushBack(Vector2 input) {
        rb.AddForce(new Vector2(input.x * -50, input.y * -50), ForceMode.Force);
    }

    /**
     * explosions that affect the world
     * https://www.youtube.com/watch?v=XMDfhHyOacM
     */
    void Detonate(float scale = 1)
    {
        Vector3 explosionPosition = this.transform.position;
        Collider[] colliders = Physics.OverlapSphere(explosionPosition, explosionRadius);
        Debug.Log(explosionForce);
        foreach (Collider hit in colliders)
        {
            Rigidbody rb = hit.GetComponent<Rigidbody>();
            
            EnemyAI ai = hit.GetComponent<EnemyAI>();

            if (ai != null)
            {
                ai.killEnemy();
            }

            if (rb != null && hit.CompareTag("Explodable"))
            {
                rb.AddExplosionForce(explosionForce * scale, explosionPosition, explosionRadius, explosionUpForce, ForceMode.Impulse);
            }
        }
    } 
}
