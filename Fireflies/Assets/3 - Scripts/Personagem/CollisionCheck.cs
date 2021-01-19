﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionCheck : MonoBehaviour
{
    public JumpRecovery Jump;

    // Faz todos os checks que precisam de colisão
    private void OnCollisionEnter2D(Collision2D collision) {

        switch (collision.transform.tag) {
            case "Plataforma_Recarregavel":
                Jump.setJump(true);
                break;
            default:
                break;
        }
        
    }

    private void OnCollisionStay2D(Collision2D collision) {
        
    }

    private void OnCollisionExit2D(Collision2D collision) {
        
    }


}