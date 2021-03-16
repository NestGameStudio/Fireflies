using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityBarrel : Entity {

    public Animator anim;
    public float hp = 10;
    private float maxHp = 10;
    public int reward = 10;
    public GameObject rewardParticle;

    // Start is called before the first frame update
    void Start()
    {
        if(anim == null) GetComponentInChildren<Animator>();
        maxHp = hp;
    }

    // Sobrescreve o método com as informações adequadas
    public override void Initialize(string name) {
        base.Initialize(name);

        // Vincular ao coin sistem que o DJ fez (spawnnar moedas quando quebra)
    }

    public void OnCollisionEnter(Collision other){
        if(other.gameObject.CompareTag("Player")){
            //Player bateu neste objeto

            hp--; //Perde 1 hp
            anim.SetFloat("hp", hp/maxHp); //valor de 0 a 1 de quão destruído

            if(hp <= 0){
                //objeto deverá dar recompensa a player e ser destruído
                MoneyManager money = other.gameObject.GetComponent<MoneyManager>();
                money.ganharDinheiro(reward);

                //Instancia partícula
                if(rewardParticle != null) Instantiate(rewardParticle, this.transform.position, Quaternion.identity);

                Destroy(this); //Destrói este objeto
            } 
        }
    }
}
