using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateButton : MonoBehaviour
{
    private bool isActivatedEnter = false;
    private bool isActivatedExit = false;

    public bool pressed = false;

    public bool getActivatedEnter() {
        return isActivatedEnter;
    }

    public bool getActivatedExit() {
        return isActivatedExit;
    }

    public void setActivatedEnter() {
        isActivatedEnter = false;
        EventManager.TriggerEvent<Vector3>("buttonClickAudio", this.transform.position);
        EventManager.TriggerEvent<Vector3>("doorOpenAudio", this.transform.position);
    }

    public void setActivatedExit() {
        isActivatedExit = false;
        EventManager.TriggerEvent<Vector3>("buttonClickAudio", this.transform.position);
        EventManager.TriggerEvent<Vector3>("doorCloseAudio", this.transform.position);
    }

    void OnTriggerEnter(Collider c) {
        if (c.attachedRigidbody != null) {
            CanActivateButton collidingObject = c.attachedRigidbody.gameObject.GetComponent<CanActivateButton>();
            if (collidingObject != null) {
                EventManager.TriggerEvent<Vector3>("buttonClickAudio", this.transform.position);
                EventManager.TriggerEvent<Vector3>("doorOpenAudio", this.transform.position);
            }
        }
    }

    private void OnTriggerStay(Collider other) {
        if (other.attachedRigidbody != null) {
            CanActivateButton collidingObject = other.attachedRigidbody.gameObject.GetComponent<CanActivateButton>();
            if (collidingObject != null) {
                isActivatedEnter = true;
                pressed = true;
            }
        }
    }

    void OnTriggerExit(Collider c) {
        if (c.attachedRigidbody != null) {
            CanActivateButton collidingObject = c.attachedRigidbody.gameObject.GetComponent<CanActivateButton>();
            if (collidingObject != null) {
                isActivatedExit = true;
                pressed = false;
                EventManager.TriggerEvent<Vector3>("buttonClickAudio", this.transform.position);
            EventManager.TriggerEvent<Vector3>("doorCloseAudio", this.transform.position);
            }
        }
    }
}
