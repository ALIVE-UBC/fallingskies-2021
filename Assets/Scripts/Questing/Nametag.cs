using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Nametag : MonoBehaviour
{
    private TMP_Text _nameText;
    private SpriteRenderer _icon;
    private Transform _camera;

    void Awake()
    {
        _nameText = GetComponentInChildren<TMP_Text>();
        _icon = GetComponentInChildren<SpriteRenderer>();
        _camera = Camera.main.transform;
    }

    void Update()
    {
        LookAtCamera();
    }

    void LookAtCamera()
    {
        this.transform.LookAt(_camera);
        this.transform.Rotate(0, 180, 0);
    }

    public void UpdateName(string text)
    {
        if (!_nameText) return;
        _nameText.text = text;
    }

    public void UpdateIcon(Sprite icon)
    {
        if (!_icon) return;
        _icon.sprite = icon;
        _icon.enabled = true;
    }

    public void HideIcon()
    {
        if (!_icon) return;
        _icon.enabled = false;
    }
}
