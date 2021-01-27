using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;


public class JumpTimeController : MonoBehaviour
{
    public float MaxJumpTime = 10f;
    public ControlManager ControlManager;

    private Slider slider;
    private bool isJumping = false;
    private float currentTime;

    // Start is called before the first frame update
    void Start() {
        slider = GetComponent<Slider>();
        currentTime = MaxJumpTime;
        slider.maxValue = MaxJumpTime;
    }

    // Update is called once per frame
    void Update() {  

        if (isJumping) {
            DecreaseTimer();
        }
        
    }

    public void DecreaseTimer() {;

        currentTime = Mathf.Clamp(currentTime - Time.unscaledDeltaTime, 0, MaxJumpTime);
        slider.value = currentTime;

        if (currentTime <= 0) {
            ControlManager.slowMotion.Disable();
            ControlManager.slowMotion.Enable();
        }
    }

    public void ResetJumpTimer() {
        currentTime = MaxJumpTime;
        slider.value = currentTime;
    }

    public void StartJumpTimer() {
        isJumping = true;
    }

    public void StopJumpTimer() {
        isJumping = false;
    }


}
