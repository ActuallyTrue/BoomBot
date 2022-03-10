using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkPointDetect : MonoBehaviour
{
    // Start is called before the first frame update
    private checkPointSave chkPt;
    void Start()
    {
        chkPt = GameObject.FindGameObjectWithTag("C").GetComponent<checkPointSave>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log(transform.position + "pos");
            chkPt.lastCheckpoint = transform.position;
        }
    }
}
