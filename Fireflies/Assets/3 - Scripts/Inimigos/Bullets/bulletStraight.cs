using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletStraight : MonoBehaviour
{
    bool canDestroy = false;

    void Start()
    {
        GetComponent<CircleCollider2D>().enabled = false;
        Invoke("activateCollider", 0.2f);

        //se autodestruir apos x segundos
        Destroy(transform.parent.gameObject, 5);
    }

    void activateCollider()
    {
        GetComponent<CircleCollider2D>().enabled = true;
        canDestroy = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (canDestroy)
        {
            Destroy(transform.parent.gameObject);
        }
    }
}
