using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveProjectile : MonoBehaviour
{
    [SerializeField] private int speed = 25;
    [SerializeField] private float offsetY = 0.7f;
    [SerializeField] private float timeUntilDestroy = 5f;
    [SerializeField] private float maxDistanceAllowed = 20f;
    private Vector3 original;

    // Start is called before the first frame update
    void Start()
    {
        original = transform.localPosition;
        transform.localPosition = new Vector3(original.x, original.y + offsetY, original.z);
    }

    // Update is called once per frame
    void Update()
    {
        transform.localPosition = transform.localPosition + transform.forward * speed * Time.deltaTime;
        if (timeUntilDestroy <= 0 || Vector3.Distance(transform.localPosition, original) >= maxDistanceAllowed)
        {
            Destroy(gameObject);
        }
        timeUntilDestroy -= Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            print("HIT");
        }
        Destroy(gameObject);
    }
}