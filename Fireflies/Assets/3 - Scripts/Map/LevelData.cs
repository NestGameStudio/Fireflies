using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum LevelType { Null, Dungeon, Lobby, Shop}

public class LevelData: MonoBehaviour {

    public string name = null;
    public LevelType type = LevelType.Null;
    public bool isSafeZone;

    public List<GameObject> entities = new List<GameObject>();

    // Ter alguma forma de identificar as portas do Level
    // - Talvez olhar o tileset, identificar os tiles de porta, em que margem elas estão e quantos blocos são para conseguir gerar proceduralidade

    // Initialization
    public void Initialize(string name) {

        this.name = name;
        LevelTypeClassifier(name);

    }

    private LevelType LevelTypeClassifier(string name) {

        LevelType type = LevelType.Null;

        if (name.Contains("Dungeon")) {
            type = LevelType.Dungeon;
            isSafeZone = false;

        } else if (name.Contains("Lobby")) {
            type = LevelType.Lobby;
            isSafeZone = true;

        } else if (name.Contains("Shop")) {
            type = LevelType.Lobby;
            isSafeZone = true;
        }

        if (type == LevelType.Null) {
            Debug.LogError("O Level " + name + " não contem nenhuma das palavras chave: Dungeon, Lobby ou Shop");
        }

        return type;
    }

    // Entities
    public void AddEntity(GameObject entity) {
        entities.Add(entity);
    }

    public void RemoveEntity(GameObject entity) {
        entities.Remove(entity);
    }

    
}
