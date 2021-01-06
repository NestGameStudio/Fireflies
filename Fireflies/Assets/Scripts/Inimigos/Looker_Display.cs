using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Looker_Display : MonoBehaviour
{
    public Looker_Behaviour behaviourScript;

    public GameObject debugObject;

    public GameObject effectObject;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float newAngle = 180 - behaviourScript.angle / 2;

        behaviourScript.parteVulneravel.sharedMaterial.SetFloat("_Arc1", newAngle);
        behaviourScript.parteVulneravel.sharedMaterial.SetFloat("_Arc2", newAngle);

        //visualizacao de debug
        if (behaviourScript.Debug)
        {
            debugObject.SetActive(true);
        }
        else
        {
            debugObject.SetActive(false);
        }

        //visualizacao de efeito
        if (behaviourScript.Effect)
        {
            effectObject.SetActive(true);
        }
        else
        {
            effectObject.SetActive(false);
        }
    }
}
