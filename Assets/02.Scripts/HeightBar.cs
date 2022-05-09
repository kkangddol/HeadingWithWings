using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeightBar : MonoBehaviour
{
    public Slider slider;
    public Gradient gradient;
    public Image fill;
    public void SetHeight(float height)
    {
        slider.value = height;

        fill.color = gradient.Evaluate(slider.normalizedValue);
    }
}
