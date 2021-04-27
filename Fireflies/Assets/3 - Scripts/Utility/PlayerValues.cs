using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class PlayerValues : ScriptableObject
{
    [Header("Pulo")]
    [Space(0.3f)]
    [SerializeField]
    private float ImpulseForce = 0.78f;
    [HideInInspector]
    public float ForcaImpulso;
   
    [Tooltip("O tamanho máximo afeta o impulso final da Cali")]
    public float LineMaxRadius = 3.0f;
    [Tooltip("O tamanho mínimo afeta o impulso final da Cali")]
    public float LineMinRadius = 1.5f;
    // ------------------- Time slow -------------------------
    [Header("Time Slow")]
    [Space(0.3f)]

    [Range(0.0f, 1.0f)] 
    public float TimeSlow = 0.02f;

    // ------------------- Gamepad -------------------------
    [Header("Gamepad")]
    [Space(0.3f)]

    [Range(1, 10)] public float GamepadSensibility = 2.5f;
    public float JumpDelay = 0.1f;

    [Header("Dano")]
    [SerializeField]
    private float Damage;
    [HideInInspector]
    public float Dano;
    [SerializeField]
    private float LifeSteal;
    [HideInInspector]
    public float LiSteal;
    [Range(0,100)]
    [SerializeField]
    private float CritChance;
    [HideInInspector]
    public float CriticalChance;

    [Header("Invenciblidade")]
    [SerializeField]
    private float invincibilityTime;
    [HideInInspector]
    public float invinciTime;
    [Header("Vida")]
    [SerializeField]
    private float MaxHealth;
    [HideInInspector]
    public float MaxHp;
    [Header("Quantidade de Pulos")]
    [SerializeField]
    private int MaxJumpCharges;
    [HideInInspector]
    public int MaxJump;

    [Header("Upgrades")]
    public int CommonWeight;
    public int RareWeight;
    public int EpicWeight;
    public int LegendaryWeight;
    public List<Upgrade> upgrades;

    public void InitValues() {
        ForcaImpulso = ImpulseForce;
        Dano = Damage;
        LiSteal = LifeSteal;
        CriticalChance = CritChance;
        invinciTime = invincibilityTime;
        MaxHp = MaxHealth;
        MaxJump = MaxJumpCharges;
    }


    public void IncreaseCritChance(float amount, int cost) {
        CriticalChance += amount;
        Debug.Log("Critico!");
    }

    public void IncreaseDamage(float amount, int cost) {
        Dano += amount;
        Debug.Log("Dano!");
    }

    public void IncreaseInvicibility(float amount, int cost) {
        invinciTime += amount;
        Debug.Log("Invencivel!");
    }

    public void IncreaseMaxHealth(float amount, int cost) {
        MaxHp += amount;
        HUDManager.instance.healthUI.SetMaxHealth(MaxHp);
        HealthManager.instance.maisVida(amount);
        Debug.Log("Vida!");
    }

    public void IncreaseLifeSteal(float amount, int cost) {
        LiSteal += amount;
        Debug.Log("LifeSteal!");
    }

    public void IncreaseImpulseForce(float amount, int cost) {
        ForcaImpulso += amount;
        Debug.Log("Impulso!");
    }

    public void IncreaseMaxJumpCharges(float amount, int cost) {
        MaxJump += (int) amount;
        HealthManager.instance.Player.GetComponent<CollisionCheck>().Jump.chargeUI.Setup(MaxJump);
        Debug.Log("MaxPulo!");
    }
}
