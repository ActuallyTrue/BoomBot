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
            check.lastCheckpoint = new Vector3(187.18f, -0.12f, 100.5f);
            SceneManager.LoadScene(0);
        }
    }
}
