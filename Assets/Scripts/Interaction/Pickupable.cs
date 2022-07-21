using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pickupable : Interactable
{
    public ItemObject Item;
    public bool IsRecyclable;

    protected override void Awake()
    {
        base.Awake();
        ButtonType = IsRecyclable ? InteractButtonType.Recycle : InteractButtonType.Collect;
    }

    private void Start()
    {
        UpdateName();
    }

    private void UpdateName()
    {
        if (Item) Nametag.UpdateName(Item.ItemName);
    }

    public override void HandleInteraction()
    {
        if (!IsPlayerInRange) return;

        if (IsRecyclable)
        {
            HandleInteractionRecycle();
        }
        else
        {
            HandleInteractionPickup();
            MetricsUploader.LogEvent(MetricEventType.BACKPACK_ADD, "ItemId", Item.ItemId, "ItemName", Item.ItemName);
        }

        IntCtl.InteractButton.RemoveButton(this);
    }

    private void HandleInteractionPickup()
    {
        if (IntCtl.PlayerInventory.Inventory.Slots.Count >= 8)
        {
            IntCtl.ShowToast("Inventory full.");
            return;
        }

        IntCtl.PlayerInventory.AddItem(Item, this.gameObject);
        IntCtl.ShowToast($"{Item.ItemName} +1");
        this.gameObject.SetActive(false);
    }

    private void HandleInteractionRecycle()
    {
        IntCtl.RecycleInventory.AddItem(Item);
        Destroy(this.gameObject);
    }
}
