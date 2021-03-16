using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealCollectable : MonoBehaviour
{
    private HealthManager healthManager;
    public GameObject healParticle;

    [Header("Quanto de vida recupera")]
    public int healValue;

    void Start()
    {
        healthManager = GameObject.FindWithTag("Player").GetComponent<HealthManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision) {

        if (collision.transform.CompareTag("Player")) 
        {
            healthManager.maisVida(healValue);
            if (healParticle != null)
                healParticleTrigger();
            Destroy(this.gameObject);
        }
    }

    void healParticleTrigger()
    {
        Instantiate(healParticle,gameObject.transform.position,Quaternion.identity);
    }
}
