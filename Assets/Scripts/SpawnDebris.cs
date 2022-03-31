using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnDebris : MonoBehaviour
{
    public GameObject debris;

    private float rateOfSpawn;
    private float time;
    private GameObject spawnedObject;
    private ArrayList debrisList;

    // Start is called before the first frame update
    void Start()
    {
        rateOfSpawn = Random.Range(0.5f, 5f);
        time = rateOfSpawn;

        debris.transform.localScale = new Vector3(5, 5, 5);
        debrisList = new ArrayList();
    }

    // Update is called once per frame
    void Update()
    {
        time -= Time.deltaTime;
        if (time <= 0f) {
            spawnedObject = (GameObject) Instantiate(debris, this.transform.position, Quaternion.identity);
            debrisList.Add(spawnedObject);
            time = rateOfSpawn;
        }

        // foreach (GameObject obj in debrisList) {
        //     print(obj);
        //     if (obj != null && obj.transform.position.y <= -20) {
        //         Destroy(obj, 1.0f);
        //         debrisList.Remove(obj);
        //     }
        // }

        if (Input.GetKey("g") && debrisList.Count > 0) {
            Destroy((GameObject) debrisList[0]);
            debrisList.Remove((GameObject) debrisList[0]);
        }
        
    }
}
