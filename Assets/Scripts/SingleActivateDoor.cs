using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleActivateDoor : MonoBehaviour
{
    public GameObject[] buttonList;
    public Animator anim;

    private bool canOpen;

    // Start is called before the first frame update
    void Start()
    {
        canOpen = false;
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        int count = 0;
        for (int i = 0; i < buttonList.Length; i++) {
            SingleActivateButton button = (SingleActivateButton) buttonList[i].GetComponent(typeof(SingleActivateButton));
            if (button.isActivated) {
                count++;
            }
        }

        if (count == buttonList.Length) {
            if (anim != null && !anim.GetBool("open")) {
                anim.SetBool("open", true);
                anim.SetBool("close", false);
                EventManager.TriggerEvent<Vector3>("doorOpenAudio", this.transform.position);
            }
            
        }
    }
}
