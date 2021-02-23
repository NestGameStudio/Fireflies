using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    public static HealthManager instance { get; private set; }

    [Header("Vida do player")]
    public int health = 100;

    [Header("Maximo de vida do player")]
    public int maximoVida = 100;

    private void Awake()
    {
        // Singleton
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
    }

    //perder vida por x quantidade
    public void menosVida(int quantidade)
    {
        if (health - quantidade > 0)
        {
            //perdeu vida
            health -= quantidade;
        }
        else
        {
            //morreu
            health = 0;
            morreu();
        }
    }

    //ganhar vida por x quantidade
    public void maisVida(int quantidade)
    {
        if(health + quantidade > maximoVida)
        {
            //limitar a vida pelo maximo
            health = maximoVida;
        }
        else
        {
            health += quantidade;
        }
    }

    //aumentar limite de vida por x quantidade, completar vida?
    public void aumentarLimite(int quantidade, bool completar)
    {
        maximoVida += quantidade;

        //caso esteja true, completar vida 
        if (completar)
        {
            health = maximoVida;
        }
    }

    //diminuir limite de vida por x quantidade
    public void diminuirLimite(int quantidade)
    {
        maximoVida -= quantidade;

        //perder vida caso a vida esteja dentro do range que sera perdido
        if(health > maximoVida)
        {
            health = maximoVida;
        }
    }

    //especificar um limite de vida, completar vida?
    public void setLimite(int limite, bool completar)
    {
        maximoVida = limite;

        if (completar)
        {
            health = maximoVida;
        }
    }

    //morreu
    public void morreu()
    {
        Respawn.instance.RepositionPlayer();
    }     
}
