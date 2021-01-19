using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManager_Level : MonoBehaviour
{
    public static SceneManager_Level sceneManagerInstance { get; private set; }

    public string[] sceneNames;
    public int startingLevel;

    //Scene masterScene;

    public Transform startingPoint;

    private GameObject Player;

    private void Awake()
    {
        sceneManagerInstance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        //masterScene = SceneManager.GetActiveScene();

        loadActiveLevel();

        if (GameObject.FindGameObjectWithTag("Player") != null)
        {
            Player = GameObject.FindGameObjectWithTag("Player");
        }
        else
        {
            Debug.Log("Cali nao encontrada");
        }

        
        desabilitarCali();
        
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
                    SceneManager.LoadSceneAsync(sceneNames[startingLevel - 1], LoadSceneMode.Additive);
                    
                    //SceneManager.SetActiveScene(SceneManager.GetSceneByName(sceneNames[startingLevel - 1]));

                }                
                else if(x != startingLevel - 1 && SceneManager.GetSceneByName(sceneNames[x]).isLoaded == true)
                {
                    SceneManager.UnloadSceneAsync(sceneNames[x], UnloadSceneOptions.None);
                }

                
            }
        }

        //disable player
        desabilitarCali();

        StartCoroutine(getRespawn());
        

        //SceneManager.SetActiveScene(masterScene);
    }


    public void nextLevel()
    {
        if (startingLevel < sceneNames.Length) {
            startingLevel += 1;

            loadActiveLevel();
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

    //espera um tempo ate a cena ser carregada para pegar o objeto de spawn dentro dela - meio gambiarra
    IEnumerator getRespawn()
    {
        yield return new WaitForSeconds(1);
        
        //get starting point
        if (GameObject.FindGameObjectWithTag("Respawn") != null) startingPoint = GameObject.FindGameObjectWithTag("Respawn").transform;

        //JOGADOR COLOCADO NO LUGAR CERTO E ATIVADO
        posicionarCali();

    }
    public void desabilitarCali()
    {
        if (Player != null)
        {
            //disable player
            Player.SetActive(false);
        }
    }
    public void posicionarCali()
    {
        if (Player != null)
        {
            //enable player
            Player.SetActive(true);
            Player.transform.position = startingPoint.position;
            
            //resetar cali a um estado estacionario
            Player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }
    }

}
