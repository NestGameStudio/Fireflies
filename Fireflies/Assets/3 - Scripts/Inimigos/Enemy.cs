using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Vida")]
    public float health = 100;

    [Header("Dano ao toque (mínimo/máximo)")]
    public Vector2 damage = new Vector2(5f, 10f);

    [Header("Drop")]
    [Range(0,100)]
    public float MoneyDropChance;
    public int MoneyQuantityMin;
    public int MoneyQuantityMax;
    [Range(0,100)]
    public float HealthDropChance;
    public GameObject HealthItem;

    [Header("Visual feedback")]
    public GameObject deathParticle;
    public GameObject damageParticle;
    public GameObject critParticle;
    public GameObject moneyParticle;
    public GameObject deadBody;

    [Header("BodyCollider")]
    public CircleCollider2D _col;

    [Header("VisionCollider")]
    public CircleCollider2D _vcol;

    private Rigidbody2D _rb;
    private bool onGround = false;
    private GameObject vulneravelObject;
    private bool critalDamage = false;

    void Start() {
        _rb = GetComponent<Rigidbody2D>();
        if (this.gameObject.name == "Enemy_Looker") {
            vulneravelObject = this.gameObject.transform.GetChild(0).GetChild(1).gameObject;
        }
    }

    public void TakeDamage(float damage)
    {
        PlayerValues values = Setup.Instance.PlayerValue;
        float dam;
        float rnd = Random.Range(0,100);
        if(values.rCritChance >= rnd) {
            dam = damage*3;
            critalDamage = true;
        }
        else {
            dam = damage;
            critalDamage = false;
        }
        if (critalDamage)
            {
                CritParticle();
            }
        DamageParticle();
        HealthManager.instance.maisVida(dam*values.rLifeSteal/100);
        if (health - dam <= 0){
            health = 0;
            Death();
        } else {  
            
            health -= dam;
        }

        
    }

    private void Death(){
        DropItem();
        MoneyParticle();
        EnemyDeadBody();
        SaveSystem.instance.Stats.EnemiesDefeated++;
        Destroy(gameObject);
    }

    private void DamageParticle() {
        if (damageParticle != null) Instantiate(damageParticle,gameObject.transform.position,Quaternion.identity);
    }

    private void CritParticle() {
        if (critParticle != null) Instantiate(critParticle,gameObject.transform.position,Quaternion.identity);
    }

    private void EnemyDeadBody() {
        if (damageParticle != null) Instantiate(deadBody,gameObject.transform.position,Quaternion.identity);
    }

    private void MoneyParticle() {
        if (moneyParticle != null) Instantiate(moneyParticle,gameObject.transform.position,Quaternion.identity);
    }

    private void DropItem() {
        float rnd = Random.Range(0,100);
        if(HealthDropChance > rnd) {
            Instantiate(HealthItem, new Vector2(transform.position.x,transform.position.y),Quaternion.identity);
        }
        rnd = Random.Range(0,100);
        if(MoneyDropChance > rnd) {
            int money = Random.Range(MoneyQuantityMin,MoneyQuantityMax);
            MoneyManager.instance.ganharDinheiro(money);
        }
    }
}
