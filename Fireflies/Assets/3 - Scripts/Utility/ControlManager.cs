using System.Collections;
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
    public JumpRecovery JumpControl;

    // Referência ao setting de controle da Cali
    private PlayerControls controls;

    private ControlScheme currentControlScheme = ControlScheme.KeyboardMouse;

    // Actions possíveis 
    public InputAction slowMotion;
    private InputAction slingshotMovementDirectionMouse;
    private InputAction slingshotMovementDirectionGamepad;

    private Vector2 directions;

    private bool isOnSlowMotion = false;
    private bool gamepadDelay = false;
    private float currentDelayTime = 0;

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
        controls.Debug.InvertDirection.performed += InvertDirection;
        controls.Debug.QuitGame.performed += QuitGame;

        // Define os controles do input system (Keyboard & mouse, mouse e gamepad)
        slowMotion = controls.Gameplay.SlingshotSlowMotion;
        slingshotMovementDirectionMouse = controls.Gameplay.SlingshotMovementDirectionMouse;
        slingshotMovementDirectionGamepad = controls.Gameplay.SlingshotMovementDirectionGamepad;

        slowMotion.performed += EnterSlowMotionMode;
        slowMotion.canceled += ExitSlowMotionMode;

        EnableCursor(false);

        currentDelayTime = Setup.Instance.JumpDelay;
    }

    // Update is called once per frame
    void Update() {

        UpdateDirection();

        // Cuida do delay do controle
        if (gamepadDelay) {
            currentDelayTime -= Time.unscaledDeltaTime;

            if (currentDelayTime <= 0) {
                gamepadDelay = false;
                currentDelayTime = Setup.Instance.JumpDelay;
            }
        }
    }

    private void UpdateDirection() {

        if (currentControlScheme == ControlScheme.Gamepad) {

            Vector2 maxVector = (Vector2) Camera.main.WorldToScreenPoint(SlingshotController.transform.position) + 
                                slingshotMovementDirectionGamepad.ReadValue<Vector2>() * Setup.Instance.GamepadSensibility * 100;
            directions = maxVector;

        } else {
            directions = slingshotMovementDirectionMouse.ReadValue<Vector2>();
        }

        SlingshotController.direction = directions;
    }

    // ------------- Funções das actions ------------------
    private void EnterSlowMotionMode(InputAction.CallbackContext context) {

        if (!gamepadDelay) {

            if (currentControlScheme == ControlScheme.Gamepad) {
                gamepadDelay = true;
            }

            isOnSlowMotion = true;

            UpdateDirection();

            //if (!Setup.Instance.StartTimerAfterJump) {
            //    StartTimer();
            //}

            EnableCursor(true);
            SlingshotController.EnterSlowMotionMode();
        } 
    }

    private void ExitSlowMotionMode(InputAction.CallbackContext context) {

        if (isOnSlowMotion) {

            /*if (Setup.Instance.StartTimerAfterJump) {
                StartTimer();
            }*/

            isOnSlowMotion = false;

            EnableCursor(false);

            SlingshotController.ExitSlowMotionMode();
        }

    }

    private void EnableCursor(bool enable) {

        if (currentControlScheme == ControlScheme.KeyboardMouse && Setup.Instance.setReferenceInCenter) {
            Mouse.current.WarpCursorPosition(new Vector2(Screen.width / 2, Screen.height / 2));

            if (enable) {

                if (Setup.Instance.showCursor) {
                    Cursor.visible = true;
                }
                Cursor.lockState = CursorLockMode.None;
            } else {
                if (Setup.Instance.showCursor) {
                    Cursor.visible = false;
                }
                Cursor.lockState = CursorLockMode.Locked;
            }
        }
    }

    // ---------------- Funções de debug ---------------------
    private void GoToNextScene(InputAction.CallbackContext context) {
        LevelManager.Instance.nextLevel();
    }

    private void GoToPreviousScene(InputAction.CallbackContext context) {
        LevelManager.Instance.previousLevel();
    }

    private void InvertDirection(InputAction.CallbackContext context){
        Setup.Instance.InvertedSlingshot = !Setup.Instance.InvertedSlingshot; 
    }

    private void QuitGame(InputAction.CallbackContext context){
        Application.Quit();
    }
    // ------------- Cuida da troca de devices ------------------
    private void onInputDeviceChange(InputUser user, InputUserChange change, InputDevice device) {

        if (change == InputUserChange.ControlSchemeChanged && !isOnSlowMotion) {

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

    //public void starttimer() {

    //    if (!timercontroller.instance.hasstarted && levelmanager.instance.canstarttimer) {
    //        timercontroller.instance.hasstarted = false;
    //        timercontroller.instance.pausetimer(false);

    //    }

    //}

}
