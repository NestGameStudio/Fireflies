using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Looker_DetectCollision : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            //trocar o 10 pela variavel de dano do player
            if (gameObject.transform.parent.parent.gameObject.GetComponent<Looker_Behaviour>().health > 10)
            {
                if (gameObject.transform.parent.parent.gameObject.GetComponent<Looker_Behaviour>().damageParticle != null)
                    gameObject.transform.parent.parent.gameObject.GetComponent<Looker_Behaviour>().damageParticleTrigger();
            }
            gameObject.transform.parent.parent.gameObject.GetComponent<Looker_Behaviour>().perderVida(10);
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        
    }
}
