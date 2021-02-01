using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TimerUI : MonoBehaviour
{
    public TextMeshProUGUI text;
    [HideInInspector] public Slider slider;
    public Color startColor;
    public RectTransform feedbackRect;
    private Image feedbackImage;

    public void Start(){
        slider = GetComponent<Slider>();
        if(text == null) text = GetComponentInChildren<TextMeshProUGUI>();
        if(feedbackRect == null) feedbackRect = transform.Find("Feedback").GetComponent<RectTransform>();
        feedbackImage = feedbackRect.gameObject.GetComponent<Image>();
    }
    public void SetupUI(float value)
    {
        slider.maxValue = value;
        UpdateUI(value);
    }

    public void UpdateUI(float val)
    {
        slider.value = val;
        text.text = Mathf.Ceil(val).ToString();
    }

    public void StateDefault(){
        Debug.Log("State: Default");

        feedbackRect.LeanCancel();
        feedbackRect.sizeDelta = new Vector2(50f,50f);

        feedbackImage.rectTransform.LeanCancel();
    }
    public void StateDanger(){
        Debug.Log("State: Danger");

        feedbackRect.sizeDelta = new Vector2(150f,150f);
        feedbackImage.color = new Color (1f,1f,1f,0f);

        //Começa animações loop
        feedbackRect.LeanSize(new Vector2(90f, 90f), 0.5f).setLoopClamp().setEase(LeanTweenType.easeInExpo).setIgnoreTimeScale(true);
        LeanTween.color(feedbackImage.rectTransform,new Color (1f,1f,1f,0.5f), 0.5f).setLoopClamp().setIgnoreTimeScale(true);
    }

    public void StateOver(){
        Debug.Log("State: Over");
    }
}
