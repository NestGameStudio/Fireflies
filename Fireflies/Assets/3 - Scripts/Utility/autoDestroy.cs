using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class autoDestroy : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        ///destroy after 3 seconds
        Destroy(gameObject, 3);
    }

}
