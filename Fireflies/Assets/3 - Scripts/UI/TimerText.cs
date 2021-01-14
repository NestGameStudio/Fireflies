using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TimerText : MonoBehaviour
{
    private TextMeshProUGUI text;
    private Slider slider;
    private float value;

    void Start()
    {
        slider = GetComponentInParent<Slider>();
        value = slider.value;

        text = GetComponent<TextMeshProUGUI>();
        text.text = Mathf.Floor(value).ToString();
    }

    public void UpdateText()
    {
        value = slider.value;
        text.text = Mathf.Floor(value).ToString();
    }
}
