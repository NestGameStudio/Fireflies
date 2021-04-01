using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionCheck : MonoBehaviour
{
    public JumpRecovery Jump;
    public Respawn Respawn;
    public TimerManager Timer;
    public HurtFeedback HurtFeedback;
    public GameObject paredeParticle;
    public GameObject damageParticle;
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

        //Trecho usado para criar delay em OnCollisionStay2D, atualmente inativo
        /*if (!Jump.CanJump()) {
            currentColissionStayTimer += Time.unscaledDeltaTime;

            if(currentColissionStayTimer >= collisionStayDelay) {
                canRecharge = true;
                currentColissionStayTimer = 0;
            }
        } else {
            canRecharge = false;
        }*/

    }

    // Faz todos os checks que precisam de colisão
    private void OnCollisionEnter2D(Collision2D collision) {

        //Bateu em uma plataforma recarregável
        if(collision.transform.CompareTag("Plataforma_Recarregavel")){
            Jump.setJump(true);

            playAudioColisao();
            playFeedbackRecarga();

            return;
        }

        //Bateu em inimigo
        if(collision.transform.CompareTag("Inimigo")){
            Jump.setJump(true);

            playAudioColisao();
            playFeedbackRecarga();

            //tomou dano
            dano();

            return;
        }

        //Bateu em área ou inimigo vulnerável
        if(collision.transform.CompareTag("Inimigo_Vulneravel")){
            Jump.setJump(true);

            playAudioColisao();
            playFeedbackRecarga();

            return;
        }

        //Bateu em perigo
        if(collision.transform.CompareTag("Perigo")){
            Jump.setJump(true);

            playAudioColisao();
            playFeedbackRecarga();

            //tomou dano
            dano();

            return;            
        }

        /*
        switch (collision.transform.tag) {
            case "Plataforma_Recarregavel":
                Jump.setJump(true);

                playAudioColisao();
                playFeedbackRecarga();

                break;
            
            case "Inimigo":

                Jump.setJump(true);

                playAudioColisao();
                playFeedbackRecarga();

                //tomou dano
                dano();
                break;

            case "Inimigo_Vulneravel":
                
                Jump.setJump(true);

                playAudioColisao();
                playFeedbackRecarga();

                break;    

            case "Perigo":

                Jump.setJump(true);

                playAudioColisao();
                playFeedbackRecarga();

                //tomou dano
                dano();
                break;

            default:
                break;
        }
        */
        
    }

    /*private void OnCollisionStay2D(Collision2D collision) {
        
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

    }*/

    private void OnTriggerEnter2D(Collider2D collision){
        /*if(collision.gameObject.tag == "PlatRec_Curva")
        {
            setCurveMaterial(collision);
        }
        else
        {
            resetMaterial(collision);
        }*/

        //Entrou em área ou objeto recarregável
        if(collision.transform.CompareTag("Trigger_Recarregavel")){
            Jump.setJump(true);
            playFeedbackRecarga();
            return;
        }
        
        //Entrou em safe zone
        if (collision.transform.CompareTag("Trigger_Safe")){
            Timer.stopTimer();
            return;
        }

    }

    private void OnTriggerExit2D(Collider2D collision){

        //Saiu da safe zone
        if (collision.transform.CompareTag("Trigger_Safe")){
            Timer.startTimer();
            return;
        }
    }
    void damageParticleTrigger()
    {
        Instantiate(damageParticle,gameObject.transform.position,Quaternion.identity);

        //Adiciona ao contador de mortes
        //if (deathCounter.instance != null) {deathCounter.instance.addDeath();}
    }

    //Usado para deslizar em plataformas curvas
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
        if(HealthManager.instance.IsPlayerInvencible()) return;

        //particula de morte e contador de morte
        if (damageParticle != null)
            damageParticleTrigger();

        //camera shake
        if (CameraShake.instance != null) { CameraShake.instance.shakeCam(2, 1, 0.5f); }

        //tocar audio de dano
        playAudioLose();

        //perder vida
        HealthManager.instance.menosVida(10, 10);

        //Feedback de dano
        HurtFeedback.HurtTrigger();
    }

    void playFeedbackRecarga(){
        //funcao para camerashake ----------------------------> shakecam(intensidade,frequencia,tempo)
        if (CameraShake.instance != null) { CameraShake.instance.shakeCam(rb.velocity.magnitude/5,1, 0.2f); }

        //resetMaterial(collision);

        //instanciar particula de colisao com a parede
        ParticleSystem particula = Instantiate(paredeParticle,transform.position,Quaternion.identity).GetComponent<ParticleSystem>();
        if (rb.velocity.magnitude > 10000f){
            particula.startSpeed = rb.velocity.magnitude * 1.2f;
        }
    }

}
