using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class mainMenu : MonoBehaviour
{
    public GameObject instructions;
    // Start is called before the first frame update
    public void startGame()
    {
        SceneManager.LoadScene("Main");
    }

    public void openInstructions()
    {
        instructions.SetActive(true);
    }

    public void closeInstructions()
    {
        instructions.SetActive(false);
    }

    // Update is called once per frame
    public void endGame()
    {
        Application.Quit();
    }
}
