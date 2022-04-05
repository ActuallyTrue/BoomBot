using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class playerPosition : MonoBehaviour
{
    // Start is called before the first frame update
    private checkPointSave chkPt;
    void Start()
    {
        chkPt = GameObject.FindGameObjectWithTag("CP").GetComponent<checkPointSave>();
        transform.position = chkPt.lastCheckpoint;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
