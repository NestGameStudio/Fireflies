using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Bleeper_DisplayColor : MonoBehaviour
{
    //Esse script faz algumas alteracoes em editor, sao elas: a visualizacao de debug, de efeito e de mudanca de cor

    public BleeperBehaviour behaviourScript;

    public GameObject debugObject;

    public GameObject effectObject;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {


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
