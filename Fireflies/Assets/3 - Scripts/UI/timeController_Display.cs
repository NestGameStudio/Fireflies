using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class timeController_Display : MonoBehaviour
{
    public bool hasTimer = true;
    public GameObject timerObj;
    // Start is called before the first frame update
    void Update()
    {
        if (hasTimer)
        {
            timerObj.SetActive(true);
        }
        else
        {
            timerObj.SetActive(false);
        }
    }
}
