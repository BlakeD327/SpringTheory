using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;
    
    //this creates a max for starting health amount
    public void SetMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;
    }

    //this updates the health with damage
    public void SetHealth(int health)
    {
        slider.value = health;
    }
}
