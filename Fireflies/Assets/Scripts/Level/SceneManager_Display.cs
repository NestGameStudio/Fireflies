using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.SceneManagement;
using UnityEditor;

[ExecuteInEditMode]
public class SceneManager_Display : MonoBehaviour
{
    SceneManager_Level sceneManager;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public void Update()
    {
        if (Application.isPlaying == false)
        {
            if (sceneManager == null)
            {
                sceneManager = GetComponent<SceneManager_Level>();
            }

            if (sceneManager != null)
            {

                for (int x = 0; x < sceneManager.sceneNames.Length; x++)
                {
                    //checa se a cena existe
                    if (EditorSceneManager.GetSceneByPath("Assets/1 - Scenes/Levels/" + sceneManager.sceneNames[x] + ".unity") != null)
                    {
                        //carrega a cena caso ela exista
                        EditorSceneManager.OpenScene("Assets/1 - Scenes/Levels/" + sceneManager.sceneNames[x] + ".unity", OpenSceneMode.Additive);
                    }

                    //Fazer a cena em referência a unica a estar ativada
                    if (x != sceneManager.startingLevel - 1)
                    {
                        EditorSceneManager.CloseScene(EditorSceneManager.GetSceneByPath("Assets/1 - Scenes/Levels/" + sceneManager.sceneNames[x] + ".unity"), false);
                    }
                }
            }

        }
    }
}
