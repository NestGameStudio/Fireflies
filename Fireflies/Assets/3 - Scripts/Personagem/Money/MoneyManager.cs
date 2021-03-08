using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyManager : MonoBehaviour
{
    public static MoneyManager instance {get; private set;}

    [Header("Dinheiro do player")]
    public int money = 0;

    private HUDManager hudUI;

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

    public void Start(){
        //guarda referência para singleton de HUD Manager
        hudUI = HUDManager.instance;
        if(hudUI != null){
            hudUI.moneyUI.SetupUI(money);
        } 
        else{
            Debug.Log("Não há nenhum objeto com HUD Manager em cena");
        }
    }

    //perder dinheiro por x quantidade
    public void perderDinheiro(int quantidade)
    {
        //caso a quantidade de dinheiro n seja suficiente, nao fazer nada
        if (money - quantidade < 0)
        {
            hudUI.moneyUI.LowMoney();
        }
        //caso contrario, descontar da quantidade de dinheiro atual do jogador
        else
        {
            money -= quantidade;
            hudUI.moneyUI.SetMoney(money);
        }
    }

    //ganhar dinheiro por x quantidade
    public void ganharDinheiro(int quantidade)
    {
        money += quantidade;
        hudUI.moneyUI.SetMoney(money);
    }

    //definir uma quantidade especifica de dinheiro
    public void setDinheiro(int quantidade)
    {
        money = quantidade;
        hudUI.moneyUI.SetMoney(money);
    } 
}
