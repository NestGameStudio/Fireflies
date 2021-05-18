using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityPlayerSpawn: Entity {

    public Vector2 SpawnPosition = Vector2.zero;

    // Sobrescreve o método com as informações adequadas
    public override void Initialize(string name) {
        base.Initialize(name);

        SpawnPosition = this.transform.position;

        GameObject.FindGameObjectWithTag("Player").transform.position = SpawnPosition;
        CameraShake.instance.CameraFollow();
    }
}
