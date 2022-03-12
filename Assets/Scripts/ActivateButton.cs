using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateButton : MonoBehaviour
{
    private bool isActivatedEnter = false;
    private bool isActivatedExit = false;

    public bool getActivatedEnter() {
        return isActivatedEnter;
    }

    public bool getActivatedExit() {
        return isActivatedExit;
    }

    public void setActivatedEnter() {
        isActivatedEnter = false;
    }

    public void setActivatedExit() {
        isActivatedExit = false;
    }

    void OnTriggerEnter(Collider c) {
        if (c.attachedRigidbody != null) {
            CanActivateButton collidingObject = c.attachedRigidbody.gameObject.GetComponent<CanActivateButton>();
            if (collidingObject != null) {
                isActivatedEnter = true;
            }
        }
    }

    void OnTriggerExit(Collider c) {
        if (c.attachedRigidbody != null) {
            CanActivateButton collidingObject = c.attachedRigidbody.gameObject.GetComponent<CanActivateButton>();
            if (collidingObject != null) {
                isActivatedExit = true;
            }
        }
    }
}
