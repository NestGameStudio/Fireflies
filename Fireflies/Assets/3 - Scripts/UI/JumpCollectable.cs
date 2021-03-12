﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpCollectable : MonoBehaviour
{
    private JumpRecovery Jump;
    private GameObject Used;
    private bool canUse = true;

    [Header("Segundos para ser reconstituído")]
    public int resetTime = 10;

    void Start()
    {
        Jump = GameObject.Find("Movement").GetComponent<JumpRecovery>();
        Used = gameObject.transform.GetChild(1).gameObject;
    }

    private void OnTriggerEnter2D(Collider2D collision) {

        if (collision.transform.CompareTag("Player") && canUse == true) 
        {
            canUse = false;
            Jump.setJump(true);
            Used.SetActive(true);
            StartCoroutine(Reset(resetTime));
        }
    }

    IEnumerator Reset(int value)
    {
        yield return new WaitForSeconds(value);
        canUse = true;
        Used.SetActive(false);
    }

}