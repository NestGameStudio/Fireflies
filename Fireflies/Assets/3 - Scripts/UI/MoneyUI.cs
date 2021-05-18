using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MoneyUI : MonoBehaviour
{
    public TextMeshProUGUI moneyText;
    public Color lowMoneyColor = Color.red;
    public Image feedback;

    public void SetupUI(float value){
        SetMoney(value);
    }
    public void SetMoney(float value){
        moneyText.text = value.ToString();
    }

    public void LowMoney(){
        feedback.color = lowMoneyColor;
        LeanTween.alpha(feedback.gameObject, 0f, 0.5f);
    }

}
