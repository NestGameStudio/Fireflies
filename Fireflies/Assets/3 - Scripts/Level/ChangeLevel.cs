﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeLevel : MonoBehaviour
{


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            //troca de level
            trocaDeLevel();
        }
    }

    void trocaDeLevel()
    {

    }
}
