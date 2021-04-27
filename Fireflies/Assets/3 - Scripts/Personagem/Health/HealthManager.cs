using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    public static HealthManager instance { get; private set; }

    [Header("Vida do player")]
    public GameObject Player;
    private Animator playerAnim;
    public float health = 100;
    private HUDManager hudUI;

    [Header("Morte")]
    public DeathAnimation DeathAnimation;
    private CameraZoom CameraZoom;
    public int DeathWaitTime;
    [Tooltip("Precisa dele pra desligar o slowmotion e habilitar a movimentacao do mouse")]
    public GameObject Input;

    private bool IsInvencible = false;

    private PlayerValues playervalue;
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
        playervalue = Setup.Instance.PlayerValue;
        playervalue.InitValues();
        hudUI = HUDManager.instance;
        health = playervalue.MaxHp;
        if(hudUI != null){
            hudUI.healthUI.SetupUI(playervalue.MaxHp);
            hudUI.healthUI.SetMaxHealth(playervalue.MaxHp);
        }else{
            Debug.Log("Não há nenhum objeto com HUD Manager em cena");
        }
        //CameraZoom script
        CameraZoom = GameObject.Find("2D Cam_RogueLike").GetComponent<CameraZoom>();
        playerAnim = Player.transform.GetChild(0).GetComponent<Animator>();
        playerAnim.SetFloat("invincibilityTime", 1/playervalue.invinciTime);
    }

    //perder vida por x quantidade, definindo um minimo e maximo de dano
    public void menosVida(float dano)
    {      
        if (health - dano > 0)
        {
            //perdeu vida
            health -= dano;
            hudUI.healthUI.SetHealth(health);
            StartCoroutine(InvencibilidadeTimer());
        }
        else
        {
            //morreu
            health = 0;
            hudUI.healthUI.SetHealth(health);
            death();
        }
    }

    //ganhar vida por x quantidade
    public void maisVida(float quantidade)
    {
        if(health + quantidade > playervalue.MaxHp)
        {
            //limitar a vida pelo maximo
            health = playervalue.MaxHp;
        }
        else
        {
            health += quantidade;
        }
        hudUI.healthUI.SetHealth(health);
    }

    public void death()
    {  
        StartCoroutine(DeathWait());
        CameraZoom.DeathZoomTrigger();
        DeathAnimation.DeathAnimationTrigger();
        Player.gameObject.GetComponent<Rigidbody2D>().simulated = false;
        SaveSystem.instance.Stats.AttemptCount++;
        SaveSystem.instance.Stats.MoneyCount = MoneyManager.instance.money;
        SaveSystem.instance.Stats.RunTime = TimerManager.instance.time;
    }   

    private IEnumerator DeathWait()
    {
        yield return new WaitForSeconds(DeathWaitTime);
        //Respawn.instance.RepositionPlayer();
        health = playervalue.MaxHp;
        hudUI.healthUI.SetHealth(health);
        Player.gameObject.GetComponent<Rigidbody2D>().simulated = true;
        GameOverScreen();
    }

    private IEnumerator InvencibilidadeTimer() {
        IsInvencible = true;
        playerAnim.SetBool("invincible", IsInvencible);
        yield return new WaitForSeconds(playervalue.invinciTime);
        IsInvencible = false;
        playerAnim.SetBool("invincible", IsInvencible);
    }

    public bool IsPlayerInvencible() {
        return IsInvencible;
    }

    private void GameOverScreen() {
        FreezePlayer();
        GameStats stats = SaveSystem.instance.Stats; 
        hudUI.GameOverStats.SetActive(true);
        hudUI.JumpText.text = "Jumps performed: " + stats.JumpCount.ToString();
        hudUI.MoneyText.text = "Money gathered:     " + stats.MoneyCount.ToString();
        hudUI.EnemiesText.text = "Enemies defeated: " + stats.EnemiesDefeated.ToString();
        hudUI.AttemptText.text = "Attempt #" + stats.AttemptCount.ToString();
        hudUI.TimeText.text = "Run time: " + GetConvertedTime(stats.RunTime);
    }

    private string GetConvertedTime(float time) {
        int hours = Mathf.FloorToInt(time / 3600F);
		int minutes = Mathf.FloorToInt((time % 3600)/60);
		int seconds = Mathf.FloorToInt(time % 60);

        return string.Format("{0:00}:{1:00}:{2:00}", hours, minutes,seconds);
    }

    public void FreezePlayer() {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        HealthManager.instance.Player.GetComponent<CollisionCheck>().Jump.setJump(false);
        Time.timeScale = 0f;
        Input.GetComponent<ControlManager>().enabled = false;
    }

    public void UnFreezePlayer() {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        HealthManager.instance.Player.GetComponent<CollisionCheck>().Jump.setJump(true);
        Time.timeScale = 1f;
        Input.GetComponent<ControlManager>().enabled = true;
    }
}
