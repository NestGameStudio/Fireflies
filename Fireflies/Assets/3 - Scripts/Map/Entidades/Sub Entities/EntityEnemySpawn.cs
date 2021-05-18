using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyType { Null, Bleeper, Looker, Random}

public class EntityEnemySpawn : Entity {

    [HideInInspector]
    public Vector2 SpawnPosition = Vector2.zero;
    public EnemyType EnemyType = EnemyType.Null;

    [Tooltip("Chance de spawnar o indicado pelo EnemyType. Valor 1 sempre realizará spawn, 0 nunca realizará spawn")]
    [Range(0.0f,1.0f)]
    public float spawnChance = 1.0f;
    public GameObject bleeperPrefab;
    public GameObject lookerPrefab;
    private GameObject spawnedObject; //Referência para o objeto que spawnou, se houver

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

    public void Start(){
        if(bleeperPrefab==null){
            Debug.LogError("Prefab do inimigo Bleeper não tem referência");
        }
        if(lookerPrefab==null){
            Debug.LogError("Prefab do inimigo Looker não tem referência");
        }

        SpawnPosition = this.transform.position;
        SpawnRoll();
    }

    public void SpawnRoll(){
        int enemyTypes = 2; //Bleeper e Looker
        float roll = Random.Range(0.0f, 1.0f);

        if(roll <= spawnChance){
            //Algo será spawnado
            switch(EnemyType){

                //Spawnar Aleatório (Bleeper ou Looker)
                case EnemyType.Random:
                    int enemyTypeRoll = Mathf.RoundToInt(roll*100) % enemyTypes; //resulta em 0 ou 1
                    switch(enemyTypeRoll){
                        case 0:
                            //Bleeper
                            spawnedObject = SpawnEnemy(bleeperPrefab, SpawnPosition);
                            break;
                        case 1:
                            //Looker
                            spawnedObject = SpawnEnemy(lookerPrefab, SpawnPosition);
                            break;
                    }
                    break;
                
                //Spawnar Bleeper
                case EnemyType.Bleeper:
                    spawnedObject = SpawnEnemy(bleeperPrefab, SpawnPosition);
                    break;
                
                //Spawnar Looker
                case EnemyType.Looker:
                    spawnedObject = SpawnEnemy(lookerPrefab, SpawnPosition);
                    break;
            }
        }
    }
    public GameObject SpawnEnemy(GameObject enemyPrefab, Vector2 spawnPos){
        return Instantiate(enemyPrefab, new Vector3(SpawnPosition.x, SpawnPosition.y, 0f), Quaternion.identity, this.transform);
    }

    //Desenha um gizmo correspondente ao EnemyType
    #if UNITY_EDITOR
        private void OnDrawGizmos(){
            string name = "EnemySpawner\\" + EnemyType.ToString() + ".png";
            Gizmos.DrawIcon(transform.position, name, true);
        }
    #endif
}
