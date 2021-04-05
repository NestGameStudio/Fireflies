using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeanTweenTest : MonoBehaviour
{
    public GameObject shop_bg, text_bg, Title, Back, Skills, Skills_bg;
    public LeanTweenType leanTweenType;

    void Start()
    {
        LeanTween.scale(shop_bg.GetComponent<RectTransform>(), new Vector3(0,0,1), 0f);
        LeanTween.scale(text_bg.GetComponent<RectTransform>(), new Vector3(0,0,1), 0f);
        LeanTween.scale(Title.GetComponent<RectTransform>(), new Vector3(0,0,1), 0f);
        LeanTween.scale(Back.GetComponent<RectTransform>(), new Vector3(0,0,1), 0f);
        LeanTween.scale(Skills.GetComponent<RectTransform>(), new Vector3(0,0,1), 0f);
        LeanTween.scale(Skills_bg.GetComponent<RectTransform>(), new Vector3(0,0,1), 0f);

        LeanTween.scale(shop_bg.GetComponent<RectTransform>(), new Vector3(1,1,1), 0.5f).setEase(leanTweenType).setIgnoreTimeScale(true);
        LeanTween.scale(text_bg.GetComponent<RectTransform>(), new Vector3(1,1,1), 0.5f).setEase(leanTweenType).setIgnoreTimeScale(true);
        LeanTween.scale(Title.GetComponent<RectTransform>(), new Vector3(1,1,1), 0.5f).setEase(leanTweenType).setIgnoreTimeScale(true);
        LeanTween.scale(Back.GetComponent<RectTransform>(), new Vector3(1,1,1), 0.5f).setEase(leanTweenType).setIgnoreTimeScale(true);
        LeanTween.scale(Skills.GetComponent<RectTransform>(), new Vector3(1,1,1), 0.5f).setEase(leanTweenType).setIgnoreTimeScale(true);
        LeanTween.scale(Skills_bg.GetComponent<RectTransform>(), new Vector3(1,1,1), 0.5f).setEase(leanTweenType).setIgnoreTimeScale(true);
    }

}
