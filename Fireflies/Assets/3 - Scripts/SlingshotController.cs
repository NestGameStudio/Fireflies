using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SlingshotController : MonoBehaviour
{
    [Header("Controle da Movimentação")]
    [Space(0.3f)]

    [Range(0.0f, 1.0f)] public float TimeSlow = 0.02f;


    [Header("Configuração da linha do slingshot")]
    [Space(0.3f)]

    public Material LineMaterial = new Material(Shader.Find("Particles/Additive"));
    public float LineWidth = 0.2f;


    // -> Controles
    private PlayerControls controls;

    private InputAction slowMotion;
    private InputAction slingshotMovementDirection;


    // -> Variáveis da movimentação

    // Entrou no slowMotion com um clique
    private bool isOnSlowMotion = false;

    // Está com o pulo carregado
    //public bool 
    
    // Linha que indica a direção e intensidade que vai sair a movimentação da Cali 
    private LineRenderer line;
    // Direção atualizada conforme pega os valores do analógico/mouse
    private Vector2 direction;

    // -> Controles
    private void Awake() {

        controls = new PlayerControls();

        // Define os controles do input system (Keyboard & mouse, mouse e gamepad)
        slowMotion = controls.Gameplay.SlingshotSlowMotion;
        slingshotMovementDirection = controls.Gameplay.SlingshotMovementDirection;

        slowMotion.performed += EnterSlowMotionMode;
        slowMotion.canceled += ExitSlowMotionMode;

        // linha de desenho da movimentação
        lineSetup();
    }

    private void OnEnable() { 
        slowMotion.Enable();
        slingshotMovementDirection.Enable();
    }

    private void OnDisable() {
        slowMotion.Disable();
        slingshotMovementDirection.Disable();
    }

    private void Update() {

        direction = slingshotMovementDirection.ReadValue<Vector2>();

        // Se está apertando o slowMotion, ajeita a posição do slingshot de acordo com a movimentação do analógico/mouse
        if (isOnSlowMotion) {
            AdjustSlingshot();
        }
    }

    // -> Slow Motion 
    private void EnterSlowMotionMode(InputAction.CallbackContext context) {

        print("Time Slow");
        Time.timeScale = TimeSlow;
        //Time.fixedDeltaTime = TimeSlow * Time.deltaTime;

        isOnSlowMotion = true;
    }

    private void ExitSlowMotionMode(InputAction.CallbackContext context) {

        print("Time Normal");
        Time.timeScale = 1f;
        //Time.fixedDeltaTime = 1f * Time.deltaTime;

        isOnSlowMotion = false;
    }

    // -> Slingshot

    // Define as propriedades da seta que vai indicar a movimentação da Cali
    private void lineSetup() {

        line.material = LineMaterial;
        line.widthMultiplier = LineWidth;
        line.positionCount = 2;

    }

    // Ajusta a posição da linha do slingshot quando movimenta o analógico/mouse
    private void adjustSlingshot() {



    }

}
