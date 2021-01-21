using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerController : MonoBehaviour
{
    [SerializeField]
    public GameObject timerObj;
    private TimerUI timerUI;
    private float time;
    public float timeStart = 30f;
    public bool isPaused = true;
    public int state; // controlador da máquina de estados
    public bool isOver = false;    // true quando timer atinge ZERO no método AddTimer

    public void Start(){
        if(timerObj != null) {
            timerUI = timerObj.GetComponent<TimerUI>();
            timerUI.SetupUI(timeStart);
            ResetTimer();
        }
        else {
            PauseTimer(true);
            Debug.Log("Timer UI Object is not referenced in TimeController");
        }
    }
    void Update(){
        if(!isPaused){
            AddTimer(-Time.unscaledDeltaTime); //UnscaledDeltaTime o torna independente de variações em Time Scale
        }
    }

    //Pause/Unpause timer
    public void PauseTimer(bool state){
        isPaused = state;
    }

    //Adiciona tempo ao timer, identifica se tempo esgotou
    public void AddTimer(float value){
        time = Mathf.Clamp(time + value, 0, timeStart);
        timerUI.UpdateUI(time);

        if(time <= 0 && !isOver){
            TimerOver();
        }
    }

    public void TimerOver(){
        PauseTimer(true); //para o tempo
        isOver = true; 
    }

    public void ResetTimer(){
        time = timeStart;
        timerUI.UpdateUI(time);
        isOver = false;
        PauseTimer(false);
    }
}
