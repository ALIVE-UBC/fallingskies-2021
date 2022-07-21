using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class InventoryObject : ScriptableObject
{
    public List<Slot> Slots;

    public void AddItem(ItemObject item, int count)
    {
        foreach (Slot slot in Slots)
        {
            if (slot.Item == item)
            {
                slot.Count += count;
                break;
            }
        }

        // No items of the same type in the inventory
        var newSlot = new Slot(item, count);
        Slots.Add(newSlot);
    }

    public void RemoveItem(ItemObject item, int count)
    {
        foreach (Slot slot in Slots)
        {
            if (slot.Item == item)
            {
                Slots.Remove(slot);
                break;
            }
        }
    }

    public bool ContainsItem(ItemObject item)
    {
        foreach (Slot slot in Slots)
        {
            if (slot.Item == item)
            {
                return true;
            }
        }
        return false;
    }
}
