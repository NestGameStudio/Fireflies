using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtFeedback : MonoBehaviour
{
    [Header("Cor ao tomar dano")]
    public Color hurtColor;

    [Header("Velocidade da mudança de cor")]
    public float lerpTime;
    private float lerpControl = 0;

    private Color baseColor, earsColor;
    private GameObject Base, Ears;

    [HideInInspector]
    public bool isHurt = false;

    void Start()
    {
        Ears = gameObject.transform.GetChild(0).transform.GetChild(0).gameObject;
        Base = gameObject.transform.GetChild(0).transform.GetChild(1).gameObject;
    }

    void Update()
    {
        if (isHurt)
        {
            Hurt();
        }
    }

    public void HurtTrigger()
    {
        earsColor = Ears.GetComponent<SpriteRenderer>().color;
        baseColor = Base.GetComponent<SpriteRenderer>().color;
        isHurt = true;
        lerpControl = 0;
    }
    
    void Hurt()
    {
        Ears.GetComponent<SpriteRenderer>().color = Color.Lerp(hurtColor, earsColor, lerpControl);
        Base.GetComponent<SpriteRenderer>().color = Color.Lerp(hurtColor, baseColor, lerpControl);
        
        if (lerpControl < 1)
        {
            gameObject.GetComponent<Animator>().enabled = false;
            lerpControl += Time.deltaTime/lerpTime;
        }
        else
        {
            gameObject.GetComponent<Animator>().enabled = true;
            isHurt = false;
        }
    }
}
