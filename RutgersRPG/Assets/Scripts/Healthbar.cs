using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    public Slider slider;
    public Gradient gradient;
    public Image fill;
    public Text HPCounter;

    public void SetMaxHealth(int value)
    {
        slider.value = slider.maxValue = value;
        HPCounter.text = "" + slider.value;
        fill.color = gradient.Evaluate(1f);
    }

    public void UpdateHealth(int value)
    {
        slider.value = value;
        HPCounter.text = "" + slider.value;
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }
}
