using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class healthBar : MonoBehaviour
{
    public health health;
    public Slider slider;
    // Start is called before the first frame update
    void Start()
    {
        health = GameObject.FindGameObjectWithTag("Player").GetComponent<health>();
        slider = GetComponent<Slider>();
        slider.maxValue = health.maximHealth;
        slider.value = health.maximHealth;

    }

    // Update is called once per frame
    // https://weeklyhow.com/how-to-make-a-health-bar-in-unity/
    public void updateHealth(int val)
    {
        slider.value = val;
    }
}
