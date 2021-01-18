using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleConsole : MonoBehaviour
{
    [Header("Key to toggle:")]
    public KeyCode tecla = KeyCode.L;
    
    private GameObject consoleObject;
    private bool active = false;
    // Start is called before the first frame update
    void Start()
    {
        consoleObject = transform.GetChild(0).gameObject;
        consoleObject.SetActive(false);
        active = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            active = !active;
            consoleObject.SetActive(active);
        }
    }

}
