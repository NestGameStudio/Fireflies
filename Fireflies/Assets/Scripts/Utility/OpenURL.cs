using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenURL : MonoBehaviour
{
    public string url;

    public void openURL()
    {
        Debug.Log("URL Opened");
        Application.OpenURL(url);
    }
}
