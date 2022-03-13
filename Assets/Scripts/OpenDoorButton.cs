using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoorButton : MonoBehaviour
{
    public GameObject buttons;

    public Animator anim;
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
        if (button.pressed)
        {
            if (anim != null)
            {
                anim.SetBool("open", true);
                anim.SetBool("close", false);
            }
            
        } else{
            if (anim != null)
            {
                anim.SetBool("open", false);
                anim.SetBool("close", true);
            }
            
        }
        // if (button.getActivatedEnter() && !alreadyEntered) {
        //     //print(alreadyEntered);
        //     alreadyEntered = true;
            
        //     button.setActivatedEnter();
            
        // } else {
        //     alreadyEntered = false;
        // }
        // print(alreadyEntered);
        // if (button.getActivatedExit() && !alreadyExited) {
        //     alreadyExited = true;
        //     anim.SetBool("open", false);
        //     anim.SetBool("close", true);
        //     button.setActivatedExit();
            
        // }
        // else {
        //     alreadyExited = false;
        // }
    }
}
