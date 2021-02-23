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
    private float startPitch;
    public PhysicsMaterial2D playerMaterial;
    public PhysicsMaterial2D playerMaterialCurva;
    public PhysicsMaterial2D platformMaterial;
    private Rigidbody2D rb;

    public float collisionStayDelay = 0.3f;
    private float currentColissionStayTimer = 0;
    private bool canRecharge = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        startPitch = colisao.pitch;

    }

    private void Update() {

        if (!Jump.CanJump()) {
            currentColissionStayTimer += Time.unscaledDeltaTime;

            if(currentColissionStayTimer >= collisionStayDelay) {
                canRecharge = true;
                currentColissionStayTimer = 0;
            }
        } else {
            canRecharge = false;
        }

    }

    // Faz todos os checks que precisam de colisão
    private void OnCollisionEnter2D(Collision2D collision) {

        switch (collision.transform.tag) {
            case "Plataforma_Recarregavel":
                Jump.setJump(true);

                playAudioColisao();
                playFeedbackRecarga();

                break;

            case "PlatRec_Curva":
                Jump.setJump(true);

                playAudioColisao();
                playFeedbackRecarga();

                break;

            case "Plataforma_Quebravel":

                Jump.setJump(true);

                playAudioColisao();
                playFeedbackRecarga();
                
                //resetMaterial(collision);
                LevelManager.Instance.getLevelBreakPlats();

                //fazer com que a plataforma desapareca
                //collision.transform.parent.gameObject.SetActive(false);
                collision.gameObject.GetComponentInParent<Animator>().SetBool("Break",true);

                    break;

            case "Plataforma_Quebravel_Fake":

                LevelManager.Instance.getLevelBreakPlats();

                //fazer com que a plataforma desapareca
                //collision.transform.parent.gameObject.SetActive(false);
                collision.gameObject.GetComponentInParent<Animator>().SetBool("Break", true);

                break;
            case "Bleeper_Invulneravel":

                //if (CameraShake.instance != null) { CameraShake.instance.shakeCam(2, 1, 0.5f); }

                //playAudioLose();

                //particula de morte e contador de morte
                if (deathParticle != null)
                    deathParticleTrigger();

                //Respawn.RepositionPlayer();

                //tomou dano
                dano();

                break;
            case "Inimigo":

                //if (CameraShake.instance != null) { CameraShake.instance.shakeCam(2, 1, 0.5f); }

                //playAudioLose();

                //particula de morte e contador de morte
                if (deathParticle != null)
                    deathParticleTrigger();

                //Respawn.RepositionPlayer();

                //tomou dano
                dano();
                break;
            case "Perigo":

                

                //playAudioLose();

                //particula de morte e contador de morte
                if (deathParticle != null)
                    deathParticleTrigger();

                //Respawn.RepositionPlayer();

                //tomou dano
                dano();

                break;
            default:
                break;
        }
        
    }

    private void OnCollisionStay2D(Collision2D collision) {
        
        if (!Jump.CanJump() && canRecharge) {

            switch (collision.transform.tag) {
                case "Plataforma_Recarregavel":
                    Jump.setJump(true);

                    playAudioColisao();
                    playFeedbackRecarga();
                    break;
                case "PlatRec_Curva":
                    Jump.setJump(true);

                    playAudioColisao();
                    playFeedbackRecarga();
                    break;
                case "Plataforma_Quebravel":
                    Jump.setJump(true);

                    playAudioColisao();
                    playFeedbackRecarga();
                    break;
                default:
                    break;
            }
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "PlatRec_Curva")
        {
            setCurveMaterial(collision);
        }
        else
        {
            resetMaterial(collision);
        }
    }
    void deathParticleTrigger()
    {
        Instantiate(deathParticle,gameObject.transform.position,Quaternion.identity);

        if (deathCounter.instance != null)
        {
            deathCounter.instance.addDeath();
        }
    }

    void resetMaterial(Collider2D collision)
    {
        if (collision.gameObject.GetComponentInChildren<EdgeCollider2D>() != null)
        {
            collision.gameObject.GetComponentInChildren<EdgeCollider2D>().sharedMaterial = null;
        }
        rb.sharedMaterial = playerMaterial;
    }
    void setCurveMaterial(Collider2D collision)
    {
        if (collision.gameObject.GetComponentInChildren<EdgeCollider2D>() != null)
        {
            collision.gameObject.GetComponentInChildren<EdgeCollider2D>().sharedMaterial = platformMaterial;
        }
        rb.sharedMaterial = playerMaterialCurva;
    }
    private void OnCollisionExit2D(Collision2D collision) {
        //print("sai disso");
    }

    void playAudioColisao()
    {
        //play audio
        colisao.pitch = Random.Range(startPitch - 0.1f, startPitch + 0.1f);
        colisao.PlayOneShot(colisao.clip, colisao.volume.Remap(0,1,0, rb.velocity.magnitude/2));
    }
    void playAudioLose()
    {
        //play audio
        Lose.pitch = Random.Range(0.9f, 1.1f);
        Lose.PlayOneShot(Lose.clip, Lose.volume);
    }

    void dano()
    {
        //camera shake
        if (CameraShake.instance != null) { CameraShake.instance.shakeCam(2, 1, 0.5f); }

        //tocar audio de dano
        playAudioLose();

        //perder vida
        HealthManager.instance.menosVida(10);
    }

    void playFeedbackRecarga(){
        //funcao para camerashake ----------------------------> shakecam(intensidade,frequencia,tempo)
        if (CameraShake.instance != null) { CameraShake.instance.shakeCam(rb.velocity.magnitude/6,1, 0.13f); }

        //resetMaterial(collision);

        //instanciar particula de colisao com a parede
        ParticleSystem particula = Instantiate(paredeParticle,transform.position,Quaternion.identity).GetComponent<ParticleSystem>();
        if (rb.velocity.magnitude > 10000f){
            particula.startSpeed = rb.velocity.magnitude * 1.2f;
        }
    }

}
