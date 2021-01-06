using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Looker_Behaviour : MonoBehaviour
{
    /*
    //A cada 2 segundos ele atualiza sua direção, olhando para a posição atual da Kali.
    //Para ser derrotado, Kali deve atacá-lo por trás. Caso Kali acerte ele enquanto ele está olhando para ela, Kali perde Vida.
    */

    [Header("Debug Vars")]
    public Text timeDisplay;

    public GameObject lookerGraphics;

    //tempo ate olhar para o player novamente
    public float changeTime = 2;

    //angulo de abertura de area vulneravel
    [Range(0,360)]
    public float angle = 60;

    public SpriteRenderer parteVulneravel;

    [Header("Ativa efeito de contagem de tempo")]
    public bool Effect = true;

    public GameObject effectObject;

    [Header("Ativa visualizacao de debug")]
    public bool Debug = false;

    public ArcCollider2D vulnerableCollider;

    private Transform player;

    //variavel que guarda o changeTime logo no inicio do jogo
    private float timeBackup;
    void Start()
    {
        timeBackup = changeTime;
        
        //procurar player por tag PLAYER - SEM ISSO O INIMIGO NAO VAI OLHAR PARA O JOGADOR
        if (GameObject.FindGameObjectWithTag("Player") != null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;

        }
        else
        {
            UnityEngine.Debug.LogWarning("Looker nao achou player", gameObject);
        }
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

        //mostrar tempo faltante em display
        timeDisplay.text = (Mathf.Round(changeTime * 10) / 10).ToString();

        //mudar tamanho de objeto de efeito baseado no tempo faltante
        // min - 0.275 / max - 0.5
        float EffectSize = changeTime.Remap(0, timeBackup, 0.275f, 0.5f);

        effectObject.transform.localScale = new Vector3(EffectSize, EffectSize, 1);
    }

    void lookAtPlayer()
    {
        //resetar timer
        changeTime = timeBackup;


        //olhar para o jogador
        Vector3 dir = player.position - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        lookerGraphics.transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);



        UnityEngine.Debug.Log("Looker olhou pro player",gameObject);
    }
}
