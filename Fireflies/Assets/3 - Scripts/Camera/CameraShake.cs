using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraShake : MonoBehaviour
{
    public static CameraShake instance { get; private set; }

    private CinemachineVirtualCamera cinemachineVC;
    private CinemachineBasicMultiChannelPerlin cinemachineNoise;

    void Awake()
    {
        instance = this;
        cinemachineVC = GetComponent<CinemachineVirtualCamera>();
        cinemachineNoise = cinemachineVC.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

        //Iniciar com zero shake
        cinemachineNoise.m_AmplitudeGain = 0;
        cinemachineNoise.m_FrequencyGain = 0;
    }

    public void CameraFollow() {
        cinemachineVC.m_Follow = GameObject.FindGameObjectWithTag("Player").transform;
    }
    public void shakeCam(float intensity, float frequency , float time){
        //Debug.Log("Starting shake...");
        StartCoroutine(shake(intensity, frequency, time));
    }

    private IEnumerator shake(float intensity, float frequency , float time){
        cinemachineNoise.m_AmplitudeGain = intensity;
        cinemachineNoise.m_FrequencyGain = frequency;
        
        yield return new WaitForSecondsRealtime(time);
        
        //acabou tempo de shake
        cinemachineNoise.m_AmplitudeGain = 0;
        cinemachineNoise.m_FrequencyGain = 0;
        //Debug.Log("Shake finished");
        yield return 0;
    }
}
