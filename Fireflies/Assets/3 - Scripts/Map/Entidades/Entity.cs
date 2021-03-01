using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections.Generic;

public enum EntityType { Null, PlayerSpawn, EnemySpawn, CoinBarrel, UpgradeTreasure }

public class Entity: MonoBehaviour {

    public string name = null;
    public EntityType type = EntityType.Null;

    public virtual void Initialize(string name) {

        this.name = name;

        if (name.Contains("Player Spawn")) {
            type = EntityType.PlayerSpawn;

        // Separa o tipo em Bleeper, Looker, Random dentro da classe Entity Enemy Spawn
        } else if (name.Contains("Enemy Spawn")) {
            type = EntityType.EnemySpawn;

        } else if (name.Contains("Barrel")) {
            type = EntityType.CoinBarrel;

        } else if (name.Contains("Treasure")) {
            type = EntityType.UpgradeTreasure;

        } else {
            type = EntityType.Null;
            Debug.LogError("A entidade " + name + " não contem nenhuma das palavras chave: Player Spawn, Enemy Spawn, Barrel ou Treasure");
        }

    }

}
