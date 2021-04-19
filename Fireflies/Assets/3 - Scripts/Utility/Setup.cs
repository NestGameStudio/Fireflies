﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Setup: MonoBehaviour {
    public static Setup Instance { get { return instance; } }
    private static Setup instance;

    // ------------------- Timer -------------------------
    //[Header("Tempo")]
    //[Space(0.3f)]

    //[Tooltip(" Inicia o timer geral do jogo depois que solta o botão de pulo")]
    //public bool StartTimerAfterJump = true;

    // ------------------- Seta -------------------------
    [Header("Seta")]
    [Space(0.3f)]

    [Tooltip("Usar arco de trajetória de impulso")]
    public bool ShowArc = true; 
    
    [Tooltip("Usar linha reta de indicação de direção de impulso")]
    public bool ShowArrow = false;

    [Tooltip("Linha da seta avança apenas para frente do player (false) ou para frente e para trás do player (true)")]
    public bool TransversalArrow = true;

    [Tooltip("Input será invertido, ou seja, arrastar para a direção (false) ou ao contrário da direção (true)")]
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

    public PlayerValues PlayerValue;

    private void Awake() {

        // Singleton
        if (instance != null && instance != this) {
            Destroy(this.gameObject);
        } else {
            instance = this;
        }
    }

}
