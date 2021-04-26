
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

    public TextMeshProUGUI DescriptionText;

    public void OnPointerEnter(PointerEventData eventData)
    {
        DescriptionText.text = Description;  
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        DescriptionText.text = "";
    }

    public void AddDescription(string desc) {
        Description = desc;
    }
}