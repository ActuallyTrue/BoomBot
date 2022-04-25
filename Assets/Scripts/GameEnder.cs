using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEnder : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Player"))
        {
            Cursor.lockState = CursorLockMode.None;
            checkPointSave check = FindObjectOfType<checkPointSave>();
            check.lastCheckpoint = new Vector3(430.0484f, 0f, 415.8f);
            SceneManager.LoadScene(1);
        }
    }
}
