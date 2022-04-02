using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float minChaseDistance = 10;

    public SphereCollider sphereCollider;

    public GameObject explosionPrefab;

    public StatePlayerController player;

    void Start() {
        sphereCollider = GetComponent<SphereCollider>();
        player = FindObjectOfType<StatePlayerController>();
        
    }


    public bool spotPlayerByDistance(Vector2 playerPos) {
        float distance = Vector2.Distance(this.gameObject.transform.position, playerPos);
        if (distance < minChaseDistance) {
            return true;
        }
        return false;
    }


    public virtual void Die() {
        Instantiate(explosionPrefab, transform.position, Quaternion.identity);
    }

    private void OnCollisionEnter(Collision other) {
        //7 is the player layer
        Collider[] collisionList = Physics.OverlapSphere(gameObject.transform.position, 5f, 7);
        if (collisionList.Length > 0)
        {
            player.takeDamage();
        }
        Die();
        Destroy(this.gameObject);
    }
   
}