using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class detectEndLevel : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("Level_Final"))
        {
            //quando cali encostar no objeto de fim de fase ir pra proxima fase
            LevelManager.Instance.nextLevel();
        }

    }
}
