using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerCollectable : MonoBehaviour
{
    public float value = 5;


    private void OnTriggerEnter2D(Collider2D collision) {

        if (collision.transform.CompareTag("Player")) {
            TimerController.Instance.AddTimer(value);
            gameObject.SetActive(false);

            LevelManager.Instance.addTimeCollectable(gameObject);
        }
    }
    
}
