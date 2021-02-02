using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerigoMovel : MonoBehaviour
{
    //1. 
    [Header("Nao mexer")]
    public Transform pointA;
    public Transform pointB;
    public GameObject parteMovel;

    private float t = 0;

    //2. 
    [Header("Velocidade do Perigo")]
    [Range(0f, 3f)]
    public float velocidade = 1;

    //2. 
    [Header("Inverter direcao do Perigo")]
    public bool invertDirection = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (invertDirection)
        {
            parteMovel.transform.position = Vector2.Lerp(pointA.position, pointB.position, t);

            if(t >= 1)
            {
                t = 0;
                invertDirection = false;
            }
            else
            {
                t += velocidade * Time.deltaTime;
            }

        }
        else
        {
            parteMovel.transform.position = Vector2.Lerp(pointB.position, pointA.position, t);

            if (t >= 1)
            {
                t = 0;
                invertDirection = true;
            }
            else
            {
                t += velocidade * Time.deltaTime;
            }
        }
               
        
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(pointA.position,pointB.position);
    }
    public void resetarPerigoMovel()
    {
        t = 0;
    }
}
