using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

[CommandInfo("Questing", "Is item in inventory", "")]
public class IsItemInInventory : Command
{
    public InventoryWrapper Inventory;
    public ItemObject Item;

    [VariableProperty(typeof(BooleanVariable))]
    public BooleanVariable Result;

    public override void OnEnter()
    {
        if (Inventory.Inventory.ContainsItem(Item))
        {
            Result.Value = true;
        }
        else
        {
            Result.Value = false;
        }

        Continue();
    }
}
