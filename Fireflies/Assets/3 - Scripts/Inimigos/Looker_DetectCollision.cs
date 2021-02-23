using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Looker_DetectCollision : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            gameObject.transform.parent.parent.gameObject.GetComponent<Looker_Behaviour>().morreu();
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        
    }
}
