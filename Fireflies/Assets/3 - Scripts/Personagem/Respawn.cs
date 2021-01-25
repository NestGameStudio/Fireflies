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
            //GetInitialSpawn();
        }
    }

    private void GetInitialSpawn() {

        Player.SetActive(false);
        Player.GetComponent<CircleCollider2D>().enabled = false;

        //get starting point
        if (GameObject.FindGameObjectWithTag("Spawn") != null) {
            currentCheckpoint = GameObject.FindGameObjectWithTag("Spawn").transform.position;
        } else {
            currentCheckpoint = this.transform.position;

            Debug.Log("nao achou local de respawn");
        }

        RepositionPlayer();
    }

    public void RepositionPlayer() {

        //enable player
        Player.SetActive(true);

        //Player.GetComponentInChildren<TrailRenderer>().enabled = false;
        
        Player.transform.position = currentCheckpoint;

        Player.GetComponent<CircleCollider2D>().enabled = true;

        //resetar cali a um estado estacionario
        Player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;

        //resetar timer
        TimerController.Instance.ResetTimer();

        //Player.GetComponentInChildren<TrailRenderer>().enabled = true;

    }

    public void UpdateCheckpoint(Vector2 position) {
        currentCheckpoint = position;
    }

}
