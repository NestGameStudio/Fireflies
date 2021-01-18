﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManager_Level : MonoBehaviour
{
    //fazer mudar a cena em referencia no editor

    public string[] sceneNames;
    public int startingLevel;

    public KeyCode nextLevelKey;
    public KeyCode previousLevelKey;

    Scene masterScene;

    public Transform startingPoint;
    // Start is called before the first frame update
    void Start()
    {
        masterScene = SceneManager.GetActiveScene();

        loadActiveLevel();
        
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKeyDown(nextLevelKey) && startingLevel < sceneNames.Length)
        {
            nextLevel();
        }
        if (Input.GetKeyDown(previousLevelKey) && startingLevel > 1)
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
                    SceneManager.LoadScene(sceneNames[startingLevel - 1], LoadSceneMode.Additive);

                }                
                else if(x != startingLevel - 1 && SceneManager.GetSceneByName(sceneNames[x]).isLoaded == true)
                {
                    SceneManager.UnloadSceneAsync(sceneNames[x], UnloadSceneOptions.None);
                }
                

            }
        }

        //get starting point
        if (GameObject.FindGameObjectWithTag("Respawn") != null) startingPoint = GameObject.FindGameObjectWithTag("Respawn").transform;

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
