using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Rewired;
using UnityEngine.SceneManagement;
using Cinemachine;

public class StatePlayerController : MonoBehaviour
{
    public float moveSpeed = 6f;
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

    [HideInInspector]
    public Vector2 moveInput;
    public Player player;
    public int playerId;

    //the player's rigidbody
    public Rigidbody rb;

    public GameObject groundChecker;
    public bool isGrounded;
    private Vector3 gravity;

    public float jumpGraceTime;

    public PlayerManager playerManager;

    public BoxCollider boxCollider;

    public Animator anim;
    public SpriteRenderer spriteRenderer;
    [HideInInspector]
    public bool invincible = false;
    [HideInInspector]
    private bool damaged = false;

    public float health = 100f;
    public float invincibilityTime;

    public bool canAct = true;

    public Camera camera;

    void Start()
    {
        playerId = 0;
        player = ReInput.players.GetPlayer(playerId);
        player.controllers.hasKeyboard = true;
        rb = GetComponent<Rigidbody>();
        playerManager = GetComponent<PlayerManager>();
        boxCollider = GetComponent<BoxCollider>();
        camera = Camera.main;
        gravity = Physics.gravity * gravityScale;
    }

    public void FixedUpdate() {
        if (isGrounded == false)
        {
            Debug.Log(gravity);
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
        rb.AddForce(camera.transform.forward * blastForce, ForceMode.Impulse);
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

    public bool checkIfGrounded() {
        RaycastHit hit;
        int layerMask = 1 << 6;
        if (Physics.Raycast(groundChecker.transform.position, Vector3.down, out hit, 1f, layerMask))
        {
            if (hit.collider.gameObject.layer == 6) { //the ground layer
                return true;
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


    public void HandleMovement()
    {
        moveInput = player.GetAxis2D("MoveHorizontal", "MoveVertical");
        if (rb != null) {
            Vector3 velocity = CalculatePlayerVelocity(rb.velocity, moveInput, moveSpeed, velocityXSmoothing, velocityZSmoothing, accelerationTime);
            rb.velocity = velocity;
        }
        HandleRotation();
    }

    public void HandleRotation()
    {
        if (rb.velocity != Vector3.zero)
        {
            Vector3 movementDir = rb.velocity;
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

     //void OnCollisionEnter(Collision collision)
     //{
     //    if (collision.gameObject.CompareTag("Platform"))
     //    {
     //        transform.parent = collision.transform;
     //    }
     //}

    //private void OnCollisionExit(Collision collision)
    //{
    //    transform.parent = null;
    //}

    // public void OnTriggerEnter(Collider collider) {
    //     if (collider.gameObject.layer == 12) { //if it's a hazard
            
    //     }
    // }

    public void takeDamage(bool damaged) {
        if (invincible == false) {
            setDamaged(damaged);
            health -= 5f;
            SetPlayerInvincibility(true);
            if (health <= 0) {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }  
    }

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
}
