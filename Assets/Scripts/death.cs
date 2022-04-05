using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class death : MonoBehaviour
{
    public GameObject deathMenu;
    public bool dead;


    // Start is called before the first frame update
    void Start()
    {
        deathMenu.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        if (deathMenu.activeSelf)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            Time.timeScale = 0f;
            dead = true;
        }

    }
    public void checkPointGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        deathMenu.SetActive(false);
    }

    public void quitGame()
    {
        Application.Quit();
        deathMenu.SetActive(false);
    }
}
