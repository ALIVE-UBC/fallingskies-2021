using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public enum InventoryType
{
    Player,
    Computer,
    Microscope,
    Recycle,
    Submission
}

public class InventoryWrapper : MonoBehaviour
{
    public InventoryObject Inventory;
    public InventoryUi InventoryUi;

    [Header("Internal data")]
    public Dictionary<int, GameObject> GoRefs = new Dictionary<int, GameObject>();

    private InteractionController _intCtl;

    private void Start()
    {
        _intCtl = FindObjectOfType<InteractionController>();
    }

    public void AddItem(ItemObject item)
    {
        Inventory.AddItem(item, 1);
        UpdateUi();
    }

    public void AddItem(ItemObject item, GameObject gameObjectRef)
    {
        GoRefs.Add(item.ItemId, gameObjectRef);
        AddItem(item);
    }

    public void RemoveItem(ItemObject item)
    {
        Inventory.RemoveItem(item, 1);
        UpdateUi();
    }

    private void OnApplicationQuit()
    {
        Inventory.Slots.Clear();
    }

    private void UpdateUi()
    {
        if (InventoryUi) InventoryUi.UpdateInventory(this.Inventory);
    }
}
