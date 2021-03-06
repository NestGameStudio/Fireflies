﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HUDManager : MonoBehaviour
{
    public static HUDManager instance { get; private set; }

    //Timer variables
    [Header("Timer")]
    [Space(0.3f)]
    public TextMeshProUGUI timerCounterText;
    public Color timerActiveColor = Color.white;
    public Color timerPausedColor = Color.gray;

    //Health variables
    [Header("Health")]
    [Space(0.3f)]
    public HealthBarUI healthUI;
    //Money variables
    [Header("Money")]
    [Space(0.3f)]
    public MoneyUI moneyUI;

    // Estattisticas da Tentativa
    [Header("Stats")]
    [Space(0.3f)]
    public GameObject GameOverStats;
    public GameObject EndStats;
    public GameObject SkillButtonPrefab;
    
    public TextMeshProUGUI AttemptText;
    public TextMeshProUGUI JumpText;
    public TextMeshProUGUI EnemiesText;
    public TextMeshProUGUI MoneyText;
    public TextMeshProUGUI TimeText;
    public GameObject SkillList;

    public TextMeshProUGUI AttemptTextEnd;
    public TextMeshProUGUI JumpTextEnd;
    public TextMeshProUGUI EnemiesTextEnd;
    public TextMeshProUGUI MoneyTextEnd;
    public TextMeshProUGUI TimeTextEnd;
    public GameObject SkillListEnd;


    private void Awake(){
        // Singleton
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
    }

    // Update is called once per frame
    public void UpdateTimer(float time){
        //assign do valor do tempo ao elemento de ui, formatacao com a variavel de tempo
        System.TimeSpan timeFormat = System.TimeSpan.FromSeconds(time);
        string timeText = string.Format("{0:D2}:{1:D2}:{2:D2}", timeFormat.Hours, timeFormat.Minutes, timeFormat.Seconds);

        timerCounterText.text = timeText;
    }

    public void ChangeTimerState(bool timerActive){
        if(timerActive){
            timerCounterText.color = timerActiveColor;
        }
        else{
            timerCounterText.color = timerPausedColor;
        }
    }

    public void QuitGame() {
        Application.Quit();
    }
}
