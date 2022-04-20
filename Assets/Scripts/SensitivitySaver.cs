using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class SensitivitySaver : MonoBehaviour
{
    private static SensitivitySaver sensSaver;

    //private cinemachineF
    public float xSensitivity;
    public float ySensitivity;
    // Start is called before the first frame update
    void Awake()
    {
         if (sensSaver == null)
        {
            sensSaver = this;
            //cinemachineFreeLook.m_XAxis.m_MaxSpeed = xSensitivity;

            DontDestroyOnLoad(sensSaver);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        Debug.Log("Test");
    }

    public void saveSens(float xSens, float ySens)
    {
        xSensitivity = xSens;
        ySensitivity = ySens;
    }

    public void setSens()
    {
        CinemachineFreeLook cinemachineFreeLook = FindObjectOfType<CinemachineFreeLook>();
        cinemachineFreeLook.m_XAxis.m_MaxSpeed = xSensitivity;
        cinemachineFreeLook.m_YAxis.m_MaxSpeed = ySensitivity;
    }
}
