﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.InputSystem;
//using UnityEngine.InputSystem.Users;

public class SlingshotController : MonoBehaviour {

    // ------------------- Movimentação -------------------------
    [Header("Controle da Movimentação")]
    [Space(0.3f)]

    [Range(0.0f, 1.0f)] public float TimeSlow = 0.02f;
    public float ImpulseForce = 330f;
    public SlingshotVisual slingshotVisual;

    // ------------------- Ponto de referência -------------------------
    [Header("Teste & Debug - Referência de Giro")]
    [Space(0.3f)]

    [Tooltip ("(Mouse/Keyboard & Mouse) Define a ponto de centro para que aconteça a rotação da linha de slingshot da Cali, ele está indicado como um Gizmo no editor." +
              "Caso seja true, o ponto de referência deste centro está acompenhando a movimentação da Cali. Caso seja false, o ponto de referência está aonde foi clicado com o mouse." +
              "Caso esteja usando Gamepad o ponto de referência é sempre o jogador.")]
    public bool ClickReferenceInPlayer = true;

    [Tooltip("(Mouse/Keyboard & Mouse/Gamepad) Valido apenas para quando o ponto de referencia está no player. Se true, o ponto de referência segue a posição da Cali que se move mesmo em slow motion." +
             "Se falso, ele usa como ponto de referência a posição original da Cali quando clicou com o mouse.")]
    public bool ReferenceFollowPlayer = true;

    // ------------- Variáveis privadas ------------------

    private JumpRecovery JumpControl; 

    // Variáveis da movimentação
    private bool isOnSlowMotion = false;    // Entrou no slowMotion com um clique
    private bool canJump = true;             // Está com o pulo carregado

    // Direção atualizada conforme pega os valores do analógico/mouse
    [HideInInspector] public Vector2 direction;
    // o centro da linha
    private Vector2 lineCenterPos;

    private Vector2 impulseVector = Vector2.zero;

    public AudioSource jumpAudio;
    private float startPitch;
    private AudioLowPassFilter audioMusic; 

    // ------------- Setup e checagens ------------------
    private void Start() {
        JumpControl = this.GetComponent<JumpRecovery>();

        startPitch = jumpAudio.pitch;
        

        if(audioMusic == null)
        {
            audioMusic = GameObject.FindGameObjectWithTag("Music").GetComponent<AudioLowPassFilter>();
            audioMusic.enabled = false;
        }
    }

    // Verifica se usa recursos de seguir o player -> atualiza a posição das coisas visuais
    private void Update() {

        // Se está apertando o slowMotion, ajeita a posição do slingshot de acordo com a movimentação do analógico/mouse
        if (isOnSlowMotion) {

            if (ClickReferenceInPlayer & ReferenceFollowPlayer || ControlManager.Instance.getCurrentControlScheme() == ControlScheme.Gamepad) {
                CenterReference();
            }

            slingshotVisual.SetFinalLinePosition(direction);
            adjustImpulse();
        }

    }
   
    // -> Slow Motion 
    public void EnterSlowMotionMode() {

        if (JumpControl.CanJump()) {

            Time.timeScale = TimeSlow;
            Time.fixedDeltaTime = TimeSlow * Time.deltaTime;    // faz com que o slowmotion não fique travado

            CenterReference();
            slingshotVisual.SlingshotVisualSetup(lineCenterPos);

            isOnSlowMotion = true;

            audioMusic.enabled = true;
        }
    }

    public void ExitSlowMotionMode() {

        // precisa do isOnSlowMotion para que não ocorra o soft lock
        if (JumpControl.CanJump() && isOnSlowMotion) {

            Time.timeScale = 1f;
            slingshotVisual.DisableSlingshotVisuals();
            
            //evita pulos de força extremamente baixa (game breaking)
            if (impulseVector.magnitude > slingshotVisual.GetMinLine()) {
                Jump();
                JumpControl.setJump(false);
            }

            isOnSlowMotion = false;
            audioMusic.enabled = false;
        }
    }

    // -> Slingshot

    // Pega a referência do centro da rotação da linha do slingshot conforme o padrão de controles
    private void CenterReference() {

        if (ClickReferenceInPlayer || ControlManager.Instance.getCurrentControlScheme() == ControlScheme.Gamepad) {
            lineCenterPos = this.gameObject.transform.position;

        } else if (!ClickReferenceInPlayer) {

            lineCenterPos = Camera.main.ScreenToWorldPoint(direction);
        }
    }
    
    // Ajusta a posição da linha do slingshot quando movimenta o analógico/mouse
    private void adjustImpulse() {

        // Posição atual do Mouse ou Analógico
        Vector2 lineFinalPos = Camera.main.ScreenToWorldPoint(direction);
        //Debug.Log("Direction - x: " + direction.x + " | y: " + direction.y);

        if ((lineCenterPos - lineFinalPos).magnitude <= slingshotVisual.GetMaxLine()) {

            if (slingshotVisual.InvertedSlingshot) {
                impulseVector = lineCenterPos - lineFinalPos;
            } else {
                impulseVector = lineFinalPos - lineCenterPos;
            }

        // Passou da linha limite
        } else {

            if (slingshotVisual.InvertedSlingshot) {
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
        Vector2 impulse = new Vector2(impulseVector.x, impulseVector.y) * ImpulseForce;
        rb.AddForce(impulse, ForceMode2D.Impulse);

        jumpAudioEvent();

        impulseVector = Vector2.zero;

    }

    void jumpAudioEvent()
    {
        jumpAudio.pitch = Random.Range(startPitch - 0.13f, startPitch + 0.13f);
        jumpAudio.PlayOneShot(jumpAudio.clip,jumpAudio.volume);
    }
}
