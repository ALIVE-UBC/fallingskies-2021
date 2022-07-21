using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public enum PopupType
{
    NormalDiscard,
    NormalAnalyze,
    ReportDiscard,
    ReportGet,
    ReportSelect,
    ReportDeselect,
    GenericYesNo
}

[Serializable]
public struct PopupConfig
{
    public PopupType Type;
    public Popup Popup;
}

public class PopupController : MonoBehaviour
{
    public PopupConfig[] PopupConfigs;

    private Dictionary<PopupType, PopupConfig> _popupConfigs = new Dictionary<PopupType, PopupConfig>();

    private void Awake()
    {
        InitConfig();
    }

    private void InitConfig()
    {
        foreach (var pc in PopupConfigs)
        {
            _popupConfigs.Add(pc.Type, pc);
        }
    }

    public Popup GetPopup(PopupType type)
    {
        return _popupConfigs[type].Popup;
    }
}
