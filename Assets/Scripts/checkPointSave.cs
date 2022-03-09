using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkPointSave : MonoBehaviour
{
    private static checkPointSave checkP;
    public Vector3 lastCheckpoint;
    // Start is called before the first frame update
    void Awake()
    {
        if (checkP == null)
        {
            checkP = this;
            DontDestroyOnLoad(checkP);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
