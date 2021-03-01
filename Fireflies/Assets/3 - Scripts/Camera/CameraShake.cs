using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraShake : MonoBehaviour
{
    public static CameraShake instance { get; private set; }

    private CinemachineVirtualCamera cinemachineVC;
    private float shakeTimer;
    private float startingIntensity;
    private float shakeTimerTotal;
    //private float shakeFrequency;

    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
        cinemachineVC = GetComponent<CinemachineVirtualCamera>();
    }

    public void CameraFollow() {
        cinemachineVC.m_Follow = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public void shakeCam(float intensity, float frequency , float time)
    {
        CinemachineBasicMultiChannelPerlin cinemachineNoise = cinemachineVC.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

        cinemachineNoise.m_AmplitudeGain = intensity;
        cinemachineNoise.m_FrequencyGain = frequency;

        startingIntensity = intensity;
        shakeTimer = time;
        shakeTimerTotal = time;
        
    }

    private void Update()
    {
        if(shakeTimer > 0)
        {
            shakeTimer -= Time.unscaledDeltaTime;
            if(shakeTimer <= 0)
            {
                //acabou tempo de shake
                CinemachineBasicMultiChannelPerlin cinemachineNoise = cinemachineVC.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

                cinemachineNoise.m_AmplitudeGain = Mathf.Lerp(startingIntensity, 0, 1 - (shakeTimer / shakeTimerTotal));

            }
        }
    }
}
