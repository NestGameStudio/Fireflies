using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class AssignCameraToPlayer : MonoBehaviour
{
    public string playerTag = "Player";
    public CinemachineVirtualCamera CMcamera;

    void Start()
    {
        GameObject playerGO = GameObject.FindGameObjectWithTag(playerTag).gameObject;

        CMcamera.m_Follow = playerGO.transform;
    }

}
