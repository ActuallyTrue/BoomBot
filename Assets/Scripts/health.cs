using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class health : MonoBehaviour
{
    public int maximHealth = 100;
    public int currHealth = 0;
    public healthBar healthBar;
    public GameObject deathMenu;

    private bool invulnerable = false; 
    private float timer = 1f;


    // Start is called before the first frame update
    void Start()
    {
        currHealth = maximHealth;
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    reduceHealth(10);
        //}

        if (invulnerable) {
            timer -= Time.deltaTime;
            if (timer <= 0f) {
                invulnerable = false;
                timer = 1f;
            }
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Projectile")
        {
            reduceHealth(10);
        }
    }

    public void reduceHealth(int val)
    {
        if (!invulnerable) {
            currHealth -= val;
            if (currHealth <= 0)
            {
                EventManager.TriggerEvent<Vector3>("boomBotDeathAudio", this.transform.position);
                deathMenu.SetActive(true);
                
                //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
            EventManager.TriggerEvent<Vector3>("boomBotHurtAudio", this.transform.position);
            healthBar.updateHealth(currHealth);
            invulnerable = true;
        }
    }
}
