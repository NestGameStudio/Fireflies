using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.SceneManagement;
using UnityEditor;

[CustomEditor(typeof(LevelManager))]
public class LevelManager_Display : Editor
{
    void onEnable()
    {
        LevelManager sceneManager = (LevelManager) target;
        atualizar(sceneManager);
    }
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        LevelManager sceneManager = (LevelManager) target;

        GUILayout.BeginHorizontal();

            if (GUILayout.Button("Previous Level") && sceneManager.startingLevel > 1)
            {
                
                //if application is playing 
                if (Application.isPlaying)
                {
                    sceneManager.previousLevel();
                }
                //if is in editor
                else
                {
                    sceneManager.startingLevel -= 1;

                    EditorUtility.SetDirty(sceneManager);

                    atualizar(sceneManager);
            }

                //sceneManager.previousLevel();
            }

            if (GUILayout.Button("Next Level") && sceneManager.startingLevel < sceneManager.sceneNames.Length)
            {

                //if application is playing 
                if (Application.isPlaying)
                {
                    sceneManager.nextLevel();
                }
                //if is in editor
                else
                {
                    sceneManager.startingLevel += 1;

                    EditorUtility.SetDirty(sceneManager);

                    atualizar(sceneManager);
                }
            //sceneManager.nextLevel();
        }

        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();

        if (GUILayout.Button("Close Scenes"))
        {
            descarregar(sceneManager);
        }
            GUILayout.EndHorizontal();
    }
    

    public void atualizar(LevelManager sceneManager)
    {
        if (Application.isPlaying == false)
        {

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
    void descarregar(LevelManager sceneManager)
    {
        if (sceneManager != null)
        {

            for (int x = 0; x < sceneManager.sceneNames.Length; x++)
            {
                
                    EditorSceneManager.CloseScene(EditorSceneManager.GetSceneByPath("Assets/1 - Scenes/Levels/" + sceneManager.sceneNames[x] + ".unity"), false);
                
            }
        }
    }


}
