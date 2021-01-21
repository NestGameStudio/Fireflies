using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    public static Respawn instance { get; private set; }

    public GameObject Player;

    private Vector2 currentCheckpoint;

    private void Awake()
    {
        // Singleton
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
    }
    // Start is called before the first frame update
    void Start() {

        // Level Manager tá na Scene
        if (LevelManager.Instance != null) {

            // Está escutando se a scene foi loaded
            LevelManager.Instance.SceneLoaded.AddListener(GetInitialSpawn);

        } else {
            GetInitialSpawn();
        }
    }

    private void GetInitialSpawn() {

        Player.SetActive(false);

        //get starting point
        if (GameObject.FindGameObjectWithTag("Spawn") != null) {
            currentCheckpoint = GameObject.FindGameObjectWithTag("Spawn").transform.position;
        } else {
            currentCheckpoint = this.transform.position;
        }

        RepositionPlayer();
    }

    public void RepositionPlayer() {

        //enable player
        Player.SetActive(true);
        Player.transform.position = currentCheckpoint;

        //resetar cali a um estado estacionario
        Player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;

        //resetar timer
        TimerController.Instance.ResetTimer();

    }

    public void UpdateCheckpoint(Vector2 position) {
        currentCheckpoint = position;
    }

}
