using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorRecharge : MonoBehaviour
{
    private Collider2D col;

    [Tooltip("Time to reactivate Rechargeable state, set it to 0 if always active")]
    public float rechargeTime = 1f;
    public Animator anim;
    public bool charged = true;

    public void Start(){
        if(anim==null) anim = GetComponentInChildren<Animator>();
        if(col==null) col = GetComponent<Collider2D>();
        if(charged) EnableTrigger(); else DisableTrigger();
    }

    public void OnTriggerEnter2D (Collider2D other){
        if(charged){
            if(other.CompareTag("Player")){
                DisableTrigger();
                //Liga trigger após X segundos
                Invoke("EnableTrigger", rechargeTime);
            }
        }
    }

    public void DisableTrigger(){
        SetTrigger(false);
    }
    public void EnableTrigger(){
        SetTrigger(true);
    }

    public void SetTrigger(bool state){
        charged = state;
        col.enabled = state;
        anim.SetBool("charged", charged);
    }
}
