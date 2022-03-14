using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lava : MonoBehaviour
{
    private health player;

    void Start() {
        player = GetComponent<health>();
    }

    void OnTriggerEnter(Collider c) {
        if (c.attachedRigidbody != null) {
            CanDieInLava collidingObject = c.attachedRigidbody.gameObject.GetComponent<CanDieInLava>();
            if (collidingObject != null) {
                player.currHealth = 0;
            }
        }
    }
}
