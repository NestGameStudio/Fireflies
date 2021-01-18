using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TimerUI : MonoBehaviour
{
    public TextMeshProUGUI text;
    private Slider slider;

    public void SetupUI(float value)
    {
        slider = GetComponent<Slider>();
        if(text == null) text = GetComponentInChildren<TextMeshProUGUI>();
        slider.maxValue = value;
        UpdateUI(value);
    }

    public void UpdateUI(float val)
    {
        slider.value = val;
        text.text = Mathf.Ceil(val).ToString();
    }
}
