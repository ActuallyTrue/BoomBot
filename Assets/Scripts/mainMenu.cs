using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class mainMenu : MonoBehaviour
{
    // Start is called before the first frame update
    public void startGame()
    {
        SceneManager.LoadScene("checkpoint");
    }

    // Update is called once per frame
    public void endGame()
    {
        Application.Quit();
    }
}
