using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToMasterScene : MonoBehaviour
{
    public void masterScene()
    {
        SceneManager.LoadScene("MasterScene");
    }
}
