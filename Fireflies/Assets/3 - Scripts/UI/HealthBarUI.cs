using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour
{
    //em quantas vezes visual da barra é multiplicada para corresponder vida
    //Ex: Com 'sizeToHealthRate = 2', vida máxima 100 indica tamanho de UI 200;
    public float sizeToHealthRate = 2f;
    private float sizeHeight;
    public float dangerLimitValue = 25f;
    public Color dangerLimitColor;
    public float alertLimitValue = 50f;
    public Color alertLimitColor;
    private Color defaultColor;
    private RectTransform rect;
    private Slider slider;
    private RectTransform fillRect;
    private Image fillImage;
    private int state = 0; //State machine value

    void Awake()
    {
        rect = GetComponent<RectTransform>();
        sizeHeight = rect.sizeDelta.y;
        slider = GetComponent<Slider>();
        fillRect = slider.fillRect;
        fillImage = fillRect.GetComponent<Image>();
        defaultColor = fillImage.color;
    }

    public void SetupUI(float value){
        slider.maxValue = value;
        SetHealth(value);
    }

    public void SetHealth(float value){
        slider.value = value;

        //State machine controller
        if (value > alertLimitValue){
            //default zone
            if(state != 0) ChangeHealthState(0);            
        } else if (value > dangerLimitValue){
            //alert zone
            if(state != 1) ChangeHealthState(1);
        } else{
            //danger zone
            if(state != 2) ChangeHealthState(2);
        }

    }

    public void SetMaxHealth(float value){
        slider.maxValue = value;
        rect.sizeDelta = new Vector2 (slider.maxValue*sizeToHealthRate, sizeHeight);
    }

    public void ChangeHealthState(int s){
        state = s;
        switch (s){
            case 2:
                StateDanger();
                break;
            case 1:
                StateAlert();
                break;
            default:
                StateDefault();
                break;

        }
    }

    public void StateDanger(){
        fillImage.color = dangerLimitColor;
    }
    public void StateAlert(){
        fillImage.color = alertLimitColor;
    }
    public void StateDefault(){
        fillImage.color = defaultColor;
    }

}
