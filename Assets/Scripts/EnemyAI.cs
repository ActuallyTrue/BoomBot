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

        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetFloat("speed", agent.velocity.magnitude / agent.speed);
        // switch (aiState) {
        //     case AIState.Stationary:
        //     if (currWayPoint != -1)
        //     {
        //         if (Vector3.Distance(transform.position, waypoints[currWayPoint].transform.position) < 1.0f && !agent.pathPending)
        //         {
        //             setNextWayPoint();
        //         }
        //     }
        //     break;
        //     case AIState.Moving:
        if (Vector3.Distance(transform.position, movingWaypoint.transform.position) < 1.0f && !agent.pathPending)
        {
            anim.SetBool("forward", false);
            anim.SetTrigger("attack");
        }
        float lookaheadTime = (movingWaypoint.transform.position - agent.transform.position).magnitude / agent.speed;
        Vector3 targetPos = movingWaypoint.transform.position + movingWaypoint.GetComponent<VelocityReporter>().velocity * lookaheadTime;
        agent.SetDestination(targetPos);
        bool cantGetThereFromHere = NavMesh.Raycast(movingWaypoint.transform.position, targetPos, out var hit, NavMesh.AllAreas);
        if (!cantGetThereFromHere)
        {
            // destinationTracker.transform.position = targetPos;
            agent.SetDestination(targetPos);
        }
        else
        {
            // destinationTracker.transform.position = hit.position;
            agent.SetDestination(hit.position);
        }
        //     break;
        //     default:
        //     break;
        // }
    }
}
