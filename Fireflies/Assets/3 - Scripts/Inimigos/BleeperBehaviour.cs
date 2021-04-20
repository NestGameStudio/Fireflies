using System.Collections;
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

[RequireComponent(typeof(Enemy))]
public class BleeperBehaviour : MonoBehaviour
{
    /*
    Fica piscando entre duas cores, vermelho e verde.
    No caso da Cali acertar o inimigo enquanto ele está Verde, ele é derrotado.
    Caso Cali acerte ele enquanto ele está Vermelho, Cali perde Vida.
    */
    [HideInInspector]
    public Enemy enemy;

    [Header("Debug Vars")]
    public Text timeDisplay;
    public Text healthDisplay;

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

    [Header("É afetado pelo slow motion?")]
    public bool scaledTime = false;

    //variavel que guarda o changeTime logo no inicio do jogo
    private float timeBackup;

    [Header("Forca de impulso ate o player")]
    public float impulseForce = 2;

    private Rigidbody2D rb;

    public bool canViewPlayer = false;

    public bool raycastCanViewPlayer = false;
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
        enemy = GetComponent<Enemy>();
        timeBackup = changeTime;

        rb = GetComponent<Rigidbody2D>();

        changeState();
    }

    // Update is called once per frame
    void Update()
    {
        if (changeTime > 0)
        {
            changeTime -= scaledTime? Time.deltaTime : Time.unscaledDeltaTime;
        }
        else if(changeTime <= 0 )
        {
            changeState();
        }

        //mostrar tempo faltante em display
        timeDisplay.text = (Mathf.Round(changeTime*10)/10).ToString();
        //mostrar vida em display
        healthDisplay.text = ("HP: " + enemy.health).ToString();

        //mudar tamanho de objeto de efeito baseado no tempo faltante
        // min - 0.275 / max - 0.5
        float EffectSize = changeTime.Remap(0,timeBackup,0.275f,0.5f);

        effectObject.transform.localScale = new Vector3(EffectSize, EffectSize, 1);
    }
    void changeState()
    {
        //reseta timer
        changeTime = timeBackup;

        //alternar estado
        if (Estado == estado.inatingivel)
        {
            Estado = estado.atingivel;
            gameObject.tag = "Inimigo_Vulneravel";
        }
        else
        {
            Estado = estado.inatingivel;
            gameObject.tag = "Inimigo";

            //se joga no player
            forceToPlayer();
        }

        mudarCor();

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

    public void forceToPlayer()
    {
        RaycastView();

        //caso esteja vendo o player
        if (canViewPlayer && raycastCanViewPlayer)
        {
            //get player
            Vector3 playerPos = GameObject.FindGameObjectWithTag("Player").transform.position;

            Vector2 forceDirection = (playerPos - gameObject.transform.position);

            rb.AddForce(forceDirection.normalized * impulseForce, ForceMode2D.Impulse);

            //print("se jogou");
        }
    }

    //Ver se player esta dentro do raio de visao
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            //player esta dentro da visao
            canViewPlayer = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //player esta fora da visao
            canViewPlayer = false;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        if(canViewPlayer) {
            Gizmos.DrawLine(GameObject.FindGameObjectWithTag("Player").transform.position, transform.position);
        }
        //Gizmos.color = Color.red;
        //Gizmos.DrawWireSphere(transform.position, 1);
    }

    //player transform cache
    private Transform playerTransform = null;
    void RaycastView()
    {
        //Length of the ray
        float laserLength = 50f;
        Vector2 startPosition = (Vector2)transform.position;
        int layerMask = LayerMask.GetMask("Default", "Passable");

        if(!playerTransform)
        {
            playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        }
        //get player position
        Vector3 playerPos = playerTransform.position;

        //Get the first object hit by the ray
        RaycastHit2D hit = Physics2D.Raycast(startPosition, (playerPos - gameObject.transform.position), laserLength, layerMask, 0);

        //If the collider of the object hit is not NUll
        if (hit.collider.gameObject.CompareTag("Player"))
        {
            //Hit something, print the tag of the object
            UnityEngine.Debug.Log("Hitting: " + hit.collider.tag);

            //Method to draw the ray in scene for debug purpose
            UnityEngine.Debug.DrawRay(startPosition, (playerPos - gameObject.transform.position) * laserLength, Color.green);
            raycastCanViewPlayer = true;
        }
        else{
            UnityEngine.Debug.DrawRay(startPosition, (playerPos - gameObject.transform.position) * laserLength, Color.red);
            raycastCanViewPlayer = false;
        }
    }

    //detect dano
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && Estado == estado.atingivel)
        {
            enemy.TakeDamage(10);
        }
    }
}

