using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Setup: MonoBehaviour {
    public static Setup Instance { get { return instance; } }
    private static Setup instance;

    // ------------------- Timer -------------------------
    [Header("Tempo")]
    [Space(0.3f)]

    [Tooltip(" Inicia o timer geral do jogo depois que solta o botão de pulo")]
    //public bool StartTimerAfterJump = true;

    // ------------------- Seta -------------------------
    [Header("Seta")]
    [Space(0.3f)]

    public bool TransversalArrow = true;
    public bool InvertedSlingshot = true;
    public float LineWidth = 0.2f;

    // ------------------- Circunferência -------------------------
    [Header("Circunferência")]
    [Space(0.3f)]

    [Tooltip("Mostra a circunferencia.")]
    public bool showCincunference = true;

    [Tooltip("Mostra a circunferencia no player. Caso contrário a circunferência aparecerá no local de referência.")]
    public bool cincunferenceInPlayer = true;

    // ------------------- Ponto de Referência -------------------------
    [Header("Ponto de Referência")]
    [Space(0.3f)]

    [Tooltip("Mostra ou não visualmente o ponto de referência na scene")]
    public bool showReference = true;

    [Tooltip("Mostra ou não visualmente o cursor do mouse")]
    public bool showCursor = true;

    [Tooltip("Quando inicia o slowmotion o mouse começa no meio da tela")]
    public bool setReferenceInCenter = true;

    [Tooltip("(Mouse/Keyboard & Mouse) Define a ponto de centro para que aconteça a rotação da linha de slingshot da Cali, ele está indicado como um Gizmo no editor." +
          "Caso seja true, o ponto de referência deste centro está acompenhando a movimentação da Cali. Caso seja false, o ponto de referência está aonde foi clicado com o mouse." +
          "Caso esteja usando Gamepad o ponto de referência é sempre o jogador.")]
    public bool ClickReferenceInPlayer = true;

    [Tooltip("(Mouse/Keyboard & Mouse/Gamepad) Valido apenas para quando o ponto de referencia está no player. Se true, o ponto de referência segue a posição da Cali que se move mesmo em slow motion." +
             "Se falso, ele usa como ponto de referência a posição original da Cali quando clicou com o mouse.")]
    public bool ReferenceFollowPlayer = true;

    // ------------------- Pulo -------------------------
    [Header("Pulo")]
    [Space(0.3f)]

    public float ImpulseForce = 0.78f;
   

    [Tooltip("O tamanho máximo afeta o impulso final da Cali")]
    public float LineMaxRadius = 3.0f;
    [Tooltip("O tamanho mínimo afeta o impulso final da Cali")]
    public float LineMinRadius = 1.5f;



    // ------------------- Time slow -------------------------
    [Header("Time Slow")]
    [Space(0.3f)]

    [Range(0.0f, 1.0f)] public float TimeSlow = 0.02f;

    // ------------------- Gamepad -------------------------
    [Header("Gamepad")]
    [Space(0.3f)]

    [Range(1, 10)] public float GamepadSensibility = 2.5f;
    public float JumpDelay = 0.1f;

    private void Awake() {

        // Singleton
        if (instance != null && instance != this) {
            Destroy(this.gameObject);
        } else {
            instance = this;
        }
    }

}
