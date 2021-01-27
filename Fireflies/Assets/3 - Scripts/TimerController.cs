using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerController : MonoBehaviour
{
    public static TimerController Instance { get; private set;  }

    [SerializeField]

    public GameObject timerObj;
    private TimerUI timerUI;
    private float time;
    public float timeStart = 30f;
    [HideInInspector] public bool isPaused = true;
    public int state; // controlador da máquina de estados
    public bool isOver = false;    // true quando timer atinge ZERO no método AddTimer
    [HideInInspector] public bool hasStarted = false;

    private void Awake()
    {
        // Singleton
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    public void Start() {
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
    void Update() {
        if(!isPaused){
            AddTimer(-Time.unscaledDeltaTime); //UnscaledDeltaTime o torna independente de variações em Time Scale
        }
    }

    // ------------- Game Timer ------------------

    //Pause/Unpause timer
    public void PauseTimer(bool state) {
        isPaused = state;
    }

    //Adiciona tempo ao timer, identifica se tempo esgotou
    public void AddTimer(float value) {
        time = Mathf.Clamp(time + value, 0, timeStart);
        timerUI.UpdateUI(time);

        if(time <= 0 && !isOver){
            TimerOver();
        }
    }

    public void TimerOver() {
        PauseTimer(true); //para o tempo
        isOver = true; 

        //reposicionar o player na fase
        if(Respawn.instance != null)
        {
            Respawn.instance.RepositionPlayer();
        }
    }

    public void ResetTimer() {
        time = timeStart;
        timerUI.UpdateUI(time);
        isOver = false;
    }

    // ------------- Jump Timer ------------------

}

