using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InventoryUi : MonoBehaviour
{
    [SerializeField] private InventoryType _type;

    private PopupController _pCtl;
    private InventoryController _invCtl;
    private SubmitButton _submitButton;

    private void Awake()
    {
        _pCtl = FindObjectOfType<PopupController>();
        _invCtl = FindObjectOfType<InventoryController>();
        _submitButton = FindObjectOfType<SubmitButton>();
    }

    // populate computer / microscope / submit inventory with eligible items from player inventory
    public void PopulateCells(InventoryObject playerInventory, InventoryType filterType)
    {
        switch (filterType)
        {
            case InventoryType.Computer:
                ClearInventory();
                foreach (Slot slot in playerInventory.Slots)
                {
                    if (slot.Item.IsComputerCompatible) AddItem(slot.Item);
                }
                break;
            case InventoryType.Microscope:
                ClearInventory();
                foreach (Slot slot in playerInventory.Slots)
                {
                    if (slot.Item.IsMicroscopeCompatible) AddItem(slot.Item);
                }
                break;
            case InventoryType.Submission:
                ClearInventory();
                foreach (Slot slot in playerInventory.Slots)
                {
                    if (slot.Item.IsReport) AddItem(slot.Item);
                }
                break;
            default:
                break;
        }
    }

    public void AddItem(ItemObject newItem)
    {
        // Locate the first available slot.
        foreach (Transform cell in this.transform)
        {
            var itemWrapper = cell.GetComponentInChildren<ItemWrapper>();
            if (!itemWrapper.Item)
            {
                itemWrapper.Item = newItem;
                itemWrapper.UpdateSprite();
                return;
            }
        }

        // Not empty slots left.
        throw new NotImplementedException();
    }

    public void ClearInventory()
    {
        var itemWrappers = this.GetComponentsInChildren<ItemWrapper>();
        foreach (var itemWrapper in itemWrappers)
        {
            itemWrapper.Item = null;
            itemWrapper.UpdateSprite();
        }
    }

    public void UpdateInventory(InventoryObject newInventory)
    {
        ClearInventory();
        foreach (Slot slot in newInventory.Slots)
        {
            AddItem(slot.Item);
        }
    }

    public void OnCellClick(ItemObject item, bool isItemSelected)
    {
        PopupType pType;
        UnityAction action;
        switch (_type)
        {
            case InventoryType.Player:
                pType = item.IsReport ? PopupType.ReportDiscard : PopupType.NormalDiscard;
                action = () => { _invCtl.DiscardItem(item); };
                MetricsUploader.LogEvent(MetricEventType.INSPECT, "ItemId", item.ItemId, "ItemName", item.ItemName);
                break;
            case InventoryType.Computer:
            case InventoryType.Microscope:
                pType = PopupType.NormalAnalyze;
                action = () => { _invCtl.AnalyzeItem(item, _type); };
                break;
            case InventoryType.Submission:
                pType = isItemSelected ? PopupType.ReportDeselect : PopupType.ReportSelect;
                action = () => { _submitButton.ToggleSelection(item); };
                break;
            default:
                throw new NotImplementedException();
        }

        _pCtl
            .GetPopup(pType)
            .SetItem(item)
            .SetButtonPrimary(action)
            .Show();
    }
}
