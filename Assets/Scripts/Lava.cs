using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lava : MonoBehaviour
{
    private health player;

    void Start() {
        player = GetComponent<health>();
    }

    void OnTriggerEnter() {
        player.currHealth = 0;
    }
}
