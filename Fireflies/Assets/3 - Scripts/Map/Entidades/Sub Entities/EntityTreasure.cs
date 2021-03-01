using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityTreasure : Entity {

    // Sobrescreve o método com as informações adequadas
    public override void Initialize(string name) {
        base.Initialize(name);

        // Vincular ao sistema de upgrades (randomiza 1 e spawna ele quando quebra)
    }
}
