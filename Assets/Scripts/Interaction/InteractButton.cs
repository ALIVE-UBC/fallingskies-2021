using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

[RequireComponent(typeof(Button))]
public class InteractButton : MonoBehaviour
{
    [SerializeField] private Image _icon = default;
    [SerializeField] private TMP_Text _title = default;

    private Button _button;

    private void Awake()
    {
        _button = GetComponent<Button>();
    }

    public void SetButton(InteractButtonConfig config, UnityAction action)
    {
        _icon.sprite = config.Icon;
        _title.text = config.Title;
        _button.onClick.RemoveAllListeners();
        _button.onClick.AddListener(action);
    }
}
