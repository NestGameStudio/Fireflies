using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManager_Level : MonoBehaviour
{
    //fazer mudar a cena em referencia no editor

    public string[] sceneNames;
    public int startingLevel;

    //Scene masterScene;

    public Transform startingPoint;
    // Start is called before the first frame update
    void Start()
    {
        //masterScene = SceneManager.GetActiveScene();

        loadActiveLevel();
        
        
    }


    void loadActiveLevel()
    {
        if (sceneNames != null)
        {
            //load active and unactive levels
            for (int x = 0; x < sceneNames.Length; x++)
            {
                /*
                //checar se existe cena com o nome certo e se ja n foi ativada
                if (SceneManager.GetSceneByName(sceneNames[x]) == null)
                {
                    SceneManager.LoadScene(sceneNames[x], LoadSceneMode.Additive);
                }
                
                if (x != startingLevel - 1 && SceneManager.GetSceneByName(sceneNames[x]) != null)
                {
                    SceneManager.UnloadSceneAsync(sceneNames[x],UnloadSceneOptions.None);
                }
                */

                /*
                if (x == startingLevel - 1 && SceneManager.GetSceneByName(sceneNames[startingLevel - 1]) == null)
                {
                    SceneManager.LoadScene(sceneNames[startingLevel - 1], LoadSceneMode.Additive);

                }
                else 
                {
                    SceneManager.UnloadSceneAsync(sceneNames[x], UnloadSceneOptions.None);
                }
                */
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


        StartCoroutine(getRespawn());
        

        //SceneManager.SetActiveScene(masterScene);
    }


    public void nextLevel()
    {
        if (startingLevel < sceneNames.Length) {
            startingLevel += 1;

            loadActiveLevel();
        }
      
    }
    public void previousLevel()
    {
        if (startingLevel > 1) {
            startingLevel -= 1;

            loadActiveLevel();
        }
        
    }

    //espera um tempo ate a cena ser carregada para pegar o objeto de spawn dentro dela - meio gambiarra
    IEnumerator getRespawn()
    {
        yield return new WaitForSeconds(1);
        
        //get starting point
        if (GameObject.FindGameObjectWithTag("Respawn") != null) startingPoint = GameObject.FindGameObjectWithTag("Respawn").transform;

        //COLOCAR TRIGGER DE SPAWNAR O JOGADOR AQUI
    }

}
