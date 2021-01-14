﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.SceneManagement;
using UnityEditor;

[CustomEditor(typeof(SceneManager_Level))]
public class SceneManager_Display : Editor
{
    
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        SceneManager_Level sceneManager = (SceneManager_Level)target;

        GUILayout.BeginHorizontal();

            if (GUILayout.Button("PreviousLevel") && sceneManager.startingLevel > 1)
            {
                sceneManager.startingLevel -= 1;
                atualizar(sceneManager);

                //sceneManager.previousLevel();
            }

            if (GUILayout.Button("NextLevel") && sceneManager.startingLevel < sceneManager.sceneNames.Length)
            {
                sceneManager.startingLevel += 1;
                atualizar(sceneManager);

                //sceneManager.nextLevel();
            }

        GUILayout.EndHorizontal();
    }
    

    public void atualizar(SceneManager_Level sceneManager)
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
    
}
