using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Debris : MonoBehaviour
{
    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Boombot");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision c) {
        if (c.gameObject.CompareTag("Floor")) {
            Destroy(this.gameObject, 2.0f);
        }

        if (c.gameObject.CompareTag("Player")) {
            player.GetComponent<Rigidbody>().AddForce((player.transform.forward + new Vector3(2, 2, 2)) * -15f,ForceMode.Impulse);
        } 
    }

}
