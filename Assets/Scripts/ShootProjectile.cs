using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootProjectile : MonoBehaviour
{

    [SerializeField] private GameObject projectile;
    [SerializeField] private float secondsBeforeShooting = 1;
    private float currSecondsLeft;

    // Start is called before the first frame update
    void Start()
    {
        currSecondsLeft = secondsBeforeShooting;
    }

    // Update is called once per frame
    void Update()
    {
        if (currSecondsLeft <= 0)
        {
            Shoot();
            currSecondsLeft = secondsBeforeShooting;
        }
        currSecondsLeft -= Time.deltaTime;
    }

    public void Shoot()
    {
        GameObject clone = Instantiate(projectile, new Vector3(transform.position.x, transform.position.y, transform.position.z), transform.rotation);
        clone.SetActive(true);
        EventManager.TriggerEvent<Vector3>("enemyPewAudio", this.transform.position);
    }
}
