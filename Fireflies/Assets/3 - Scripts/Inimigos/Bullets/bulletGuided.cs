using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletGuided : MonoBehaviour
{

    Vector3 dir;

    public GameObject player;

    [Header("Velocidade do projetil")]
    public int force = 20;

    bool canDestroy = false;

    [Header("Tempo ate projetil explodir (s)")]
    public int tempoAteExplodir = 5;

    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        if (GameObject.FindGameObjectWithTag("Player") != null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }
        else
        {
            Debug.Log("bala guiada nao achou player");
        }

        rb = GetComponent<Rigidbody2D>();

        //GetComponentInChildren<CircleCollider2D>().enabled = false;
        Invoke("activateCollider", 0.3f);

        //se autodestruir apos x segundos
        Destroy(gameObject, tempoAteExplodir);
    }

    // Update is called once per frame
    void Update()
    {
        //registrar posicao do player uma vez
        dir = player.transform.position - transform.position;

        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        //lookerGraphics.transform.rotation = Quaternion.Lerp(lookerGraphics.transform.rotation, Quaternion.AngleAxis(angle - 90, Vector3.forward),t);

        //transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);

        //Vector3 forceDir = Quaternion.AngleAxis(angle - 90, Vector3.forward).eulerAngles * 5;

        rb.AddForce(dir.normalized * force * Time.deltaTime * 100,ForceMode2D.Force);

        if(rb.velocity.magnitude > force){
             rb.velocity = Vector3.ClampMagnitude(rb.velocity, force);
         }

        //rotacionar projetil baseado em sua velocidade
        Vector2 v = rb.velocity;
        angle = Mathf.Atan2(v.y, v.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (canDestroy)
        {
            Destroy(gameObject);
        }
    }

    void activateCollider()
    {
        GetComponentInChildren<CircleCollider2D>().enabled = true;
        canDestroy = true;
    }
}
