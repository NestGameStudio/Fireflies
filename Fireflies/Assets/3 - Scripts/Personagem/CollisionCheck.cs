using System.Collections;
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
            case "Bleeper_Invulneravel":
                SceneManager_Level.sceneManagerInstance.posicionarCali();
                break;
            case "Inimigo":
                SceneManager_Level.sceneManagerInstance.posicionarCali();
                break;
            case "Perigo":
                SceneManager_Level.sceneManagerInstance.posicionarCali();
                break;
            case "Level_Final":
                SceneManager_Level.sceneManagerInstance.nextLevel();
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
