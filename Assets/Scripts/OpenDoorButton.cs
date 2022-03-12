using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoorButton : MonoBehaviour
{
    public GameObject[] numButtons;

    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider c) {
        if (c.attachedRigidbody != null) {
            CanActivateButton object = c.attachedRigidbody.gameObject.GetComponent<CanActivateButton>();
            if (object != null) {
                anim.SetBool("open", true);
                anim.SetBool("close", false);
            }
        }
    }

    void OnTriggerExit(Collider c) {
        if (c.attachedRigidbody != null) {
            CanActivateButton object = c.attachedRigidbody.gameObject.GetComponent<CanActivateButton>();
            if (object != null) {
                anim.SetBool("open", false);
                anim.SetBool("close", true);
            }
        }
    }
}
