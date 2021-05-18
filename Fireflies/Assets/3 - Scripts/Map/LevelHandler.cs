using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LDtkUnity;

public class LevelHandler : MonoBehaviour
{
    public GameObject MapParent;

    private void OnEnable() {
        LDtkUnity.Builders.LDtkLevelBuilder.OnLevelBuilt += LDtkLevelData;
    }

    /// <summary>
    /// Faz o setup do level quando ele buildado
    /// </summary>
    /// <param name="level"></param>
    private void LDtkLevelData(Level level) {

        LevelSettings(level.Identifier);
    }

    /// <summary>
    /// Faz todas as alterações que precisam ser feitas ao iniciar um level:
    /// - Adiciona os GameObjects ao pai Map
    /// - Separa as grids como safe zones ou danger zones
    /// - Realiza todos os Spawns: Player, Inimigos, Barril, Tesouro
    /// Infelizmente identificar cada coisa por nome ainda
    /// </summary>
    private void LevelSettings(string name) {

        GameObject level = GameObject.Find(name);

        // Adiciona os GameObjects do level ao pai Map
        if (MapParent != null) {
            level.transform.parent = MapParent.transform;
        } else { Debug.LogWarning("Leveis não possuem um GameObject pai");}

        // - Adiciona a classe Level Data
        // - Classifica cada tipo de level (dungeon, lobby, shop)
        // - Ínforma se é uma safe zone ou não
        LevelData data = level.AddComponent<LevelData>();
        data.Initialize(name);

        // Verifica cada entidade do level
        GameObject Entities = level.transform.Find("Entities").gameObject;

        foreach (Transform entity in Entities.transform.GetComponentInChildren<Transform>()) {

            // Classifica as entidades do level
            EntityClassification(entity.gameObject, entity.name);

            // Adiciona a entidade ao Level Data
            data.AddEntity(entity.gameObject);

        }  
 

    }

    /// <summary>
    /// Classifica as entidades entre Player Spawn, Enemy Spawn, Barrel ou Treasure, colocando sua devida classe Entity
    /// </summary>
    /// <param name="entity"></param>
    /// <param name="name"></param>
    private void EntityClassification(GameObject entity, string name) {

        if (name.Contains("Player Spawn")) {
            EntityPlayerSpawn PlayerSpawn = entity.AddComponent<EntityPlayerSpawn>();
            PlayerSpawn.Initialize(name);

        } else if (name.Contains("Enemy Spawn")) {
            EntityEnemySpawn EnemySpawn = entity.AddComponent<EntityEnemySpawn>();
            EnemySpawn.Initialize(name);

        } else if (name.Contains("Barrel")) {
            EntityBarrel Barrel = entity.AddComponent<EntityBarrel>();
            Barrel.Initialize(name);

        } else if (name.Contains("Treasure")) {
            EntityTreasure Treasure = entity.AddComponent<EntityTreasure>();
            Treasure.Initialize(name);

        } else {
            Debug.LogError("A entidade " + name + " não contem nenhuma das palavras chave: Player Spawn, Enemy Spawn, Barrel ou Treasure");
        }
    }

}
