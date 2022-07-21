using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Interactable : MonoBehaviour
{
    protected InteractButtonType ButtonType;
    protected InteractButtonConfig ButtonConfig = null;

    protected Nametag Nametag;
    protected InteractionController IntCtl;

    protected bool IsPlayerInRange = false;

    protected virtual void Awake()
    {
        IntCtl = FindObjectOfType<InteractionController>();
        Nametag = GetComponentInChildren<Nametag>();
    }

    protected void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            IsPlayerInRange = true;
            IntCtl.InteractButton.AddButton(this, ButtonType, HandleInteraction, ButtonConfig);
        }
    }

    protected void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            IsPlayerInRange = false;
            IntCtl.InteractButton.RemoveButton(this);
        }
    }

    public virtual void HandleInteraction()
    {
        throw new NotImplementedException();
    }
}
