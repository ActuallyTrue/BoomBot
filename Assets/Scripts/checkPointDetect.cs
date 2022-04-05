using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class checkPointDetect : MonoBehaviour
{
    // Start is called before the first frame update
    private checkPointSave chkPt;
    public GameObject chktext;
    void Start()
    {
        chkPt = GameObject.FindGameObjectWithTag("CP").GetComponent<checkPointSave>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            chkPt.lastCheckpoint = transform.position;
            chktext.SetActive(true);
            StartCoroutine(timeA());
        }
    }

    private IEnumerator timeA()
    {
        yield return new WaitForSeconds(1);
        chktext.SetActive(false);
    }
}
