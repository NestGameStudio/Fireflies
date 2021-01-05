using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BleeperBehaviour : MonoBehaviour
{
    /*
    Fica piscando entre duas cores, vermelho e verde.
    No caso da Cali acertar o inimigo enquanto ele está Verde, ele é derrotado.
    Caso Cali acerte ele enquanto ele está Vermelho, Cali perde Vida.
    */

    [Header("Debug Vars")]
    public Text timeDisplay;

    [Header("Ativa visualizacao de debug")]
    public bool Debug = false;

    public Color[] baseColors;
    public SpriteRenderer[] baseRenderer;

    [Header("Tempo(s) ate mudar de estado")]
    public float changeTime = 1;

    //variavel que guarda o changeTime logo no inicio do jogo
    private float timeBackup;

    public enum estado
    {
        inatingivel,
        atingivel
    }

    [Header("Visualizacao do estado do inimigo")]
    public estado Estado;
    // Start is called before the first frame update
    void Start()
    {
        timeBackup = changeTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (changeTime > 0)
        {
            changeTime -= Time.deltaTime;
        }
        else
        {
            changeState();

        }

        //mostrar tempo faltante em display
        timeDisplay.text = (Mathf.Round(changeTime*10)/10).ToString();
    }
    void changeState()
    {
        //reseta timer
        changeTime = timeBackup;

        mudarCor();

        //alternar estado
        if (Estado == estado.inatingivel)
        {
            Estado = estado.atingivel;
        }
        else
        {
            Estado = estado.inatingivel;
        }

    }
    public void mudarCor()
    {
        //colocar cor correspondente ao estado
        for (int x = 0; x < baseRenderer.Length; x++)
        {
            baseRenderer[x].color = baseColors[(int)Estado];
        }

        //colocar tag correspondente ao estado
        mudarTag();

    }
    public void mudarTag()
    {
        if (gameObject.tag == "Bleeper_Invulneravel") {

            gameObject.tag = "Bleeper_Vulneravel";

        }
        else
        {
            gameObject.tag = "Bleeper_Invulneravel";
        }
    }
}
