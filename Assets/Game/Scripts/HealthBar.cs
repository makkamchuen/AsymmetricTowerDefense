using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;
    public Gradient gradient;
    public Image fill;


    public void SetMaxHealth(float maxHealth)
    {
        this.slider.maxValue = maxHealth;
        gradient.Evaluate(1f);
    }

    public void SetHealth(float health)
    {
        this.slider.value = health;
        SetColor();
    }

    public void IncrementHealth()
    {
        if (this.slider.value < this.slider.maxValue)
        {
            this.slider.value++;
            SetColor();
        }
    }
    
    public void DecrementHealth()
    {
        if (this.slider.value > this.slider.minValue)
        {
            this.slider.value--;
            SetColor();
        }
    }

    private void SetColor()
    {
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }


}
