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
    public int Damage;
    [Range(0,100)]
    public float CritChance;

    [Header("Invenciblidade")]
    public float invincibilityTime;

    [Header("Upgrades")]
    public int CommonWeight;
    public int RareWeight;
    public int EpicWeight;
    public int LegendaryWeight;


    public void IncreaseCritChance(float amount) {
        CritChance += amount;
    }

    public void IncreaseDamage(int amount ) {
        Damage += amount;
    }

    public void IncreaseInvicibility(float amount) {
        invincibilityTime += amount;
    }
}
