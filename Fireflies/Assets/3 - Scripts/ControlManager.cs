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

    // Referência ao setting de controle da Cali
    private PlayerControls controls;

    private ControlScheme currentControlScheme = ControlScheme.Gamepad;

    // Actions possíveis 
    private InputAction slowMotion;
    private InputAction slingshotMovementDirection;

    private Vector2 directions;

    // ------------- Ativa e coleta inputs ------------------
    private void OnEnable() {

        // Ativa as actions individualmente
        slowMotion.Enable();
        slingshotMovementDirection.Enable();

        // Verifica se houve alguma troca de controles (keyboard para gamepad viceversa)
        InputUser.onChange += onInputDeviceChange;
    }

    private void OnDisable() {

        // Desativa as actions
        slowMotion.Disable();
        slingshotMovementDirection.Disable();

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

        // Define os controles do input system (Keyboard & mouse, mouse e gamepad)
        slowMotion = controls.Gameplay.SlingshotSlowMotion;
        slingshotMovementDirection = controls.Gameplay.SlingshotMovementDirection;

        slowMotion.performed += EnterSlowMotionMode;
        slowMotion.canceled += ExitSlowMotionMode;

    }

    // Update is called once per frame
    void Update() {

        if (currentControlScheme == ControlScheme.Gamepad) {

            //print((Vector2) SlingshotController.gameObject.transform.position + "A");
            //print(slingshotMovementDirection.ReadValue<Vector2>() + "B");
            print((Vector2)SlingshotController.gameObject.transform.position + (slingshotMovementDirection.ReadValue<Vector2>() * 2) + "C");

            directions = (Vector2) SlingshotController.gameObject.transform.position + (slingshotMovementDirection.ReadValue<Vector2>() * 2);

        } else {
            directions = slingshotMovementDirection.ReadValue<Vector2>();
            print(Camera.main.ScreenToWorldPoint(slingshotMovementDirection.ReadValue<Vector2>()) + "A");

            // Isso aqui é a posição da tela
            // a direção é a posição do mouse na tela
        }

        SlingshotController.direction = directions;
    }

    // ------------- Funções das actions ------------------
    private void EnterSlowMotionMode(InputAction.CallbackContext context) {
        SlingshotController.EnterSlowMotionMode();
    }

    private void ExitSlowMotionMode(InputAction.CallbackContext context) {
        SlingshotController.ExitSlowMotionMode();
    }

    // ------------- Cuida da troca de devices ------------------
    private void onInputDeviceChange(InputUser user, InputUserChange change, InputDevice device) {

        if (change == InputUserChange.ControlSchemeChanged) {

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
}
