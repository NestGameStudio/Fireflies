using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealCollectable : MonoBehaviour
{
    public GameObject healParticle;

    [Header("Quanto de vida recupera")]
    public int healValue;

    [Header("É animado?")]
    public bool animate = true;
    private Animator anim;

    [Header("Tempo para ligar o collider")]
    public int colliderTimer = 1;
    private Collider2D col;

    void Start()
    {
        anim = GetComponent<Animator>();
        col = GetComponent<Collider2D>();
        col.enabled = false;
        
        StartCoroutine(EnableCollider2D(colliderTimer));

        if (animate)
        {
            anim.enabled = true;
        } else {
            anim.enabled = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {

        if (collision.transform.CompareTag("Player")) 
        {
            HealthManager.instance.maisVida(healValue);
            if (healParticle != null)
                healParticleTrigger();
            Destroy(this.gameObject);
        }
    }

    void healParticleTrigger()
    {
        Instantiate(healParticle,gameObject.transform.position,Quaternion.identity);
    }

    private IEnumerator EnableCollider2D(int timer)
    {
        yield return new WaitForSeconds(timer);
        col.enabled = true;
    }

}
