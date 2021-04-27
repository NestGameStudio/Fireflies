using System;
using System.Text;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class SaveSystem : MonoBehaviour
{
    [HideInInspector]
    public GameStats Stats;
    public GameStats emptyStats;
    public static float saveVersion = 1.0f;
    private static string saveFileName = "stats";
    public static bool SucessfulLoad = false;
    private static string SavePath
    {
        get
        {
            return Path.Combine(Application.persistentDataPath, saveFileName + ".dat");
        }
    }

    private static string versionSaveName = "version";
    private static string VersionSavePath
    {
        get
        {
            return Path.Combine(Application.persistentDataPath, versionSaveName + ".ver");
        }
    }

    private static SaveSystem Instance;
    public static SaveSystem instance
    {
        get{return Instance;}
    }

    private void Awake() {
        if(Instance != null)
        {
            GameObject.Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
            bool res = LoadState();
            if(!res || res)
            {
                NewGame();
            }
        }
        DontDestroyOnLoad(this.gameObject);
    }

    public void SaveState() {
        string json_ps = JsonUtility.ToJson(Stats);
        byte[] buffer = Encoding.UTF8.GetBytes(json_ps);
        for(int i = 0; i < buffer.Length; i++)
        {
            buffer[i] = (Byte)((~buffer[i]) & 0xFF);
        }
        try
        {
            File.WriteAllBytes(SavePath, buffer);
        }
        catch (Exception e)
        {
            Debug.LogWarning("Unable to write to savefile" + e.Message);
        }
        
        using (StreamWriter streamWriter = File.CreateText (VersionSavePath))
        {
            streamWriter.Write (saveVersion.ToString());
        }
    }

    public bool LoadState() {
        string path = SavePath;
        string versionPath = VersionSavePath;
        if(!File.Exists(path) || !File.Exists(versionPath)) {
            return false;
        }
        using(StreamReader streamReader = File.OpenText(versionPath)) {
            string str = streamReader.ReadToEnd();
            try {
                float ver = float.Parse(str);
            }
            catch (Exception e) {
                Debug.LogWarning("Warning: invalid save-version format" + e.Message);
            }
        }

        try {
            byte[] buffer = File.ReadAllBytes(path);
            for(int i=0;i< buffer.Length;i ++) {
                buffer[i] = (Byte)((~buffer[i]) & 0xFF);
            }
            string jsonString = Encoding.UTF8.GetString(buffer);
            Stats = ScriptableObject.CreateInstance<GameStats>();
            JsonUtility.FromJsonOverwrite(jsonString, Stats);

            SucessfulLoad = true;
            return true;
        }
        catch (Exception e) {
            Debug.LogWarning("Unable to load savefile " + e.Message);
            return false;
        }
    }  

    public void NewGame() {
        Stats = GameObject.Instantiate(emptyStats);
        SaveState();
        SucessfulLoad = true;
        string path = Path.Combine(Application.persistentDataPath, saveFileName + ".dat");
        Debug.Log("new save on path:" + path);
    } 
}