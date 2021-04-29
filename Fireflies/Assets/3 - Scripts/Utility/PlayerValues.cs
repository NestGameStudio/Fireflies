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
    public float rImpulseForce;
   
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
    public float rDamage;
    [SerializeField]
    [Range(0,100)]
    private float LifeSteal;
    [HideInInspector]
    public float rLifeSteal;
    [Range(0,100)]
    [SerializeField]
    private float CritChance;
    [HideInInspector]
    public float rCritChance;

    [Header("Invenciblidade")]
    [SerializeField]
    private float InvincibilityTime;
    [HideInInspector]
    public float rInvincibilityTime;
    [Header("Vida")]
    [SerializeField]
    private float MaxHealth;
    [HideInInspector]
    public float rMaxHealth;
    [Header("Quantidade de Pulos")]
    [SerializeField]
    private int MaxJumpCharges;
    [HideInInspector]
    public int rMaxJumpCharges;

    [Header("Upgrades")]
    public int CommonWeight;
    public int RareWeight;
    public int EpicWeight;
    public int LegendaryWeight;
    public List<Upgrade> upgrades;
    private List<Upgrade> AcquiredUpgrades;

    public void InitValues() {
        //Reseta valores do player para referência inicial (definida em Inspector)
        AcquiredUpgrades = new List<Upgrade>();
        rImpulseForce = ImpulseForce;
        rDamage = Damage;
        rLifeSteal = LifeSteal;
        rCritChance = CritChance;
        rInvincibilityTime = InvincibilityTime;
        rMaxHealth = MaxHealth;
        rMaxJumpCharges = MaxJumpCharges;
    }

    public Upgrade GetUpgradeAcquired(int i) {
        return AcquiredUpgrades[i];
    }

    public void AddUpgradeAcquired(Upgrade up) {
        AcquiredUpgrades.Add(up);
    }

    public int GetAcquiredUpgradesSize() {
        return AcquiredUpgrades.Count;
    }


    public void IncreaseCritChance(float amount) {
        rCritChance += amount;
        Debug.Log("Critico! Valor adicionado: " + amount.ToString("N2") + " | Valor atual: " + rCritChance.ToString("N2"));
    }

    public void IncreaseDamage(float amount) {
        rDamage += amount;
        Debug.Log("Dano! Valor adicionado: " + amount.ToString("N2") + " | Valor atual: " + rDamage.ToString("N2"));
    }

    public void IncreaseInvicibility(float amount) {
        rInvincibilityTime += amount;
        Debug.Log("Invencivel! Valor adicionado: " + amount.ToString("N2") + " | Valor atual: " + rInvincibilityTime.ToString("N2"));
    }

    public void IncreaseMaxHealth(float amount) {
        rMaxHealth += amount;
        HUDManager.instance.healthUI.SetMaxHealth(rMaxHealth);
        HealthManager.instance.maisVida(amount);
        Debug.Log("Vida! Valor adicionado: " + amount.ToString("N2") + " | Valor atual: " + rMaxHealth.ToString("N2"));
    }

    public void IncreaseLifeSteal(float amount) {
        rLifeSteal += amount;
        Debug.Log("LifeSteal! Valor adicionado: " + amount.ToString("N2") + " | Valor atual: " + rLifeSteal.ToString("N2"));
    }

    public void IncreaseImpulseForce(float amount) {
        rImpulseForce += amount;
        Debug.Log("Impulso! Valor adicionado: " + amount.ToString("N2") + " | Valor atual: " + rImpulseForce.ToString("N2"));
    }

    public void IncreaseMaxJumpCharges(float amount) {
        rMaxJumpCharges += (int) amount;
        HealthManager.instance.Player.GetComponent<CollisionCheck>().Jump.chargeUI.Setup(rMaxJumpCharges);
        Debug.Log("MaxPulo! Valor adicionado: " + amount.ToString("N2") + " | Valor atual: " + rMaxJumpCharges.ToString("N2"));
    }
}
