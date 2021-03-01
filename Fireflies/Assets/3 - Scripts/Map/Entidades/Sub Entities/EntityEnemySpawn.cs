using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyType { Null, Bleeper, Looker, Random}

public class EntityEnemySpawn : Entity {

    public Vector2 SpawnPosition = Vector2.zero;
    public EnemyType EnemyType = EnemyType.Null;

    // Sobrescreve o método com as informações adequadas
    public override void Initialize(string name) {
        base.Initialize(name);

        SpawnPosition = this.transform.position;

        if (name.Contains("Bleeper")) {
            EnemyType = EnemyType.Bleeper;

        } else if (name.Contains("Looker")) {
            EnemyType = EnemyType.Looker;

        } else if (name.Contains("Random")) {
            EnemyType = EnemyType.Random;

        } else {
            EnemyType = EnemyType.Null;
            Debug.LogError("O inimigo " + name + " não contem nenhuma das palavras chave: Bleeper, Looker ou Random");
        }

        // Instancia os inimigos em cima de uma probabilidade (colocar as probabilidades no script de Setup)

        // Ter uma forma de verificar se a Cali está numa sala que é safezone ou não, se entrou na safe zone faz com que os inimigos parem de correr atrás de você

        // Spawnanr moedas quando o inimigo morre
    }
}
