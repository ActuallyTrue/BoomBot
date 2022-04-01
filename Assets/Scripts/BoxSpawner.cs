using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxSpawner : MonoBehaviour
{
    public GameObject box;

    private GameObject spawnedObject;

    // Start is called before the first frame update
    void Start()
    {
        spawnedObject = (GameObject) Instantiate(box, this.transform.position, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        CanActivateButton checkIfDestroyedBox = (CanActivateButton) spawnedObject.GetComponent(typeof(CanActivateButton));
        if (checkIfDestroyedBox.isDestroyed) {
            spawnedObject = (GameObject) Instantiate(box, this.transform.position, Quaternion.identity);
            checkIfDestroyedBox.isDestroyed = false;
        }
    }
}
