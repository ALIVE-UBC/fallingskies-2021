using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemWrapper : MonoBehaviour
{
    public ItemObject Item;

    private InventoryUi _ui;

    private Toggle _toggle;

    private void Start()
    {
        _ui = GetComponentInParent<InventoryUi>();
        UpdateSprite();

        // selectable?
        _toggle = GetComponentInParent<Toggle>();
    }

    public void UpdateSprite()
    {
        var image = this.GetComponent<Image>();
        if (Item)
        {
            image.sprite = Item.ItemThumbnail;
            image.enabled = true;
        }
        else
        {
            image.enabled = false;
        }
    }

    public void OnButtonClick()
    {
        bool isItemSelected = false;
        if (_toggle) isItemSelected = _toggle.isOn;
        if (Item) _ui.OnCellClick(Item, isItemSelected);
    }
}
