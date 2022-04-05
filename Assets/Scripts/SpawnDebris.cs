using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnDebris : MonoBehaviour
{
    public GameObject[] debrisList;
    private int debrisListCount;

    private float rateOfSpawn;
    private float time;
    private GameObject spawnedObject;
    private GameObject debris;

    // Start is called before the first frame update
    void Start()
    {
        rateOfSpawn = Random.Range(1f, 5f);
        time = rateOfSpawn;

        debrisListCount = debrisList.Length;

        int rand = Random.Range(0, debrisListCount);
        debris = debrisList[rand];

        debris.transform.localScale = new Vector3(5, 5, 5);
    }

    // Update is called once per frame
    void Update()
    {
        time -= Time.deltaTime;
        if (time <= 0f) {
            spawnedObject = (GameObject) Instantiate(debris, this.transform.position, Quaternion.identity);
            spawnedObject.tag = "Debris";
            time = rateOfSpawn;
        }
        
    }
}
