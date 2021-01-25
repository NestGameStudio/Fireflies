using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionCheck : MonoBehaviour
{
    public JumpRecovery Jump;
    public Respawn Respawn;
    public GameObject paredeParticle;

    // Faz todos os checks que precisam de colisão
    private void OnCollisionEnter2D(Collision2D collision) {

        switch (collision.transform.tag) {
            case "Plataforma_Recarregavel":
                Jump.setJump(true);

                //funcao para camerashake ----------------------------> shakecam(intensidade,frequencia,tempo)
                if(CameraShake.instance != null) { CameraShake.instance.shakeCam(gameObject.GetComponent<Rigidbody2D>().velocity.magnitude/6,1, 0.2f); }

                //instanciar particula de colisao com a parede
                Instantiate(paredeParticle,transform.position,Quaternion.identity);
                
                break;
            case "Bleeper_Invulneravel":
                Respawn.RepositionPlayer();
                break;
            case "Inimigo":
                Respawn.RepositionPlayer();
                break;
            case "Perigo":
                Respawn.RepositionPlayer();
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
