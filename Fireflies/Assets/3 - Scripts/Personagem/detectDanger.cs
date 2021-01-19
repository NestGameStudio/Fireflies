using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class detectDanger : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("Bleeper_Invulneravel") || collision.gameObject.CompareTag("Inimigo") || collision.gameObject.CompareTag("Perigo"))
        {
            //colocar a cali de volta ao starting point depois dela ser atingida por algum perigo, inimigo etc
            SceneManager_Level.sceneManagerInstance.posicionarCali();
        }

    }

}
