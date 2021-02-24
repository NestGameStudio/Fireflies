using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerManager : MonoBehaviour
{
    public static TimerManager instance { get; private set; }

    public float time = 0;

    public bool isTimerOn = false;

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

    //ligar ou retomar o timer
    public void startTimer()
    {
        isTimerOn = true;
    }

    //parar o timer
    public void stopTimer()
    {
        isTimerOn = false;
    }

    //resetar timer a 0, podendo fazer ele comecar ligado ou nao
    public void resetTimer(bool isOn)
    {
        time = 0;
        if (isOn) {

            isTimerOn = true;

        }
        else
        {
            isTimerOn = false;
        }
    }

    public void FixedUpdate()
    {

        //assign do valor do tempo ao elemento de ui, formatacao com a variavel de tempo
        System.TimeSpan timeFormat = System.TimeSpan.FromSeconds(time);

        string timeText = string.Format("{0:D2}:{1:D2}:{2:D2}", timeFormat.Hours, timeFormat.Minutes, timeFormat.Seconds);

        HUDManager.instance.timerCounterText.text = timeText;

        if (isTimerOn)
        {
            time += Time.deltaTime;
        }
    }
}
