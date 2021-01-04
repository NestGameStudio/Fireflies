using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;

public class SlingshotController : MonoBehaviour {

    [Header("Controle da Movimentação")]
    [Space(0.3f)]

    [Range(0.0f, 1.0f)] public float TimeSlow = 0.02f;


    [Header("Configuração da linha do slingshot")]
    [Space(0.3f)]

    public Material LineMaterial;
    public float LineWidth = 0.2f;

    [Header("Teste e Debug")]
    [Space(0.3f)]

    [Tooltip ("(Mouse/Keyboard & Mouse) Define a ponto de centro para que aconteça a rotação da linha de slingshot da Cali, ele está indicado como um Gizmo no editor." +
              "Caso seja true, o ponto de referência deste centro está acompenhando a movimentação da Cali. Caso seja false, o ponto de referência está aonde foi clicado com o mouse." +
              "Caso esteja usando Gamepad o ponto de referência é sempre o jogador.")]
    public bool ClickReferenceInPlayer = true;
    [Tooltip("(Mouse/Keyboard & Mouse/Gamepad) Valido apenas para quando o ponto de referencia está no player. Se true, o ponto de referência segue a posição da Cali que se move mesmo em slow motion." +
             "Se falso, ele usa como ponto de referência a posição original da Cali quando clicou com o mouse.")]
    public bool ReferenceFollowPlayer = true;


    // -> Controles

    private enum ControlScheme { Gamepad, Mouse, KeyboardMouse }

    private PlayerControls controls;
    private ControlScheme currentControlScheme = ControlScheme.Gamepad;

    private InputAction slowMotion;
    private InputAction slingshotMovementDirection;
     

    // -> Variáveis da movimentação

    // Entrou no slowMotion com um clique
    private bool isOnSlowMotion = false;

    // Está com o pulo carregado
    private bool canJump = true;
    
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
    }

    private void OnEnable() {

        slowMotion.Enable();
        slingshotMovementDirection.Enable();

        InputUser.onChange += onInputDeviceChange;

        // linha de desenho da movimentação
        lineSetup();
    }

    private void OnDisable() {

        slowMotion.Disable();
        slingshotMovementDirection.Disable();

        InputUser.onChange += onInputDeviceChange;
    }

    private void Update() {

        direction = slingshotMovementDirection.ReadValue<Vector2>();

        // Se está apertando o slowMotion, ajeita a posição do slingshot de acordo com a movimentação do analógico/mouse
        if (isOnSlowMotion) {
            adjustSlingshot();
        }
    }

    private void onInputDeviceChange(InputUser user, InputUserChange change, InputDevice device) {

        if (change == InputUserChange.ControlSchemeChanged) {

            switch (user.controlScheme.Value.name) {
                case "Mouse":
                    print("mouse");
                    break;
                case "Keyboard and Mouse":
                    print("keyboard");
                    break;
                default:
                    print("gamepad");
                    break;
            }
        }
    }
   

    // -> Slow Motion 
    private void EnterSlowMotionMode(InputAction.CallbackContext context) {

        if (canJump) {

            print("Time Slow");
            Time.timeScale = TimeSlow;
            //Time.fixedDeltaTime = TimeSlow * Time.deltaTime;

            isOnSlowMotion = true;
        }
    }

    private void ExitSlowMotionMode(InputAction.CallbackContext context) {

        if (canJump) {

            print("Time Normal");
            Time.timeScale = 1f;
            //Time.fixedDeltaTime = 1f * Time.deltaTime;

            isOnSlowMotion = false;
            canJump = false;
        }
    }

    // -> Slingshot

    // Define as propriedades da seta que vai indicar a movimentação da Cali
    private void lineSetup() {

        line.material = LineMaterial;
        line.widthMultiplier = LineWidth;
        line.positionCount = 2;

    }

    // Pega a referência do centro da rotação da linha do slingshot conforme o padrão de controles
    private Vector3 InitialReference() {

        Vector3 position = Vector3.zero;


        /* if (ClickReferenceInPlayer || controls. == controls.GamepadScheme) {



        } else {

            position = Camera.main.ScreenToWorldPoint()

        }*/

        return position;
    }

    // Ajusta a posição da linha do slingshot quando movimenta o analógico/mouse
    private void adjustSlingshot() {



    }

}
