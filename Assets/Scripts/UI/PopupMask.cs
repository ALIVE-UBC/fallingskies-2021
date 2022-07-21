using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class PopupMask : MonoBehaviour
{
    public Color MaskColour;
    private Popup[] _allPopups;
    private Image _mask;

    private void Start()
    {
        _allPopups = GetComponentsInChildren<Popup>(includeInactive:true);
        _mask = GetComponent<Image>();

        // disable all active popups on start
        foreach (Popup p in _allPopups)
        {
            p.gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        foreach (Popup p in _allPopups)
        {
            if (p.gameObject.activeSelf)
            {
                EnableMask();
                return;
            }
        }
        // no active popups
        DisableMask();
    }

    public void EnableMask()
    {
        _mask.raycastTarget = true;
        _mask.color = MaskColour;
        _mask.enabled = true;
    }

    public void DisableMask()
    {
        _mask.enabled = false;
    }
}
