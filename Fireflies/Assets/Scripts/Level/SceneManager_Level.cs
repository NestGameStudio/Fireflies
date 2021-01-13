using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManager_Level : MonoBehaviour
{
    //fazer mudar a cena em referencia no editor

    public string[] sceneNames;
    public int startingLevel = 1;

    public KeyCode nextLevelKey;
    public KeyCode previousLevelKey;

    Scene masterScene;
    // Start is called before the first frame update
    void Start()
    {
        masterScene = SceneManager.GetActiveScene();

        loadActiveLevel();

        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKeyDown(nextLevelKey))
        {
            nextLevel();
        }
        if (Input.GetKeyDown(previousLevelKey))
        {
            previousLevel();
        }
    }

    void loadActiveLevel()
    {
        if (sceneNames != null)
        {
            //load active and unactive levels
            for (int x = 0; x < sceneNames.Length; x++)
            {
                //checar se existe cena com o nome certo e se ja n foi ativada
                if (SceneManager.GetSceneByName(sceneNames[x]) == null )
                {
                    SceneManager.LoadScene(sceneNames[x], LoadSceneMode.Additive);
                }
                
                if (x != startingLevel - 1)
                {
                    SceneManager.UnloadSceneAsync(sceneNames[x],UnloadSceneOptions.None);
                }
                
            }
        }

        SceneManager.SetActiveScene(masterScene);
    }


    public void nextLevel()
    {
        startingLevel += 1;

        loadActiveLevel();
    }
    public void previousLevel()
    {
        startingLevel -= 1;

        loadActiveLevel();
    }
}
