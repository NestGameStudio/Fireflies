using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class uiShopAnimation : MonoBehaviour
{
    public GameObject[] Skills;
    public GameObject[] UI;

    public LeanTweenType leanTweenType;
    public LeanTweenType NoMoneyleanTweenType;

    public void Animate()
    {
        for (int i = 0; i < Skills.Length; i++)
        {
            LeanTween.scale(Skills[i].GetComponent<RectTransform>(), new Vector3(0,0,1), 0f);
            LeanTween.scale(Skills[i].GetComponent<RectTransform>(), new Vector3(1,1,1), 0.5f).setDelay(i/10f + 0.3f).setEase(leanTweenType).setIgnoreTimeScale(true);
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
        LeanTween.alpha(obj.transform.GetChild(2).gameObject.GetComponent<RectTransform>(), 0.5f, 0.5f).setIgnoreTimeScale(true); 
        obj.transform.GetChild(0).GetChild(1).gameObject.SetActive(true); 
        obj.transform.GetChild(1).gameObject.SetActive(false);
    }

    public void noMoney(GameObject obj)
    {
        LeanTween.scale(obj.transform.GetChild(1).gameObject.GetComponent<RectTransform>(), new Vector3(1.3f,1.3f,1), 0.5f).setEase(NoMoneyleanTweenType).setIgnoreTimeScale(true);
        LeanTween.scale(obj.transform.GetChild(1).gameObject.GetComponent<RectTransform>(), new Vector3(1,1,1), 0.5f).setDelay(0.5f).setIgnoreTimeScale(true);
        AnimateText(obj.transform.GetChild(1).GetChild(0).gameObject);
    }

    private void AnimateText(GameObject obj)
    {
        TextMeshProUGUI textmeshPro = obj.GetComponent<TextMeshProUGUI>();
        textmeshPro.color = new Color32(255, 0, 0, 255);
        StartCoroutine(ResetTextColor(obj));
    }   

    private IEnumerator ResetTextColor(GameObject obj)
    {
        yield return new WaitForSecondsRealtime(0.5f);
        TextMeshProUGUI textmeshPro = obj.GetComponent<TextMeshProUGUI>();
        textmeshPro.color = new Color32(255, 255, 255, 255);
    }
}
