using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Looker_Behaviour : MonoBehaviour
{
    /*
    //A cada 2 segundos ele atualiza sua direção, olhando para a posição atual da Kali.
    //Para ser derrotado, Kali deve atacá-lo por trás. Caso Kali acerte ele enquanto ele está olhando para ela, Kali perde Vida.
    */

    //tempo ate olhar para o player novamente
    public float changeTime = 2;

    //angulo de abertura de area vulneravel
    [Range(0,360)]
    public float angle = 60;

    public SpriteRenderer parteVulneravel;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (changeTime > 0)
        {
            changeTime -= Time.deltaTime;
        }
        else
        {
            lookAtPlayer();

        }
    }

    void lookAtPlayer()
    {

    }
}
