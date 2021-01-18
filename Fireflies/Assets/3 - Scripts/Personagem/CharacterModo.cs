using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class CharacterModo : MonoBehaviour
{
    public SpriteRenderer Base;
    public SpriteRenderer Eyes;
    public SpriteRenderer Ears;

    [Header("Cores de cada modo - seguindo index do dropdown de Modo")]
    public Color[] baseColor;
    public Color[] eyesColor;
    public Color[] earsColor;

    //enum que dita os modos - caso queira adicionar mais modos deve tambem aumentar os arrays de cor
    public enum modo
    {
        fogo,
        terra
    }
    [Header("Escolha aqui o modo de Cali")]
    public modo Modo;
    
    // Start is called before the first frame update
    void Start()
    {
        Modo = modo.fogo;
    }

    // Update is called once per frame
    void Update()
    {
        //Aqui a cor pra cada parte da cali é colocada
        Base.color = baseColor[(int)Modo];
        Eyes.color = eyesColor[(int)Modo];
        Ears.color = earsColor[(int)Modo];
    }
}
