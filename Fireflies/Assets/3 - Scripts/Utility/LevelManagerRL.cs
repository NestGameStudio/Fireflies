using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManagerRL : MonoBehaviour
{
    public static LevelManagerRL Instance { get { return instance; } }
    private static LevelManagerRL instance;

    public List<string> MapList;

    private void Awake() {

        // Singleton
        if (instance != null && instance != this) {
            Destroy(this.gameObject);
        } else {
            instance = this;
        }
    }

    public void ChooseNewMap() {
        int rnd = Random.Range(0,MapList.Count);
        while(MapList[rnd] == SceneManager.GetActiveScene().name) {
            rnd = Random.Range(0,MapList.Count);
        }
        SceneManager.LoadScene(MapList[rnd],LoadSceneMode.Single);
        Time.timeScale = 1f;
    }
    
}
