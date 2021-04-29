﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.InputSystem;
//using UnityEngine.InputSystem.Users;

public class SlingshotController : MonoBehaviour {

    // ------------------- Movimentação -------------------------
    [Header("Controle da Movimentação")]
    [Space(0.3f)]

    public SlingshotVisual slingshotVisual;

    // ------------- Variáveis privadas ------------------

    private JumpRecovery JumpControl; 

    // Variáveis da movimentação
    private bool isOnSlowMotion = false;    // Entrou no slowMotion com um clique

    // Direção atualizada conforme pega os valores do analógico/mouse
    [HideInInspector] public Vector2 direction;
    // o centro da linha
    private Vector2 lineCenterPos;

    public Vector2 impulseVector = Vector2.zero;

    public AudioSource jumpAudio;
    private float startPitch;
    private AudioLowPassFilter audioMusic; 
    private bool bufferingJump;

    [HideInInspector]
    public Vector2 impulse;

    public Trajectory trajectory;

    private bool HoldingJump = false;
    private float HoldingTime = 0;
    // ------------- Setup e checagens ------------------
    private void Start() {
        JumpControl = this.GetComponent<JumpRecovery>();

        startPitch = jumpAudio.pitch;
        

        if(audioMusic == null && GameObject.FindGameObjectWithTag("Music") != null)
        {
            audioMusic = GameObject.FindGameObjectWithTag("Music").GetComponent<AudioLowPassFilter>();
            audioMusic.enabled = false;
        }

        if(trajectory == null){
            trajectory = transform.parent.parent.Find("Slingshot Visuals").GetComponent<Trajectory>();
        }
    }

    // Verifica se usa recursos de seguir o player -> atualiza a posição das coisas visuais
    private void Update() {

        // Se está apertando o slowMotion, ajeita a posição do slingshot de acordo com a movimentação do analógico/mouse
        if (isOnSlowMotion) {

            if (Setup.Instance.ClickReferenceInPlayer & Setup.Instance.ReferenceFollowPlayer || ControlManager.Instance.getCurrentControlScheme() == ControlScheme.Gamepad) {
                CenterReference();
            }

            slingshotVisual.SetFinalLinePosition(direction);
            adjustImpulse();


            if (Setup.Instance.ShowArc){
                trajectory.SimulateArc();
            }
        }
    }

    private void FixedUpdate() {
        if(HoldingJump) {
            Debug.Log("Alo");
            HoldingTime += Time.fixedDeltaTime;
            if(HoldingTime >= Setup.Instance.PlayerValue.rHoldLimit) {
                Debug.Log("Tchau");
                ExitSlowMotionMode();
                HoldingTime = 0f;
            }
        }
    }

    public void EnterSlowMotionMode() {

        if (JumpControl.CanJump()) {

            Time.timeScale = Setup.Instance.PlayerValue.TimeSlow;
            Time.fixedDeltaTime = Setup.Instance.PlayerValue.TimeSlow * Time.deltaTime;    // faz com que o slowmotion não fique travado

            HoldingJump = true;

            CenterReference();
            slingshotVisual.SlingshotVisualSetup(lineCenterPos);

            isOnSlowMotion = true;

            if (audioMusic != null)
            {
                audioMusic.enabled = true;
            }

            if (Setup.Instance.ShowArc){
                trajectory.enterArc();
            }
        }
        // Apertou o botão sem poder pular -> 'bufferiza' o pulo
        else{
            bufferingJump=true;
            StartCoroutine(BufferedJump());
        }
    }

    public void ExitSlowMotionMode() {

        // precisa do isOnSlowMotion para que não ocorra o soft lock
        if (JumpControl.CanJump() && isOnSlowMotion)
        {
            HoldingJump = false;
            Time.timeScale = 1f;
            slingshotVisual.DisableSlingshotVisuals();
            slingshotVisual.SetFinalLinePosition(new Vector2(Screen.width / 2, Screen.height / 2));

            //evita pulos de força extremamente baixa (game breaking)
            if (impulseVector.magnitude > slingshotVisual.GetMinLine())
            {
                Jump();
                JumpControl.useJumpCharge();
            }

            isOnSlowMotion = false;
            if (audioMusic != null)
            {
                audioMusic.enabled = false;
            }

            trajectory.exitArc();
        }
        // Soltou o botão sem estar no slowmotion
        else{
            bufferingJump=false;
        }
    }

    // -> Buffer
    private IEnumerator BufferedJump()
    {
        yield return new WaitUntil(()=>{
            return (JumpControl.CanJump() || !bufferingJump);
            });
        if(bufferingJump){
            EnterSlowMotionMode();
        }
    }

    // -> Slingshot

    // Pega a referência do centro da rotação da linha do slingshot conforme o padrão de controles
    private void CenterReference() {

        if (ControlManager.Instance.getCurrentControlScheme() == ControlScheme.Gamepad) {
            lineCenterPos = this.gameObject.transform.position;
        } else {

            if (Setup.Instance.setReferenceInCenter) {
                lineCenterPos = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width / 2, Screen.height / 2));
            } else if (Setup.Instance.ClickReferenceInPlayer) {
                lineCenterPos = this.gameObject.transform.position;
            } else if (!Setup.Instance.ClickReferenceInPlayer) {
                lineCenterPos = Camera.main.ScreenToWorldPoint(direction);
            }
        }
    }
    
    // Ajusta a posição da linha do slingshot quando movimenta o analógico/mouse
    private void adjustImpulse() {

        // Posição atual do Mouse ou Analógico
        Vector2 lineFinalPos = Camera.main.ScreenToWorldPoint(direction);

        if ((lineCenterPos - lineFinalPos).magnitude <= slingshotVisual.GetMaxLine()) {

            if (Setup.Instance.InvertedSlingshot) {
                impulseVector = lineCenterPos - lineFinalPos;
            } else {
                impulseVector = lineFinalPos - lineCenterPos;
            }

        // Passou da linha limite
        } else {

            if (Setup.Instance.InvertedSlingshot) {
                impulseVector = (lineCenterPos - lineFinalPos).normalized * slingshotVisual.GetMaxLine();
            } else {
                impulseVector = (lineFinalPos - lineCenterPos).normalized * slingshotVisual.GetMaxLine();
            }
        }        
    }

    private void Jump() {

        // Zera a velocidade do player antes de dar um novo impulso para não ter soma de vetores
        Rigidbody2D rb = this.GetComponentInParent<Rigidbody2D>();
        rb.velocity = Vector3.zero;

        // Calcula o impulso
        impulse = new Vector2(impulseVector.x, impulseVector.y) * Setup.Instance.PlayerValue.rImpulseForce;
        rb.AddForce(impulse, ForceMode2D.Impulse);
        Debug.Log("RBvelocity = " + rb.velocity.magnitude + " / " + "RBimpulse = " + impulse.magnitude);
        jumpAudioEvent();

        impulseVector = Vector2.zero;
        SaveSystem.instance.Stats.JumpCount++;

    }

    void jumpAudioEvent()
    {
        jumpAudio.pitch = Random.Range(startPitch - 0.13f, startPitch + 0.13f);
        jumpAudio.PlayOneShot(jumpAudio.clip,jumpAudio.volume);
    }

    public bool IsSlowMotionActive() {
        return isOnSlowMotion;
    }
}
