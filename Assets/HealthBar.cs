using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Initialises healthbar to be equal to player health

public class HealthBar : MonoBehaviour
{

    public Slider slider;

    public void SetMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;
    }

    public void Health(int health)
    {
        slider.value = health;
    }

}
