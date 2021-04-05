using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraZoom : MonoBehaviour
{
    public CinemachineVirtualCamera CMcamera;
    public float min = 7;
    public float max = 3;
    public float speed = 1.0f;
    public float waitTime = 1.0f;
    static float t = 0.0f;
    private int defaultSize = 7;
    private int count = 0;
    private bool canZoom = false;

    void Update()
    {
        if(canZoom)
        {
            DeathZoom();
        } 
        else
        {
            t = 0.0f;
        }
    }

    public void DeathZoomTrigger()
    {
        canZoom = true;
    }

    void DeathZoom()
    {
        if(count == 0)
        {
            CMcamera.m_Lens.OrthographicSize = Mathf.Lerp(min, max, t);
            t += speed * Time.unscaledDeltaTime;
            if (t > 1.0f)
            {
                StartCoroutine(WaitAndZoomOut());              
            }
        } 
        else 
        {
            CMcamera.m_Lens.OrthographicSize = Mathf.Lerp(max, min, t);
            t += speed * Time.unscaledDeltaTime;
            if (t > 1.0f)
            {        
                count = 0;
                canZoom = false;
            }
        }
    }

    void Revert()
    {
        CMcamera.m_Lens.OrthographicSize = Mathf.Lerp(max, min, t);
        t += speed * Time.unscaledDeltaTime;
        if (t > 1.0f)
        {
            canZoom = false;
            count = 0;
            t = 0.0f;
        }
    }

    IEnumerator WaitAndZoomOut()
    {
        yield return new WaitForSecondsRealtime(waitTime);
        count++;
        t = 0.0f;  
    }
}
