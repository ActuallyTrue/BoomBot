using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class insLoad : MonoBehaviour
{
    public GameObject instructions;
    public bool done = false;

    // Start is called before the first frame update
    void Start()
    {
        if (!done){
            instructions.SetActive(true);
            done = true;
            StartCoroutine(timeC());
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private IEnumerator timeC()
    {
        yield return new WaitForSeconds(3);
        instructions.SetActive(false);
    }
}
