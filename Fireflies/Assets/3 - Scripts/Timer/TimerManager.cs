using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerManager : MonoBehaviour
{
    public static TimerManager instance { get; private set; }

    public float time = 0;

    public bool isTimerOn = false;
    private HUDManager hudUI;

    private void Awake()
    {
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

    void Start(){
        //guarda referência para singleton de HUD Manager e atualiza com tempo zerado
        hudUI = HUDManager.instance;
        if(hudUI != null){
            hudUI.UpdateTimer(0f);
            hudUI.ChangeTimerState(isTimerOn);
        }
        else {
            Debug.Log("Não há nenhum objeto com HUD Manager em cena");
        }
    }

    //ligar ou retomar o timer
    public void startTimer()
    {
        isTimerOn = true;
        hudUI.ChangeTimerState(isTimerOn);
    }

    //parar o timer
    public void stopTimer()
    {
        isTimerOn = false;
        hudUI.ChangeTimerState(isTimerOn);
    }

    //resetar timer a 0, podendo fazer ele comecar ligado ou nao
    public void resetTimer(bool isOn)
    {
        time = 0;
        isTimerOn = isOn;
        hudUI.ChangeTimerState(isTimerOn);
    }

    public void Update()
    {
        if (isTimerOn)
        {
            time += Time.unscaledDeltaTime;
            hudUI.UpdateTimer(time);
        }
    }
}
