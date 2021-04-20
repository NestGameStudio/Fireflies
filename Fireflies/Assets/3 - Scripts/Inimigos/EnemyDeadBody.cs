using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeadBody : MonoBehaviour
{
    [Header("Visual feedback")]
    public GameObject deathParticle;

    private void DeathParticle(){
        if (deathParticle != null) Instantiate(deathParticle,gameObject.transform.position,Quaternion.identity);
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.transform.CompareTag("Plataforma_Recarregavel") || other.transform.CompareTag("Perigo")) {
            DeathParticle();
            Destroy(gameObject);
        }
    }
}
