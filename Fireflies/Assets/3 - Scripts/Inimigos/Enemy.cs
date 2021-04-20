﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Vida")]
    public int health = 100;

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

    [Header("BodyCollider")]
    public CircleCollider2D _col;

    [Header("VisionCollider")]
    public CircleCollider2D _vcol;

    private Rigidbody2D _rb;
    private bool onGround = false;
    private GameObject vulneravelObject;

    void Start() {
        _rb = GetComponent<Rigidbody2D>();
        if (this.gameObject.name == "Enemy_Looker") {
            vulneravelObject = this.gameObject.transform.GetChild(0).GetChild(1).gameObject;
        }
    }

    public void TakeDamage(int damage)
    {
        if (health - damage <= 0){
            health = 0;
            DropBody();
        } else {
            DamageParticle();
            health -= damage;
        }
    }

    private void Death(){
        DropItem();
        DeathParticle();
        SaveSystem.instance.Stats.EnemiesDefeated++;
        Destroy(gameObject);
    }

    private void DeathParticle(){
        if (deathParticle != null) Instantiate(deathParticle,gameObject.transform.position,Quaternion.identity);
    }

    private void DamageParticle(){
        if (damageParticle != null) Instantiate(damageParticle,gameObject.transform.position,Quaternion.identity);
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

    private void DropBody() {
        if (vulneravelObject != null) {
            vulneravelObject.SetActive(false);
        }
        _vcol.enabled = false;
        _col.isTrigger = true;
        _rb.bodyType = RigidbodyType2D.Dynamic;
        _rb.drag = 2;
        _rb.gravityScale = 2;
    }
    /*
    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag != "Player" && other.gameObject.tag != "Inimigo") {
            onGround = true;
        }
    }
    void OnTriggerStay2D(Collider2D other) {
        if (other.gameObject.tag != "Player" && other.gameObject.tag != "Inimigo") {
            onGround = true;
            if (health <= 0) {
                Death();
            }
        }
    }
    void OnTriggerExit2D(Collider2D other) {
        if (other.gameObject.tag != "Player" && other.gameObject.tag != "Inimigo") {
            onGround = false;
        } 
    }
    */
}
