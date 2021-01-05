using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Bleeper_DisplayColor : MonoBehaviour
{
    public BleeperBehaviour behaviourScript;

    public GameObject debugObject;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        behaviourScript.mudarCor();

        if (behaviourScript.Debug)
        {
            debugObject.SetActive(true);
        }
        else
        {
            debugObject.SetActive(false);
        }
    }
}
