using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TimerUI : MonoBehaviour
{
    public TextMeshProUGUI text;
    public Slider slider;
    public Color defaultColor = Color.blue;
    public Color dangerColor = Color.red;
    public Image fillImage;
    public RectTransform feedbackRect;
    public Image feedbackImage;

    public void Start(){
        //slider = GetComponent<Slider>();
        //fillImage = slider.fillRect.gameObject.GetComponent<Image>();
        if(text == null) text = GetComponentInChildren<TextMeshProUGUI>();
        if(feedbackRect == null) feedbackRect = transform.Find("Feedback").GetComponent<RectTransform>();
        //feedbackImage = feedbackRect.gameObject.GetComponent<Image>();
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

        //Debug.Log("State: Default");

        feedbackRect.LeanCancel();
        feedbackRect.sizeDelta = new Vector2(50f,50f);

        feedbackImage.rectTransform.LeanCancel();

        fillImage.color = defaultColor;
        
    }
    public void StateDanger(){
        //Debug.Log("State: Danger");

        feedbackRect.sizeDelta = new Vector2(170f,170f);
        feedbackImage.color = new Color (1f,1f,1f,0f);

        //Começa animações loop
        feedbackRect.LeanSize(new Vector2(90f, 90f), 0.5f).setLoopClamp().setEase(LeanTweenType.easeInExpo).setIgnoreTimeScale(true);
        LeanTween.color(feedbackImage.rectTransform,new Color (1f,1f,1f,0.5f), 0.5f).setLoopClamp().setIgnoreTimeScale(true);

        fillImage.color = dangerColor;
    }

    public void StateOver(){
        //Debug.Log("State: Over");

        Transform feedbackParent = feedbackRect.transform;

        RectTransform feedbackBoom = Instantiate(feedbackRect.gameObject, feedbackParent.position, Quaternion.identity, feedbackParent).GetComponent<RectTransform>();

        //feedbackBoom.position = new Vector3 (0f,0f,0f);
        feedbackBoom.sizeDelta = new Vector2(90f,90f);
        feedbackBoom.GetComponent<Image>().color = new Color (dangerColor.r,dangerColor.g,dangerColor.b,1f);

        feedbackBoom.LeanSize(new Vector2(300f,300f), 0.8f).setEase(LeanTweenType.easeOutExpo).setIgnoreTimeScale(true).setOnComplete(() => destroyObj(feedbackBoom.gameObject));
        LeanTween.color(feedbackBoom,new Color (dangerColor.r,dangerColor.g,dangerColor.b,0f), 0.8f).setIgnoreTimeScale(true);
    }

    private void destroyObj (GameObject obj){
        Destroy(obj);
    }
}
