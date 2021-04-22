using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class PlayerValues : ScriptableObject
{
    [Header("Pulo")]
    [Space(0.3f)]

    public float ImpulseForce = 0.78f;
   
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
    public float Damage;
    public float LifeSteal;
    [Range(0,100)]
    public float CritChance;

    [Header("Invenciblidade")]
    public float invincibilityTime;
    [Header("Vida")]
    public float MaxHealth;

    [Header("Upgrades")]
    public int CommonWeight;
    public int RareWeight;
    public int EpicWeight;
    public int LegendaryWeight;


    public void IncreaseCritChance(float amount, int cost) {
        if(MoneyManager.instance.money < cost) {
            // diz que nao tem dinheiro
            return;
        }
        MoneyManager.instance.money -= cost;
        CritChance += amount;
        Debug.Log("Critico!");
    }

    public void IncreaseDamage(float amount, int cost) {
        if(MoneyManager.instance.money < cost) {
            // diz que nao tem dinheiro
            return;
        }
        MoneyManager.instance.money -= cost;
        Damage += amount;
        Debug.Log("Dano!");
    }

    public void IncreaseInvicibility(float amount, int cost) {
        if(MoneyManager.instance.money < cost) {
            // diz que nao tem dinheiro
            return;
        }
        MoneyManager.instance.money -= cost;
        invincibilityTime += amount;
        Debug.Log("Invencivel!");
    }

    public void IncreaseMaxHealth(float amount, int cost) {
        if(MoneyManager.instance.money < cost) {
            // diz que nao tem dinheiro
            return;
        }
        MoneyManager.instance.money -= cost;
        MaxHealth += amount;
        HealthManager.instance.maisVida(amount);
        Debug.Log("Vida!");
    }

    public void IncreaseLifeSteal(float amount, int cost) {
        if(MoneyManager.instance.money < cost) {
            // diz que nao tem dinheiro
            return;
        }
        MoneyManager.instance.money -= cost;
        LifeSteal += amount;
        Debug.Log("LifeSteal!");
    }

    public void IncreaseImpulseForce(float amount, int cost) {
        if(MoneyManager.instance.money < cost) {
            // diz que nao tem dinheiro
            return;
        }
        MoneyManager.instance.money -= cost;
        ImpulseForce += amount;
        Debug.Log("Impulso!");
    }
}
