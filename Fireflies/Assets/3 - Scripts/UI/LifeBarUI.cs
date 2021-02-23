using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeBarUI : MonoBehaviour
{
    public float sizeToLifeRate = 2f;
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

    void Start()
    {
        rect = GetComponent<RectTransform>();
        sizeHeight = rect.sizeDelta.y;
        slider = GetComponent<Slider>();
        fillRect = slider.fillRect;
        fillImage = fillRect.GetComponent<Image>();
        defaultColor = fillImage.color;

        //debug
        //SetupUI(100f);
        //UpgradeMaxLife(50f);
    }

    void Update(){
        //debug
        //UpdateUI(slider.value - 0.1f);
    }

    public void SetupUI(float value){
        slider.maxValue = value;
        UpdateUI(value);
    }

    public void UpdateUI(float value){
        slider.value = value;

        //State machine controller
        if (value > alertLimitValue){
            //default zone
            if(state != 0) ChangeState(0);            
        } else if (value > dangerLimitValue){
            //alert zone
            if(state != 1) ChangeState(1);
        } else{
            //danger zone
            if(state != 2) ChangeState(2);
        }

    }

    public void UpgradeMaxLife(float value){
        UpgradeMaxLife(value,true);
    }
    public void UpgradeMaxLife(float value, bool increaseLife){
        slider.maxValue += value;
        rect.sizeDelta = new Vector2 (slider.maxValue*sizeToLifeRate, sizeHeight);
        if(increaseLife){
            slider.value += value;
        }
    }

    public void ChangeState(int s){
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
