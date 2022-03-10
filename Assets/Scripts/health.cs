using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class health : MonoBehaviour
{
    public int maximHealth = 100;
    public int currHealth = 0;
    public healthBar healthBar;
    // Start is called before the first frame update
    void Start()
    {
        currHealth = maximHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            reduceHealth(10);
        }
    }

    public void reduceHealth(int val)
    {
        currHealth -= val;
        healthBar.updateHealth(currHealth);
    }
}
