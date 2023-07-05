using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// unload script after first use

public class ToastOnEnter : MonoBehaviour
{
    public string message;
    protected InteractionController IntCtl;
    protected virtual void Awake()
    {
        IntCtl = FindObjectOfType<InteractionController>();
    }

    protected void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            IntCtl.ShowToast(message);
        }
    }
}