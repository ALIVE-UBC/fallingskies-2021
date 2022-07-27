using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    [Header("Inventory")]
    public InventoryWrapper PlayerInventory;
    public InventoryWrapper RecycleInventory;
    public InventoryWrapper ComputerInventory;
    public InventoryWrapper MicroscopeInventory;

    [Header("Misc")]
    public GameObject DefaultItemCube;
    public GameObject PickupableRoot;

    private Transform _playerPos;
    private PopupController _pCtl;
    private InteractionController _intCtl;

    private void Awake()
    {
        _pCtl = FindObjectOfType<PopupController>();
        _intCtl = FindObjectOfType<InteractionController>();
    }

    private void Start()
    {
        _playerPos = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public void DiscardItem(ItemObject item)
    {
        PlayerInventory.RemoveItem(item);

        // Restore the game object.
        GameObject origGameObject;
        if (!PlayerInventory.GoRefs.ContainsKey(item.ItemId))
        {
            origGameObject = Instantiate(DefaultItemCube, PickupableRoot.transform);
            var pickupable = origGameObject.GetComponent<Pickupable>();
            pickupable.Item = item;
        }
        else
        {
            origGameObject = PlayerInventory.GoRefs[item.ItemId];
            PlayerInventory.GoRefs.Remove(item.ItemId);
        }

        // Move the object to the player's current position.
        origGameObject.transform.position = _playerPos.position + _playerPos.forward + new Vector3(0,2,0);
        origGameObject.SetActive(true);

        MetricsUploader.LogEvent(MetricEventType.BACKPACK_DISCARD, "ItemId", item.ItemId, "ItemName", item.ItemName);
    }

    public void AnalyzeItem(ItemObject item, InventoryType sourceType)
    {
        MetricsUploader.LogEvent(MetricEventType.LAB_TEST, "ItemId", item.ItemId, "ItemName", item.ItemName);

        // Show "Get the report" popup
        _pCtl
            .GetPopup(PopupType.ReportGet)
            .SetItem(item.ReportItem)
            .SetButtonPrimary(() =>
            {
                // "Get the Report" button
                PlayerInventory.RemoveItem(item);
                PlayerInventory.AddItem(item.ReportItem);
                _intCtl.ShowToast($"Congratulations! {item.ItemName} +1");
                _intCtl.RefreshAnalysisInventory();
            })
            .Show();
    }
}
