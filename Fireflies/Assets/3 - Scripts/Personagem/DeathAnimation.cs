using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathAnimation : MonoBehaviour
{
    public HealthManager HealthManager;
    public lookAlongVelocity lookAlongVelocity;
    public scaleByVelocity scaleByVelocity;
    public GameObject fireParticle;
    
    private GameObject Base, Ears, Eyes;
    private Renderer rBase, rEars, rEyes;

    [Header("Velocidade da animação")]
    public float lerpTime;
    private float lerpControl = 0;

    private bool isDead = false;

    void Start()
    {   
        Ears = gameObject.transform.GetChild(0).transform.GetChild(0).gameObject;
        Base = gameObject.transform.GetChild(0).transform.GetChild(1).gameObject;
        Eyes = gameObject.transform.GetChild(0).transform.GetChild(2).gameObject;
    }

    void Update()
    {
        if (isDead)
        {
            Death();
        }
    }

    public void DeathAnimationTrigger()
    {
        isDead = true;
        lerpControl = 0;
        if (fireParticle != null)
            FireParticleTrigger();
    }

    void Death()
    {
        gameObject.GetComponent<TrailRenderer>().enabled = false; 
        lookAlongVelocity.Reset();
        scaleByVelocity.Reset();

        rEars = Ears.GetComponent<Renderer>();
        rBase = Base.GetComponent<Renderer>();
        rEyes = Eyes.GetComponent<Renderer>();

        rEars.material.SetFloat("_Visible", Mathf.Lerp(0f, 1f, lerpControl));
        rEars.material.SetFloat("_WindStr", 0.2f);

        rBase.material.SetFloat("_Visible", Mathf.Lerp(0f, 1f, lerpControl));
        rBase.material.SetFloat("_WindStr", 0.2f);

        rEyes.material.SetFloat("_Visible", Mathf.Lerp(0f, 1f, lerpControl));
        rEyes.material.SetFloat("_WindStr", 0.2f);

        if (lerpControl < 1)
        {
            lerpControl += Time.deltaTime/lerpTime;
        }
        else
        {
            rEars.material.SetFloat("_Visible", 0);
            rEars.material.SetFloat("_WindStr", 0);

            rBase.material.SetFloat("_Visible", 0);
            rBase.material.SetFloat("_WindStr", 0);

            rEyes.material.SetFloat("_Visible", 0);
            rEyes.material.SetFloat("_WindStr", 0);

            gameObject.GetComponent<TrailRenderer>().enabled = true; 

            isDead = false;
        }
    }

    private void FireParticleTrigger()
    {
        GameObject particle = (GameObject)Instantiate(fireParticle,gameObject.transform.position,Quaternion.identity);
    }
}
