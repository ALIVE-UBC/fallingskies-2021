using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Events;

public class Popup : MonoBehaviour
{
    [SerializeField] TMP_Text _title = default;
    [SerializeField] TMP_Text _desc = default;
    [SerializeField] Image _thumbnail = default;
    [SerializeField] private Button[] _buttons = default;

    public Popup SetTitleDesc(string title, string desc)
    {
        _title.text = title;
        _desc.text = desc;
        return this;
    }

    public Popup SetItem(ItemObject item)
    {
        _title.text = item.ItemName;
        _desc.text = item.ItemDescription;
        _thumbnail.sprite = item.ItemThumbnail;
        return this;
    }

    public Popup SetButton(int index, UnityAction action, bool closePopup = true)
    {
        Button button = _buttons[index];
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(action);
        if (closePopup) button.onClick.AddListener(Hide);
        return this;
    }

    public Popup SetButtonPrimary(UnityAction action)
    {
        return SetButton(0, action);
    }

    public void Show()
    {
        this.gameObject.SetActive(true);
    }

    public void Hide()
    {
        this.gameObject.SetActive(false);
    }
}
