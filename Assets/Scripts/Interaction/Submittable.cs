using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

public class Submittable : Interactable
{
    [SerializeField] private string _submitFungusMessage = default;

    protected override void Awake()
    {
        base.Awake();
        ButtonType = InteractButtonType.Submit;
    }

    public override void HandleInteraction()
    {
        Flowchart.BroadcastFungusMessage(_submitFungusMessage);
        IntCtl.InteractButton.RemoveAllButtons();
    }
}
