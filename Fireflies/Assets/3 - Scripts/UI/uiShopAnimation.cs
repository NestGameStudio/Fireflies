using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class uiShopAnimation : MonoBehaviour
{
    public GameObject[] Skills;
    public GameObject[] SkillsBG;
    public GameObject[] UI;

    public LeanTweenType leanTweenType;

    public void Animate()
    {
        for (int i = 0; i < Skills.Length; i++)
        {
            LeanTween.scale(Skills[i].GetComponent<RectTransform>(), new Vector3(0,0,1), 0f);
            LeanTween.scale(Skills[i].GetComponent<RectTransform>(), new Vector3(1,1,1), 0.5f).setDelay(i/10f + 0.3f).setEase(leanTweenType).setIgnoreTimeScale(true);
        }
        for (int i = 0; i < SkillsBG.Length; i++)
        {
            LeanTween.scale(SkillsBG[i].GetComponent<RectTransform>(), new Vector3(0,0,1), 0f);
            LeanTween.scale(SkillsBG[i].GetComponent<RectTransform>(), new Vector3(1,1,1), 0.5f).setDelay(i/10f + 0.2f).setEase(leanTweenType).setIgnoreTimeScale(true);
        }
        for (int i = 0; i < UI.Length; i++)
        {
            LeanTween.scale(UI[i].GetComponent<RectTransform>(), new Vector3(0,0,1), 0f);
            LeanTween.scale(UI[i].GetComponent<RectTransform>(), new Vector3(1,1,1), 0.5f).setEase(leanTweenType).setIgnoreTimeScale(true);
        } 
    }

    public void skillSelect(GameObject obj)
    {
        LeanTween.scale(obj.GetComponent<RectTransform>(), new Vector3(-1,1,1), 0.5f).setEase(leanTweenType).setIgnoreTimeScale(true);
        obj.transform.GetChild(0).GetChild(1).gameObject.SetActive(true); 
        obj.transform.GetChild(1).gameObject.SetActive(false); 
    }

    public void fadeBG(GameObject bg)
    {
        LeanTween.alpha(bg.GetComponent<RectTransform>(), 0f, 0.5f).setIgnoreTimeScale(true);
    }

}
