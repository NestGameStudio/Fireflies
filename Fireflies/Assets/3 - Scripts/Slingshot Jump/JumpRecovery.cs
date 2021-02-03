using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpRecovery : MonoBehaviour
{
    public JumpTimeController JumpTimeController;

    private bool canJump = false;     // Consegue executar o pulo
    public Animator playerAnim;

    private void Update() {
        //print("can Jump: " + canJump); 
    }

    public bool CanJump() {
        return canJump;
    }

    public void setJump(bool value) {

        if(value == true) {
            if (JumpTimeController != null) {
                JumpTimeController.ResetJumpTimer();
            }
        }

        canJump = value;
        playerAnim.SetBool("canJump", value);
    }

}
