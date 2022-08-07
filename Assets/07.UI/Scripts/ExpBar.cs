using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExpBar : MonoBehaviour
{
    public Slider slider;
    public Gradient gradient;
    public Image fill;

    private void Awake()
    {
        GameManager.Instance.ExpBar = this;
    }

    public void SetExp(float exp)
    {
        slider.value = exp;

        fill.color = gradient.Evaluate(slider.normalizedValue);
    }
    public void SetMaxValue(int value)
    {
        slider.maxValue = value;
    }
}
