using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanActivateButton : MonoBehaviour
{
    public bool isDestroyed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter(Collision c) {
        if (c.gameObject.CompareTag("Lava")) {
            Destroy(this.gameObject, 1.0f);
            isDestroyed = true;
        }
    }


}
