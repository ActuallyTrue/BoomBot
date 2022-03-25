using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lava : MonoBehaviour
{
    private health player;

    void Start() {
        player = FindObjectOfType<health>();
    }

    void OnTriggerEnter(Collider c) {
        if (c.gameObject.CompareTag("Player"))
        {
            player.reduceHealth(100);
        }
    }
}
