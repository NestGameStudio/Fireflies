using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    public static HealthManager instance { get; private set; }

    [Header("Vida do player")]
    public int health = 100;

    [Header("Maximo de vida do player")]
    public int maxHealth = 100;
    private HUDManager hudUI;

    [Header("Morte")]
    public DeathAnimation DeathAnimation;
    public int DeathWaitTime;

    [Header("Invencibilidade")]
    public float InvencibilityDuration;
    private bool IsInvencible = false;

    private void Awake()
    {
        // Singleton
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
    }

    private void Start(){
        //guarda referência para singleton de HUD Manager
        hudUI = HUDManager.instance;
        if(hudUI != null){
            hudUI.healthUI.SetupUI(maxHealth);
            hudUI.healthUI.SetMaxHealth(maxHealth);
        } 
        else{
            Debug.Log("Não há nenhum objeto com HUD Manager em cena");
        }
    }

    //perder vida por x quantidade, definindo um minimo e maximo de dano
    public void menosVida(int danoMin, int danoMax)
    {
        int quantidade = Random.Range(danoMin, danoMax + 1);
        
        if (health - quantidade > 0)
        {
            //perdeu vida
            health -= quantidade;
            hudUI.healthUI.SetHealth(health);
            StartCoroutine(InvencibilidadeTimer());
        }
        else
        {
            //morreu
            health = 0;
            hudUI.healthUI.SetHealth(health);
            morreu();
        }
    }

    //ganhar vida por x quantidade
    public void maisVida(int quantidade)
    {
        if(health + quantidade > maxHealth)
        {
            //limitar a vida pelo maximo
            health = maxHealth;
        }
        else
        {
            health += quantidade;
        }
        hudUI.healthUI.SetHealth(health);
    }

    //aumentar limite de vida por x quantidade, aumentar vida?
    public void aumentarLimite(int quantidade, bool aumentarVida)
    {
        maxHealth += quantidade;
        hudUI.healthUI.SetMaxHealth(maxHealth);
        
        //caso esteja true, completar vida 
        if (aumentarVida)
        {
            maisVida(quantidade);
        }
    }

    //diminuir limite de vida por x quantidade
    public void diminuirLimite(int quantidade)
    {
        maxHealth -= quantidade;
        hudUI.healthUI.SetMaxHealth(maxHealth);

        //perder vida caso a vida esteja dentro do range que sera perdido
        if(health > maxHealth)
        {
            health = maxHealth;
        }
    }

    //especificar um limite de vida, completar vida?
    public void setLimite(int limite, bool completar)
    {
        maxHealth = limite;
        hudUI.healthUI.SetMaxHealth(maxHealth);

        if (completar)
        {
            health = maxHealth;
        }
    }

    //morreu
    public void morreu()
    {  
        StartCoroutine(DeathWait());
        gameObject.GetComponent<Rigidbody2D>().simulated = false;
        DeathAnimation.DeathAnimationTrigger();
        
    }   

    private IEnumerator DeathWait()
    {
        yield return new WaitForSeconds(DeathWaitTime);
        Respawn.instance.RepositionPlayer();
        health = maxHealth;
        hudUI.healthUI.SetHealth(health);
        gameObject.GetComponent<Rigidbody2D>().simulated = true;
        LevelManagerRL.Instance.ChooseNewMap();
    }

    private IEnumerator InvencibilidadeTimer() {
        IsInvencible = true;
        yield return new WaitForSecondsRealtime(InvencibilityDuration);
        IsInvencible = false;
    }

    public bool IsPlayerInvencible() {
        return IsInvencible;
    }


}
