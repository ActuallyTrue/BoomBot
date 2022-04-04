using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    NavMeshAgent agent;
    ShootProjectile shootProjectile;
    private Animator anim;
    public GameObject movingWaypoint;

    public enum AIState
    {
        Stationary,
        Moving
    };
    public AIState aiState;

    // public GameObject destinationTracker;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        shootProjectile = GetComponent<ShootProjectile>();
        aiState = AIState.Stationary;
    }

    public void killEnemy()
    {
        agent.enabled = false;
        shootProjectile.enabled = false;
        StartCoroutine(Die());
    }

    IEnumerator Die()
    {
        float timer = 3f;
        while (timer > 0)
        {
            timer -= Time.deltaTime;
            yield return null;
        }
        EventManager.TriggerEvent<Vector3>("deathAudio", this.transform.position);
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (agent.enabled)
        {
            anim.SetFloat("speed", agent.velocity.magnitude / agent.speed);
            if (Vector3.Distance(transform.position, movingWaypoint.transform.position) < 1.0f && !agent.pathPending)
            {
                anim.SetBool("forward", false);
                anim.SetTrigger("attack");
            }
            float distanceToPlayer = (movingWaypoint.transform.position - transform.position).magnitude;
            shootProjectile.enabled = distanceToPlayer < 20;
            float lookaheadTime = distanceToPlayer / agent.speed;
            Vector3 targetPos = movingWaypoint.transform.position + movingWaypoint.GetComponent<VelocityReporter>().velocity * lookaheadTime;
            agent.SetDestination(targetPos);
            bool cantGetThereFromHere = NavMesh.Raycast(movingWaypoint.transform.position, targetPos, out var hit, NavMesh.AllAreas);
            if (!cantGetThereFromHere)
            {
                agent.SetDestination(targetPos);
            }
            else
            {
                agent.SetDestination(hit.position);
            }
        }
    }
}
