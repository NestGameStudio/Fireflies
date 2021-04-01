using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerigoAtirador : MonoBehaviour
{
    public GameObject tiroReto;
    public GameObject tiroGuiado;

    [Header("Forca de propulsao do tiro")]
    public float tiroForca = 2;

    [Header("Taxa de repeticao de tiro (s)")]
    public float repeatTime = 1;
    public float changeTime = 1;

    [HideInInspector]
    public enum type
    {
        reto,
        guiado
    }

    public type tipoTiro;
    public GameObject spawnTiro;
    public Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        if(anim==null) GetComponentInChildren<Animator>();
        anim.SetFloat("rate", 1/repeatTime);

        if(spawnTiro==null) transform.Find("BulletSpawn");

        changeTime = repeatTime;
        //InvokeRepeating("atirar",0,repeatTime);
    }

    public void Update(){
        if (changeTime > 0){
            changeTime -= Time.deltaTime;
        } else {
            atirar();
            changeTime = repeatTime;
        }
    }

    public void atirar(){
        Debug.Log("Turret atira");

        //atirar bala reta
        if (tipoTiro == type.reto)
        {
            GameObject bala = Instantiate(tiroReto, spawnTiro.transform.position, Quaternion.identity);
            //bala.GetComponent<Rigidbody2D>().AddForce((player.transform.position - lookerGraphics.transform.position).normalized * tiroForca,ForceMode2D.Impulse);
            bala.GetComponent<Rigidbody2D>().AddForce(transform.right * tiroForca, ForceMode2D.Impulse);
        }
        //atirar bala curva
        else if (tipoTiro == type.guiado)
        {
            GameObject bala = Instantiate(tiroGuiado, spawnTiro.transform.position, Quaternion.identity);

            bala.GetComponent<Rigidbody2D>().AddForce(transform.right * tiroForca * 1.5f, ForceMode2D.Impulse);
        }
    }
}
