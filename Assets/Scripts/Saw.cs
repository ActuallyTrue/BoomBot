using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Saw : MonoBehaviour
{
    private health playerHealth;
    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        playerHealth = FindObjectOfType<health>();
        player = GameObject.Find("Boombot");
    }

    void OnTriggerEnter(Collider c) {
        if (c.gameObject.CompareTag("Player")) {
            playerHealth.reduceHealth(20);
        } 
    }
}
