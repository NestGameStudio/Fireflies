using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpRecovery : MonoBehaviour
{
    public JumpTimeController JumpTimeController;

    [HideInInspector]
    public bool canJump = false;     // Consegue executar o pulo
    public int jumpCharges = 0;     // Cargas de pulo atuais
    public int maxJumpCharges = 1;  // Máximo de cargas de pulo
    public Animator playerAnim;


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

    public void useJumpCharge(){

        if(jumpCharges <= 0) return;

        else {
            jumpCharges--;
            if(jumpCharges <= 0){
                jumpCharges = 0;
                setJump(false);
            }
        }
    }

    public void restoreJumpCharge(bool max){
        if(max){
            jumpCharges = maxJumpCharges;
            setJump(true);
        } else {
            restoreJumpCharge();
        }
    }

    public void restoreJumpCharge(){
        jumpCharges++;
        if(jumpCharges > maxJumpCharges){
            jumpCharges = maxJumpCharges;
        }
        setJump(true);
    }

}
