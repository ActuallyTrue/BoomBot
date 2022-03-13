using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoorButton : MonoBehaviour
{
    public GameObject buttons;

    private Animator anim;
    private bool alreadyEntered = false;
    private bool alreadyExited = false;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        ActivateButton button = (ActivateButton) buttons.GetComponent(typeof(ActivateButton));
        if (button.getActivatedEnter() && !alreadyEntered) {
            print("here!");
            alreadyEntered = true;
            anim.SetBool("open", true);
            anim.SetBool("close", false);
            button.setActivatedEnter();
            EventManager.TriggerEvent<Vector3>("doorOpenAudio", this.transform.position);
        } else {
            alreadyEntered = false;
        }
        if (button.getActivatedExit() && !alreadyExited) {
            alreadyExited = true;
            anim.SetBool("open", false);
            anim.SetBool("close", true);
            button.setActivatedExit();
            EventManager.TriggerEvent<Vector3>("doorCloseAudio", this.transform.position);
        }
        else {
            alreadyExited = false;
        }
    }
}
