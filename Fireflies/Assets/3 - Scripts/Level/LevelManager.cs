using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { get { return instance; } }
    private static LevelManager instance;

    public string[] sceneNames;
    public int startingLevel;

    private AsyncOperation loadingSceneStatus; 

    // O evento escuta se a scene já foi loaded
    [HideInInspector] public UnityEvent SceneLoaded = new UnityEvent();

    private void Awake()
    {
        unloadScenes();

        // Singleton
        if (instance != null && instance != this) {
            Destroy(this.gameObject);
        } else {
            instance = this;
        }
    }

    // Start is called before the first frame update
    void Start() {

        loadActiveLevel();
    }


    void loadActiveLevel()
    {
        if (sceneNames != null)
        {
            //load active and unactive levels
            for (int x = 0; x < sceneNames.Length; x++)
            {

                if (x == startingLevel - 1 && SceneManager.GetSceneByName(sceneNames[startingLevel - 1]).isLoaded == false)
                {
                    loadingSceneStatus = SceneManager.LoadSceneAsync(sceneNames[startingLevel - 1], LoadSceneMode.Additive);

                    StartCoroutine(UpdateSceneStatus());
                }                
                else if(x != startingLevel - 1 && SceneManager.GetSceneByName(sceneNames[x]).isLoaded == true)
                {
                    loadingSceneStatus = SceneManager.UnloadSceneAsync(sceneNames[x], UnloadSceneOptions.None);
                }

                
            }
        }
    }
    void unloadScenes()
    {
        if (sceneNames != null)
        {
            //load active and unactive levels
            for (int x = 0; x < sceneNames.Length; x++)
            {

                loadingSceneStatus = SceneManager.UnloadSceneAsync(sceneNames[x], UnloadSceneOptions.None);
                
            }
        }
    }

    public void nextLevel()
    {
        if (startingLevel < sceneNames.Length) {
            startingLevel += 1;

            loadActiveLevel();

            //resetar timer quando passar de fase
            TimerController.Instance.ResetTimer();
        }
        else
        {
            //acabando as fases, vai pro menu
            loadFinalScene();
        }
      
    }
    public void previousLevel()
    {
        if (startingLevel > 1) {
            startingLevel -= 1;

            loadActiveLevel();
        }
        
    }
    //carregar cena de menu
    public void loadMenu()
    {
        SceneManager.LoadScene("MenuScene");
    }
    //carregar cena final
    public void loadFinalScene()
    {
        SceneManager.LoadScene("EndScene");
    }

    IEnumerator UpdateSceneStatus() {

        while (!LevelManager.Instance.loadingSceneStatus.isDone) {
            yield return null;
        }

        // Invoca todas as ações que estavam escutando a scene ser loaded
        SceneLoaded.Invoke();
    }

}
