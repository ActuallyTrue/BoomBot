using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleActivateButton : MonoBehaviour
{

    public bool isActivated;

    // Start is called before the first frame update
    void Start()
    {
        isActivated = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider c) {
        if (c.gameObject.CompareTag("Player")) {
            if (!isActivated) {
                EventManager.TriggerEvent<Vector3>("buttonClickAudio", this.transform.position);
                isActivated = true;
            }
           
        }
    }
}
