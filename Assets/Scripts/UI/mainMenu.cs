using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class mainMenu : MonoBehaviour
{
    public GameObject instructions;
    public GameObject credits;
    public GameObject creditButton;
    // Start is called before the first frame update
    public void startGame()
    {
        SceneManager.LoadScene("Main");
    }

    public void openInstructions()
    {
        creditButton.SetActive(false);
        instructions.SetActive(true);
    }

    public void closeInstructions()
    {
        creditButton.SetActive(true);
        instructions.SetActive(false);
    }

    public void openCredit()
    {
        credits.SetActive(true);
    }

    public void closeCredit()
    {
        credits.SetActive(false);
    }

    // Update is called once per frame
    public void endGame()
    {
        Application.Quit();
    }
}
