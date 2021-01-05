using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class CharacterModo : MonoBehaviour
{
    public SpriteRenderer Base;
    public SpriteRenderer Eyes;
    public SpriteRenderer Ears;

    public Color[] baseColor;
    public Color[] eyesColor;
    public Color[] earsColor;

    public enum modo
    {
        fogo,
        terra
    }
    public modo Modo;
    
    // Start is called before the first frame update
    void Start()
    {
        Modo = modo.fogo;
    }

    // Update is called once per frame
    void Update()
    {
        Base.color = baseColor[(int)Modo];
        Eyes.color = eyesColor[(int)Modo];
        Ears.color = earsColor[(int)Modo];
    }
}
