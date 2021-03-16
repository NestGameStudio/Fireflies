using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class breakable : MonoBehaviour
{
    public Animator anim;
    public float hp = 10;
    public float reward = 10;

    // Start is called before the first frame update
    void Start()
    {
        if(anim == null) GetComponentInChildren<Animator>();
    }

}
