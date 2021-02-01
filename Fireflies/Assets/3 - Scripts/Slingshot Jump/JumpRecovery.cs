using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpRecovery : MonoBehaviour
{
    public JumpTimeController JumpTimeController;

    private bool canJump = true;     // Consegue executar o pulo

    public bool CanJump() {
        return canJump;
    }

    public void setJump(bool value) {

        if(value == true) {
            //JumpTimeController.ResetJumpTimer();
        }

        canJump = value;
    }

}
