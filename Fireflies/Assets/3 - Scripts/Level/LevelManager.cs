using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { get; private set; }
    //private static LevelManager instance;

    public string[] sceneNames;
    public int startingLevel;

    private AsyncOperation loadingSceneStatus; 

    // O evento escuta se a scene já foi loaded
    [HideInInspector] public UnityEvent SceneLoaded = new UnityEvent();

    private bool isStart = true;
    public RectTransform transitionPanel;

    public float fadeInTime = 0.4f;
    public float fadeOutTime = 0.3f;

    //[HideInInspector]
    public List<GameObject> breakPlats = new List<GameObject>();

    public AudioSource nextLevelAudio;

    public List<GameObject> timeCollectables = new List<GameObject>();

    public List<PerigoMovel> espinhos = new List<PerigoMovel>();

    [HideInInspector]
    public bool canStartTimer = false;

    private void Awake()
    {

        //unloadScenes();

        // Singleton
        if (Instance != null && Instance != this) {
            Destroy(this.gameObject);
        } else {
            Instance = this;
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
            //resetar timer quando passar de fase
            TimerController.Instance.ResetTimer();

            //load active and unactive levels
            for (int x = 0; x < sceneNames.Length; x++)
            {

                if (x == startingLevel - 1 && SceneManager.GetSceneByName(sceneNames[startingLevel - 1]).isLoaded == false)
                {
                    if (isStart)
                    {
                        isStart = false;
                        //loadingSceneStatus = SceneManager.LoadSceneAsync(sceneNames[startingLevel - 1], LoadSceneMode.Additive);
                        changeScene();
                    }
                    else
                    {
                        //fade in + funcao de trocar cena
                        LeanTween.alpha(transitionPanel, 1, fadeInTime).setOnComplete(() => changeScene());
                        
                        //changeScene();

                    }


                    
                }                
                else if(x != startingLevel - 1 && SceneManager.GetSceneByName(sceneNames[x]).isLoaded == true)
                {
                    loadingSceneStatus = SceneManager.UnloadSceneAsync(sceneNames[x], UnloadSceneOptions.None);
                }

                
            }
        }
    }
    void changeScene()
    {
        Debug.Log("changedScene");

        loadingSceneStatus = SceneManager.LoadSceneAsync(sceneNames[startingLevel - 1], LoadSceneMode.Additive);

        StartCoroutine(UpdateSceneStatus());

        //resetar a lista com as plataformas quebraveis do level
        ClearBreakPlatList();

        clearTimeCollectable();

        //pegar as plataformas quebraveis do level numa lista
        getLevelBreakPlats();

        //Respawn.instance.GetInitialSpawn();
        //Respawn.instance.RepositionPlayer();

        //fade out
        LeanTween.alpha(transitionPanel,0,fadeOutTime);
    }
    void unloadScenes()
    {
        if (sceneNames != null)
        {
            //load active and unactive levels
            for (int x = 0; x < sceneNames.Length; x++)
            {
                if (SceneManager.GetSceneByName(sceneNames[x]).isLoaded == true)
                {
                    loadingSceneStatus = SceneManager.UnloadSceneAsync(sceneNames[x]);
                }
            }
        }
    }

    public void nextLevel()
    {
        NextLevelAudio();
        

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

        PegarEspinhosMoveis();

        if(startingLevel > 1)
        {
            canStartTimer = true;
            GetComponent<timeController_Display>().hasTimer = true;
        }
        else
        {
            GetComponent<timeController_Display>().hasTimer = false;
        }
    }
    public void getLevelBreakPlats()
    {
        

        if(GameObject.FindGameObjectsWithTag("Plataforma_Quebravel").Length > 0)
        {
            Debug.Log("got break plats from level");

            //pegar as plataformas quebraveis do level e colocar todas numa lista
            for (int x = 0; x < GameObject.FindGameObjectsWithTag("Plataforma_Quebravel").Length; x++)
            {
                //checar se o objeto ja nao esta na lista para prevenir duplicatas
                if (breakPlats.Contains(GameObject.FindGameObjectsWithTag("Plataforma_Quebravel")[x].transform.parent.gameObject) == false) {
                    breakPlats.Add(GameObject.FindGameObjectsWithTag("Plataforma_Quebravel")[x].transform.parent.gameObject);
                }
            }
        }

        if (GameObject.FindGameObjectsWithTag("Plataforma_Quebravel_Fake").Length > 0)
        {
            Debug.Log("got break plats from level");

            //pegar as plataformas quebraveis do level e colocar todas numa lista
            for (int x = 0; x < GameObject.FindGameObjectsWithTag("Plataforma_Quebravel_Fake").Length; x++)
            {
                //checar se o objeto ja nao esta na lista para prevenir duplicatas
                if (breakPlats.Contains(GameObject.FindGameObjectsWithTag("Plataforma_Quebravel_Fake")[x].transform.parent.gameObject) == false)
                {
                    breakPlats.Add(GameObject.FindGameObjectsWithTag("Plataforma_Quebravel_Fake")[x].transform.parent.gameObject);
                }
            }
        }
    }
    public void resetTimeCollectable()
    {
        if (timeCollectables.Count > 0)
        {
            for (int i = 0; i < timeCollectables.Count; i++)
            {
                if(timeCollectables[i] != null)
                {
                    timeCollectables[i].SetActive(true);
                }
            }
        }
    }
    public void clearTimeCollectable()
    {
        timeCollectables.Clear();
    }
    public void addTimeCollectable(GameObject objeto)
    {
        /*
        if (GameObject.FindGameObjectsWithTag("Time_Collectable").Length > 0)
        {
            Debug.Log("ativar coletavel de tempo");
            for (int i = 0; i < GameObject.FindGameObjectsWithTag("Time_Collectable").Length; i++)
            {
                Debug.Log("activated", GameObject.FindGameObjectsWithTag("Time_Collectable")[i].gameObject);
                GameObject.FindGameObjectsWithTag("Time_Collectable")[i].gameObject.SetActive(true);
            }
        }
        */
        if (timeCollectables.Contains(objeto) == false)
        {
            timeCollectables.Add(objeto);
        }
    }

    public void resetPlats()
    {
        Debug.Log("reseted breakplats");

        if (breakPlats.Count > 0)
        {
            for (int x = 0; x < breakPlats.Count; x++)
            {
                if (breakPlats[x] != null)
                {
                    breakPlats[x].transform.GetChild(0).gameObject.SetActive(true);
                    breakPlats[x].GetComponentInParent<Animator>().SetBool("Break", false);
                }
            }
        }
    }

    public void ClearBreakPlatList()
    {
        if(breakPlats.Count > 0)
        {
            breakPlats.Clear();
        }
        
    }

    void NextLevelAudio()
    {
        nextLevelAudio.PlayOneShot(nextLevelAudio.clip, nextLevelAudio.volume);
    }
    void PegarEspinhosMoveis()
    {
        espinhos.Clear();

        if(GameObject.FindGameObjectsWithTag("Perigo").Length > 0)
        {
            for (int i = 0; i < GameObject.FindGameObjectsWithTag("Perigo").Length; i++)
            {
                if(GameObject.FindGameObjectsWithTag("Perigo")[i].GetComponent<PerigoMovel>() != null)
                {
                    espinhos.Add(GameObject.FindGameObjectsWithTag("Perigo")[i].GetComponent<PerigoMovel>());
                }
            }
        }
        
    }
    public void ResetarEspinhosMoveis()
    {
        for (int i = 0; i < espinhos.Count; i++)
        {
            espinhos[i].resetarPerigoMovel();
        }
    }
}
