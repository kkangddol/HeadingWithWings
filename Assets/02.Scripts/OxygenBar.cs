using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OxygenBar : MonoBehaviour
{
    public Slider slider;
    public Gradient gradient;
    public Image fill;

    public void OnValueChanged()
    {
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }

    public void SetMaxOxygen(int oxygen)
    {
        slider.maxValue = oxygen;
        slider.value = oxygen;
    }

    public void SetOxygen(int oxygen)
    {
        slider.value = oxygen;

        fill.color = gradient.Evaluate(slider.normalizedValue);
    }
}
