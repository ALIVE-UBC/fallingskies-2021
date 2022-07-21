using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Useable : Interactable
{
    [SerializeField] private string _name = default;
    [SerializeField] private Sprite _icon = default;

    protected override void Awake()
    {
        base.Awake();
        ButtonType = InteractButtonType.Use;
        ButtonConfig = new InteractButtonConfig { Icon = _icon, Title = $"Use {_name}"};
    }

    public override void HandleInteraction()
    {
        // FIXME: a better way?
        string methodName = $"Show{_name}Screen";
        var method = IntCtl.GetType().GetMethod(methodName);
        if (method != null) method.Invoke(IntCtl, null);
    }
}
