using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionCheck : MonoBehaviour
{
    public JumpRecovery Jump;
    public Respawn Respawn;
    public GameObject paredeParticle;
    public GameObject deathParticle;
    public AudioSource colisao;
    public AudioSource Lose;

    // Faz todos os checks que precisam de colisão
    private void OnCollisionEnter2D(Collision2D collision) {

        switch (collision.transform.tag) {
            case "Plataforma_Recarregavel":
                Jump.setJump(true);

                playAudioColisao();

                //funcao para camerashake ----------------------------> shakecam(intensidade,frequencia,tempo)
                if (CameraShake.instance != null) { CameraShake.instance.shakeCam(gameObject.GetComponent<Rigidbody2D>().velocity.magnitude/6,1, 0.13f); }

                //instanciar particula de colisao com a parede
                GameObject particula = Instantiate(paredeParticle,transform.position,Quaternion.identity);
                if (GetComponent<Rigidbody2D>().velocity.magnitude > 10000f)
                {
                    particula.GetComponent<ParticleSystem>().startSpeed = gameObject.GetComponent<Rigidbody2D>().velocity.magnitude * 1.2f;
                }
                break;

            case "Plataforma_Quebravel":

                Jump.setJump(true);

                playAudioColisao();

                //funcao para camerashake ----------------------------> shakecam(intensidade,frequencia,tempo)
                if (CameraShake.instance != null) { CameraShake.instance.shakeCam(gameObject.GetComponent<Rigidbody2D>().velocity.magnitude / 6, 1, 0.13f); }

                //instanciar particula de colisao com a parede
                GameObject particula2 = Instantiate(paredeParticle, transform.position, Quaternion.identity);
                if (GetComponent<Rigidbody2D>().velocity.magnitude > 10000f)
                {
                    particula2.GetComponent<ParticleSystem>().startSpeed = gameObject.GetComponent<Rigidbody2D>().velocity.magnitude * 1.2f;

                }

                LevelManager.Instance.getLevelBreakPlats();

                //fazer com que a plataforma desapareca
                //collision.transform.parent.gameObject.SetActive(false);
                collision.gameObject.GetComponentInParent<Animator>().SetBool("Break",true);

                    break;

            case "Plataforma_Quebravel_Fake":

                //funcao para camerashake ----------------------------> shakecam(intensidade,frequencia,tempo)
                if (CameraShake.instance != null) { CameraShake.instance.shakeCam(gameObject.GetComponent<Rigidbody2D>().velocity.magnitude / 6, 1, 0.13f); }

                //instanciar particula de colisao com a parede
                GameObject particula3 = Instantiate(paredeParticle, transform.position, Quaternion.identity);
                if (GetComponent<Rigidbody2D>().velocity.magnitude > 10000f)
                {
                    particula3.GetComponent<ParticleSystem>().startSpeed = gameObject.GetComponent<Rigidbody2D>().velocity.magnitude * 1.2f;

                }

                LevelManager.Instance.getLevelBreakPlats();

                //fazer com que a plataforma desapareca
                //collision.transform.parent.gameObject.SetActive(false);
                collision.gameObject.GetComponentInParent<Animator>().SetBool("Break", true);

                break;
            case "Bleeper_Invulneravel":

                if (CameraShake.instance != null) { CameraShake.instance.shakeCam(2, 1, 0.5f); }

                playAudioLose();

                //particula de morte
                if (deathParticle != null)
                    deathParticleTrigger();

                Respawn.RepositionPlayer();
                break;
            case "Inimigo":

                if (CameraShake.instance != null) { CameraShake.instance.shakeCam(2, 1, 0.5f); }

                playAudioLose();

                //particula de morte
                if (deathParticle != null)
                    deathParticleTrigger();

                Respawn.RepositionPlayer();
                break;
            case "Perigo":

                if (CameraShake.instance != null) { CameraShake.instance.shakeCam(2, 1, 0.5f); }

                playAudioLose();

                //particula de morte
                if (deathParticle != null)
                    deathParticleTrigger();

                Respawn.RepositionPlayer();
                break;
            default:
                break;
        }
        
    }

    void deathParticleTrigger()
    {
        Instantiate(deathParticle,gameObject.transform.position,Quaternion.identity);
    }

    private void OnCollisionStay2D(Collision2D collision) {
        
    }

    private void OnCollisionExit2D(Collision2D collision) {
        
    }
    void playAudioColisao()
    {
        //play audio
        colisao.pitch = Random.Range(0.9f,1.1f);
        colisao.PlayOneShot(colisao.clip, colisao.volume.Remap(0,1,0, GetComponent<Rigidbody2D>().velocity.magnitude/2));
    }
    void playAudioLose()
    {
        //play audio
        Lose.pitch = Random.Range(0.9f, 1.1f);
        Lose.PlayOneShot(Lose.clip, Lose.volume);
    }

}
