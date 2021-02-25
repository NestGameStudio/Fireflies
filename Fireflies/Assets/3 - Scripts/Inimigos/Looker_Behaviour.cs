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

    [Header("Vida")]
    public int health = 100;

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

    private bool isChangingRot = false;

    float t = 0;
    //variavel que guarda o changeTime logo no inicio do jogo
    private float timeBackup;
    private bool oneCheck = true;

    Vector3 dir;

    [HideInInspector]
    public enum bulletType
    {
        reto,
        guiado
    }

    [Header("Tipo de bala a ser atirada")]
    public bulletType tipoDeBala;

    [Header("Forca do tiro")]
    public float tiroForca = 2;

    [Header("Projetil a ser atirado em linha reta")]
    public GameObject bulletStraight;

    [Header("Projetil a ser atirado em curva/guiado")]
    public GameObject bulletGuided;

    //Se o player esta dentro do raio de visao
    public bool canViewPlayer = false;

    //variaveis abaixo sao para o raycast

    public bool raycastCanViewPlayer = false;

    //para assegurar que o looker so vai atirar uma vez
    private bool oneCheckAtirar = true;

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

        if (changeTime > 0 && isChangingRot == false)
        {
            changeTime -= Time.deltaTime;
        }
        else if(changeTime <= 0 || isChangingRot)
        {
            if (oneCheck)
            {
                oneCheck = false;
                t = 0;

                //registrar posicao do player uma vez
                dir = player.position - transform.position;
            }

            if (canViewPlayer)
            {
                lookAtPlayer();
            }
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
        
        //acionando variavel de mudanca de rotacao
        isChangingRot = true;
        
        
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        //lookerGraphics.transform.rotation = Quaternion.Lerp(lookerGraphics.transform.rotation, Quaternion.AngleAxis(angle - 90, Vector3.forward),t);

        lookerGraphics.transform.rotation = Damp(lookerGraphics.transform.rotation, Quaternion.AngleAxis(angle - 90, Vector3.forward), 0.1f, t);

        if (t > 1)
        {
            oneCheck = true;
            isChangingRot = false;

            if (player != null)
            {
                atirar();
            }
        }
        else
        {
            t += Time.deltaTime * 2;

            oneCheckAtirar = true;
        }
        //UnityEngine.Debug.Log("Looker olhou pro player",gameObject);
    }
    //desenhar linha ate o player registrado
    private void OnDrawGizmos()
    {
        if (player != null)
        {
            Gizmos.DrawLine(lookerGraphics.transform.position, player.position);
        }
    }
    //correcao do lerp - https://gamedevbeginner.com/the-right-way-to-lerp-in-unity-with-examples/
    public static Quaternion Damp(Quaternion source, Quaternion target, float smoothing, float dt)
    {
        return Quaternion.Lerp(source, target, 1 - Mathf.Pow(smoothing, dt));
    }

    public void morreu()
    {
        Destroy(gameObject);
    }

    public void atirar()
    {
        if (oneCheckAtirar)
        {
            oneCheckAtirar = false;

            RaycastView();

            if (raycastCanViewPlayer)
            {
                UnityEngine.Debug.Log("Looker atirou");

                //atirar bala reta
                if (tipoDeBala == bulletType.reto)
                {
                    GameObject bala = Instantiate(bulletStraight, transform.position, Quaternion.identity);
                    //bala.GetComponent<Rigidbody2D>().AddForce((player.transform.position - lookerGraphics.transform.position).normalized * tiroForca,ForceMode2D.Impulse);
                    bala.GetComponent<Rigidbody2D>().AddForce(lookerGraphics.transform.up * tiroForca, ForceMode2D.Impulse);
                }
                //atirar bala curva
                else if (tipoDeBala == bulletType.guiado)
                {
                    GameObject bala = Instantiate(bulletGuided, transform.position, Quaternion.identity);

                    bala.GetComponent<Rigidbody2D>().AddForce(lookerGraphics.transform.up * tiroForca * 1.5f, ForceMode2D.Impulse);
                }
            }
        }
    }

    //Ver se player esta dentro do raio de visao

    /*
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //player esta dentro da visao
            canViewPlayer = true;
        }
    }
    */
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
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

    //perder vida por x quantidade, definindo um minimo e maximo de dano
    public void perderVida(int danoMin, int danoMax)
    {
        int quantidade = Random.Range(danoMin, danoMax + 1);

        if (health - quantidade < 0)
        {
            //morreu
            morreu();
        }
        else
        {
            health -= quantidade;
        }
    }

    void RaycastView()
    {
        //Length of the ray
        float laserLength = 50f;
        Vector2 startPosition = (Vector2)transform.position;
        int layerMask = LayerMask.GetMask("Default");

        //get player
        Vector3 playerPos = GameObject.FindGameObjectWithTag("Player").transform.position;

        //Get the first object hit by the ray
        RaycastHit2D hit = Physics2D.Raycast(startPosition, (playerPos - gameObject.transform.position), laserLength, layerMask, 0);

        //If the collider of the object hit is not NUll
        if (hit.collider.gameObject.tag == "Player")
        {
            //Hit something, print the tag of the object
            UnityEngine.Debug.Log("Hitting: " + hit.collider.tag);

            //Method to draw the ray in scene for debug purpose
            UnityEngine.Debug.DrawRay(startPosition, (playerPos - gameObject.transform.position) * laserLength, Color.green);
            raycastCanViewPlayer = true;
        }
        else
        {
            UnityEngine.Debug.DrawRay(startPosition, (playerPos - gameObject.transform.position) * laserLength, Color.red);
            raycastCanViewPlayer = false;
        }
    }
}
