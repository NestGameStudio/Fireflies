using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityBarrel : Entity {

    public SpriteRenderer renderer;
    public float hp = 10;
    private float maxHp = 10;
    public int reward = 10;
    public GameObject breakableParticle;
    public GameObject rewardParticle;

    // Start is called before the first frame update
    void Start()
    {
        if(renderer == null) GetComponentInChildren<SpriteRenderer>();

        renderer.sharedMaterial = new Material(renderer.sharedMaterial);

        maxHp = hp;
        renderer.sharedMaterial.SetFloat("_DissolveAmount", hp/maxHp);
    }

    // Sobrescreve o método com as informações adequadas
    public override void Initialize(string name) {
        base.Initialize(name);

        // Vincular ao coin sistem que o DJ fez (spawnnar moedas quando quebra)
    }

    public void OnCollisionEnter2D(Collision2D other){
        if(other.gameObject.CompareTag("Player")){
            //Player bateu neste objeto

            hp--; //Perde 1 hp
            renderer.sharedMaterial.SetFloat("_DissolveAmount", hp/maxHp); //valor de 0 a 1 de quão destruído

            if(hp <= 0){
                //objeto deverá dar recompensa a player e ser destruído
                
                MoneyManager.instance.ganharDinheiro(reward);

                //Instancia partícula
                if(rewardParticle != null) Instantiate(rewardParticle, this.transform.position, Quaternion.identity);
                if(breakableParticle != null) Instantiate(breakableParticle, this.transform.position, Quaternion.identity);
                if(gameObject.CompareTag("Treasure")) {
                    other.gameObject.GetComponent<CollisionCheck>().LigaSkillUI();
                }

                Destroy(this.gameObject); //Destrói este objeto
            } 
        }
    }
}
