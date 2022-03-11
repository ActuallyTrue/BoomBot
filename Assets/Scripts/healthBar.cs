using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class healthBar : MonoBehaviour
{
    public health health;
    public Slider slider;
    public Gradient gradient;
    public Image img;
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
    //https://opengameart.org/content/heart-2
    public void updateHealth(int val)
    {
        img.color = gradient.Evaluate(1f);
        slider.value = val;
    }
}
