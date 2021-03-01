using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityBarrel : Entity {

    // Sobrescreve o método com as informações adequadas
    public override void Initialize(string name) {
        base.Initialize(name);

        // Vincular ao coin sistem que o DJ fez (spawnnar moedas quando quebra)
    }
}
