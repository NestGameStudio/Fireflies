using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointObject : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D collision) {
        
        if (collision.CompareTag("Player")) {

            collision.GetComponentInChildren<Respawn>().UpdateCheckpoint(this.transform.position);
            Destroy(this.gameObject);

        }

    }
}
