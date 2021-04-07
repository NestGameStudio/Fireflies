using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Looker_DetectCollision : MonoBehaviour
{
    private Looker_Behaviour looker;
    private void Start(){
        if(looker == null){
            looker = gameObject.transform.parent.parent.gameObject.GetComponent<Looker_Behaviour>();
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            looker.enemy.TakeDamage(10);
        }
    }
}
