using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpRecovery : MonoBehaviour
{
    public JumpTimeController JumpTimeController;
    public JumpChargeUI chargeUI;

    [HideInInspector]
    public bool canJump = false;     // Consegue executar o pulo
    public int jumpCharges = 0;     // Cargas de pulo atuais
    public Animator playerAnim;
    private PlayerValues values;

    void Start(){
        values = Setup.Instance.PlayerValue;
        if(chargeUI != null) chargeUI.Setup(values.MaxJump);
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

    public void useJumpCharge(){

        if(jumpCharges <= 0) return;

        else {
            jumpCharges--;
            if(jumpCharges <= 0){
                jumpCharges = 0;
                setJump(false);
            }
            chargeUI.UpdateCharges(jumpCharges);
        }
    }

    public void restoreJumpCharge(bool max){
        if(max){
            jumpCharges = values.MaxJump;
            setJump(true);

            chargeUI.UpdateCharges(jumpCharges);
        } else {
            restoreJumpCharge();
        }
    }

    public void restoreJumpCharge(){
        jumpCharges++;
        if(jumpCharges > values.MaxJump){
            jumpCharges = values.MaxJump;
        }
        setJump(true);

        chargeUI.UpdateCharges(jumpCharges);
    }

}
