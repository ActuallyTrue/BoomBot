using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class pause : MonoBehaviour
{
    public bool paused;
    public GameObject pauseMenu;
    public OptionsMenu optionsMenu;
    private float instructionTime = 3f;

    // Start is called before the first frame update
    void Start()
    {
        pauseMenu.SetActive(false);
        optionsMenu = FindObjectOfType<OptionsMenu>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Escape))
        {
            if (paused)
            {
                Cursor.lockState = CursorLockMode.Locked;
                Resume();
            }
            else
            {
                Cursor.lockState = CursorLockMode.None;
                pauseGame();
            }
        }
    }

    public void pauseGame()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        paused = true;
    }

    public void checkPointGame()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 1f;
        paused = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
        optionsMenu.turnOffInstructionsMenu();
        optionsMenu.turnOffOptionsMenu();
        Time.timeScale = 1f;
        paused = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void quitGame()
    {
        Time.timeScale = 1f;
        paused = false;
        Cursor.lockState = CursorLockMode.None;
        SceneManager.LoadScene("startMenu");
    }

}
