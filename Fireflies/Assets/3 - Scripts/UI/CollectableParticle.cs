using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableParticle : MonoBehaviour
{
    public GameObject collectableParticle;
    public Color color;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision != null)
        {
            if(collision.gameObject.tag == "Player")
            {
                if (collectableParticle != null)
                    CollectableParticleTrigger();
            }
        }
    }

    private void CollectableParticleTrigger()
    {
        GameObject particle = (GameObject)Instantiate(collectableParticle,gameObject.transform.position,Quaternion.identity);
        particle.GetComponent<ParticleSystem>().startColor = color;
    }
}
