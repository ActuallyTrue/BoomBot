using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    public GameObject menu;
    public Slider xSensitivitySlider;
    public Slider ySensitivitySlider;

    public CinemachineFreeLook cinemachineFreeLook;
    // Start is called before the first frame update
    void Start()
    {
        cinemachineFreeLook = FindObjectOfType<CinemachineFreeLook>();
    }

    public void turnOnOptionsMenu()
    {
        menu.SetActive(true);
        xSensitivitySlider.value = cinemachineFreeLook.m_XAxis.m_MaxSpeed;
        ySensitivitySlider.value = cinemachineFreeLook.m_YAxis.m_MaxSpeed;
    }

    public void turnOffOptionsMenu()
    {
        menu.SetActive(false);
    }

    public void setXSensitivity(float value)
    {
       cinemachineFreeLook.m_XAxis.m_MaxSpeed = value;
    }

    public void setYSensitivity(float value)
    {
        cinemachineFreeLook.m_YAxis.m_MaxSpeed = value;
    }
}