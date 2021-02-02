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
    [Tooltip("Limite para considerar 'tempo acabando' (%)")]
    [Range(0.0f,1.0f)]
    public float timeDangerZone = 0.3f;
    [HideInInspector] public bool isPaused = true;
    private int state; // controlador da máquina de estados
    private bool isOver = false;    // true quando timer atinge ZERO no método AddTimer
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

        // Danger State
        if(time <= timeStart * timeDangerZone) {if(state != 1) ChangeState(1);}
        else {if(state != 0) ChangeState(0);}
    }

    public void TimerOver() {
        isOver = true; 
        ChangeState(2); //feedback de tempo esgotado

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
        ChangeState(0);
    }

    public void ChangeState(int s){
        state = s;
        switch (s){
            case 2:
                timerUI.StateOver();
                break;
            case 1:
                timerUI.StateDanger();
                break;
            default:
                timerUI.StateDefault();
                break;

        }
    }

    // ------------- Jump Timer ------------------

}

