
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SkillDescription : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [TextArea]
    private string Description;
    private string Title;

    public TextMeshProUGUI DescriptionText;
    public TextMeshProUGUI TitleText;

    public void OnPointerEnter(PointerEventData eventData)
    {
        DescriptionText.text = Description;
        TitleText.text = Title;  
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        DescriptionText.text = "";
        TitleText.text = "";
    }

    public void AddDescription(string desc) {
        Description = desc;
    }

    public void AddTitle(string title) {
        Title = title;
    }
}