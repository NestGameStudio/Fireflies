using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private PlayerControls controls;

    private InputAction slowMotion;
    private InputAction slingshotMovementDirection;

    private Vector2 direction;

    private void Awake() {

        controls = new PlayerControls();

        slowMotion = controls.Gameplay.SlingshotSlowMotion;
        slingshotMovementDirection = controls.Gameplay.SlingshotMovementDirection;

        slowMotion.performed += EnterSlowMotionMode;
        slowMotion.canceled += ExitSlowMotionMode;

    }

    private void Update() {

        direction = slingshotMovementDirection.ReadValue<Vector2>();

    }

    private void EnterSlowMotionMode(InputAction.CallbackContext context) {

    }

    private void ExitSlowMotionMode(InputAction.CallbackContext context) {

    }

}
