using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyManager : MonoBehaviour
{
    public static MoneyManager instance {get; private set;}

    [Header("Dinheiro do player")]
    public int money = 0;

    public void Awake()
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

    //perder dinheiro por x quantidade
    public void perderDinheiro(int quantidade)
    {
        //caso a quantidade de dinheiro n seja suficiente, nao fazer nada
        if (money - quantidade < 0)
        {
            
        }
        //caso contrario, descontar da quantidade de dinheiro atual do jogador
        else
        {
            money -= quantidade;
        }
    }

    //ganhar dinheiro por x quantidade
    public void ganharDinheiro(int quantidade)
    {
        money += quantidade;
    }

    //definir uma quantidade especifica de dinheiro
    public void setDinheiro(int quantidade)
    {
        money = quantidade;
    } 
}
