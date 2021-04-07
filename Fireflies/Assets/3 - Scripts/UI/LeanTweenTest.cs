using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeanTweenTest : MonoBehaviour
{
    public GameObject shop_bg, text_bg, Title, Back;
    public GameObject skill_1, skill_2, skill_3;
    public GameObject skill_1_bg, skill_2_bg, skill_3_bg;
    public LeanTweenType leanTweenType;

    void Start()
    {
        LeanTween.scale(shop_bg.GetComponent<RectTransform>(), new Vector3(0,0,1), 0f);
        LeanTween.scale(Back.GetComponent<RectTransform>(), new Vector3(0,0,1), 0f); 
        LeanTween.scale(Title.GetComponent<RectTransform>(), new Vector3(0,0,1), 0f);
        LeanTween.scale(text_bg.GetComponent<RectTransform>(), new Vector3(0,0,1), 0f);
        LeanTween.scale(skill_1.GetComponent<RectTransform>(), new Vector3(0,0,1), 0f);
        LeanTween.scale(skill_1_bg.GetComponent<RectTransform>(), new Vector3(0,0,1), 0f);
        LeanTween.scale(skill_2.GetComponent<RectTransform>(), new Vector3(0,0,1), 0f);
        LeanTween.scale(skill_2_bg.GetComponent<RectTransform>(), new Vector3(0,0,1), 0f);
        LeanTween.scale(skill_3.GetComponent<RectTransform>(), new Vector3(0,0,1), 0f);
        LeanTween.scale(skill_3_bg.GetComponent<RectTransform>(), new Vector3(0,0,1), 0f);


        LeanTween.scale(shop_bg.GetComponent<RectTransform>(), new Vector3(1,1,1), 0.5f).setEase(leanTweenType).setIgnoreTimeScale(true);
        LeanTween.scale(Back.GetComponent<RectTransform>(), new Vector3(1,1,1), 0.5f).setEase(leanTweenType).setIgnoreTimeScale(true);
        LeanTween.scale(Title.GetComponent<RectTransform>(), new Vector3(1,1,1), 0.5f).setDelay(0.1f).setEase(leanTweenType).setIgnoreTimeScale(true);

        LeanTween.scale(text_bg.GetComponent<RectTransform>(), new Vector3(1,1,1), 0.5f).setDelay(0.2f).setEase(leanTweenType).setIgnoreTimeScale(true);

        LeanTween.scale(skill_1.GetComponent<RectTransform>(), new Vector3(1,1,1), 0.5f).setDelay(0.3f).setEase(leanTweenType).setIgnoreTimeScale(true);
        LeanTween.scale(skill_1_bg.GetComponent<RectTransform>(), new Vector3(1,1,1), 0.5f).setDelay(0.2f).setEase(leanTweenType).setIgnoreTimeScale(true);
        LeanTween.scale(skill_2.GetComponent<RectTransform>(), new Vector3(1,1,1), 0.5f).setDelay(0.35f).setEase(leanTweenType).setIgnoreTimeScale(true);
        LeanTween.scale(skill_2_bg.GetComponent<RectTransform>(), new Vector3(1,1,1), 0.5f).setDelay(0.25f).setEase(leanTweenType).setIgnoreTimeScale(true);
        LeanTween.scale(skill_3.GetComponent<RectTransform>(), new Vector3(1,1,1), 0.5f).setDelay(0.4f).setEase(leanTweenType).setIgnoreTimeScale(true);
        LeanTween.scale(skill_3_bg.GetComponent<RectTransform>(), new Vector3(1,1,1), 0.5f).setDelay(0.3f).setEase(leanTweenType).setIgnoreTimeScale(true);
    }

    public void skillInfo(bool isOn)
    {
        switch (isOn)
        {
            case true:
                break;
        }
    }

}
