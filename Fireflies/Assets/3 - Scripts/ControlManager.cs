using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;

public enum ControlScheme { Gamepad, KeyboardMouse };

public class ControlManager : MonoBehaviour {

    // Singleton
    public static ControlManager Instance { get { return instance; } }
    private static ControlManager instance;

    // Referência as classes de acesso
    public SlingshotController SlingshotController;

    // Inicia o timer depois que solta o botão de pulo
    public bool StartTimerAfterJump = true;

    // Referência ao setting de controle da Cali
    private PlayerControls controls;

    private ControlScheme currentControlScheme = ControlScheme.Gamepad;

    // Actions possíveis 
    public InputAction slowMotion;
    private InputAction slingshotMovementDirectionMouse;
    private InputAction slingshotMovementDirectionGamepad;

    private Vector2 directions;

    private bool isOnSlowMotion = false;

    // ------------- Ativa e coleta inputs ------------------
    private void OnEnable() {

        controls.Debug.Enable();

        // Ativa as actions individualmente
        slowMotion.Enable();
        slingshotMovementDirectionMouse.Enable();
        slingshotMovementDirectionGamepad.Enable();

        // Verifica se houve alguma troca de controles (keyboard para gamepad viceversa)
        InputUser.onChange += onInputDeviceChange;
    }

    private void OnDisable() {

        controls.Debug.Disable();

        // Desativa as actions
        slowMotion.Disable();
        slingshotMovementDirectionMouse.Disable();
        slingshotMovementDirectionGamepad.Disable();

        // Verifica se houve alguma troca de controles
        InputUser.onChange += onInputDeviceChange;
    }

    private void Awake() {

        // Singleton
        if (instance != null && instance != this) {
            Destroy(this.gameObject);
        } else {
            instance = this;
        }

        controls = new PlayerControls();

        // Debug
        controls.Debug.NextScene.performed += GoToNextScene;
        controls.Debug.PreviousScene.performed += GoToPreviousScene;

        // Define os controles do input system (Keyboard & mouse, mouse e gamepad)
        slowMotion = controls.Gameplay.SlingshotSlowMotion;
        slingshotMovementDirectionMouse = controls.Gameplay.SlingshotMovementDirectionMouse;
        slingshotMovementDirectionGamepad = controls.Gameplay.SlingshotMovementDirectionGamepad;

        slowMotion.performed += EnterSlowMotionMode;
        slowMotion.canceled += ExitSlowMotionMode;

    }

    // Update is called once per frame
    void Update() {

        if (currentControlScheme == ControlScheme.Gamepad) {

            //print((Vector2) SlingshotController.gameObject.transform.position + "A");
            //print(slingshotMovementDirection.ReadValue<Vector2>() + "B");
            //print((Vector2)SlingshotController.gameObject.transform.position + (slingshotMovementDirection.ReadValue<Vector2>() * 2) + "C");

            

            Vector2 maxVector = (Vector2) Camera.main.WorldToScreenPoint(SlingshotController.transform.position) + slingshotMovementDirectionGamepad.ReadValue<Vector2>() * 300;
            directions = maxVector;

            //directions = (Vector2) SlingshotController.gameObject.transform.position + (slingshotMovementDirection.ReadValue<Vector2>() * 2);

        } else {
            directions = slingshotMovementDirectionMouse.ReadValue<Vector2>();
            //print(Camera.main.ScreenToWorldPoint(slingshotMovementDirection.ReadValue<Vector2>()) + "A");

            // Isso aqui é a posição da tela
            // a direção é a posição do mouse na tela
        }


        SlingshotController.direction = directions;
    }

    // ------------- Funções das actions ------------------
    private void EnterSlowMotionMode(InputAction.CallbackContext context) {

        if (!StartTimerAfterJump) {
            StartTimer();
        }

        if (!isOnSlowMotion) {
            SlingshotController.EnterSlowMotionMode();
            isOnSlowMotion = true;
        }
    }

    private void ExitSlowMotionMode(InputAction.CallbackContext context) {

        if (StartTimerAfterJump) {
            StartTimer();
        }

        SlingshotController.ExitSlowMotionMode();
        isOnSlowMotion = false;
    }

    // ---------------- Funções de debug ---------------------
    private void GoToNextScene(InputAction.CallbackContext context) {
        LevelManager.Instance.nextLevel();
    }

    private void GoToPreviousScene(InputAction.CallbackContext context) {
        LevelManager.Instance.previousLevel();
    }

    // ------------- Cuida da troca de devices ------------------
    private void onInputDeviceChange(InputUser user, InputUserChange change, InputDevice device) {

        if (change == InputUserChange.ControlSchemeChanged && !isOnSlowMotion) {

            print(user.controlScheme.Value.name);

            switch (user.controlScheme.Value.name) {
                case "Keyboard and Mouse":
                    currentControlScheme = ControlScheme.KeyboardMouse;
                    break;
                default:
                    currentControlScheme = ControlScheme.Gamepad;
                    break;
            }
        }
    }
    
    // Permite outras classes saberem qual o tipo de controle atual
    public ControlScheme getCurrentControlScheme() {
        return currentControlScheme;
    }

    public void StartTimer() {

        if (!TimerController.Instance.hasStarted) {
            TimerController.Instance.hasStarted = false;
            TimerController.Instance.PauseTimer(false);
        }
    }
}
