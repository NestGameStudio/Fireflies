﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class ExtensionMethods
{

    public static float Remap(this float value, float from1, float to1, float from2, float to2)
    {
        return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
    }

}
public class BleeperBehaviour : MonoBehaviour
{
    /*
    Fica piscando entre duas cores, vermelho e verde.
    No caso da Cali acertar o inimigo enquanto ele está Verde, ele é derrotado.
    Caso Cali acerte ele enquanto ele está Vermelho, Cali perde Vida.
    */

    [Header("Debug Vars")]
    public Text timeDisplay;

    [Header("Ativa visualizacao de debug")]
    public bool Debug = false;

    [Header("Ativa efeito de contagem de tempo")]
    public bool Effect = true;

    public GameObject effectObject;

    public Color[] baseColors;
    public SpriteRenderer[] baseRenderer;
    public GameObject angy;

    [Header("Tempo(s) ate mudar de estado")]
    public float changeTime = 1;

    //variavel que guarda o changeTime logo no inicio do jogo
    private float timeBackup;

    [Header("Forca de impulso ate o player")]
    public float impulseForce = 2;

    private Rigidbody2D rb;

    public bool canViewPlayer = false;

    public enum estado
    {
        inatingivel,
        atingivel
    }

    [Header("Visualizacao do estado do inimigo")]
    public estado Estado;
    // Start is called before the first frame update
    void Start()
    {
        timeBackup = changeTime;

        rb = GetComponent<Rigidbody2D>();

        if (Estado == estado.inatingivel)
        {
            gameObject.tag = "Bleeper_Invulneravel";
        }
        else
        {
            gameObject.tag = "Bleeper_Vulneravel";
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (changeTime > 0)
        {
            changeTime -= Time.deltaTime;
        }
        else if(changeTime <= 0 && rb.velocity.magnitude <= 0.2f)
        {
            changeState();

        }

        //mostrar tempo faltante em display
        timeDisplay.text = (Mathf.Round(changeTime*10)/10).ToString();

        //mudar tamanho de objeto de efeito baseado no tempo faltante
        // min - 0.275 / max - 0.5
        float EffectSize = changeTime.Remap(0,timeBackup,0.275f,0.5f);

        effectObject.transform.localScale = new Vector3(EffectSize, EffectSize, 1);
    }
    void changeState()
    {
        //reseta timer
        changeTime = timeBackup;

        mudarCor();

        //colocar tag correspondente ao estado
        mudarTag();

        //alternar estado
        if (Estado == estado.inatingivel)
        {
            Estado = estado.atingivel;
        }
        else
        {
            Estado = estado.inatingivel;

            //se joga no player
            forceToPlayer();
        }

    }
    public void mudarCor()
    {
        //colocar cor correspondente ao estado
        for (int x = 0; x < baseRenderer.Length; x++)
        {
            baseRenderer[x].color = baseColors[(int)Estado];
        }

        if (Estado == estado.inatingivel)
        {
            angy.SetActive(true);

            
        }
        else
        {
            angy.SetActive(false);
        }

        

    }
    public void mudarTag()
    {

        if (Estado == estado.inatingivel)
        {
            gameObject.tag = "Bleeper_Vulneravel";
        }
        else
        {
            gameObject.tag = "Bleeper_Invulneravel";
        }
    }
    public void forceToPlayer()
    {
        //caso esteja vendo o player
        if (canViewPlayer)
        {
            //get player
            Vector3 playerPos = GameObject.FindGameObjectWithTag("Player").transform.position;

            rb.AddForce((playerPos - gameObject.transform.position) * impulseForce, ForceMode2D.Impulse);

            print("se jogou");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            //player esta dentro da visao
            canViewPlayer = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //player esta fora da visao
            canViewPlayer = false;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawLine(GameObject.FindGameObjectWithTag("Player").transform.position, transform.position);

        //Gizmos.color = Color.red;
        //Gizmos.DrawWireSphere(transform.position, 1);
    }
}

