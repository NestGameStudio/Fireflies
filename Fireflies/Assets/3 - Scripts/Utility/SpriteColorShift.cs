using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteColorShift : MonoBehaviour
{
    private SpriteRenderer sr;
    [SerializeField] [Range(0f,3f)] float lerpTime = 1f;
    [SerializeField] Color[] myColors;
    int colorIndex = 0;
    float t = 0f;
    int len = 0;
    private Color cColor;


    void Start()
    {
        len = myColors.Length;
        sr = GetComponent<SpriteRenderer>();
        cColor = myColors[colorIndex];
    }

    
    void Update()
    {

        sr.color = Color.Lerp(cColor, myColors[colorIndex], t);

        t = Mathf.Lerp(t, 1f, lerpTime * Time.deltaTime);
        if(t>.9f)
        {
            t = 0f;
            cColor = myColors[colorIndex];
            colorIndex++;
            colorIndex = (colorIndex >= len) ? 0 : colorIndex;
        }

    }
}
