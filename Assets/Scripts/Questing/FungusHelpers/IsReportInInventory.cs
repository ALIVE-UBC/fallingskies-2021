using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

[CommandInfo("Questing", "Is report in inventory", "")]
public class IsReportInInventory : Command
{
    public InventoryWrapper Inventory;

    [VariableProperty(typeof(BooleanVariable))]
    public BooleanVariable Result;

    public override void OnEnter()
    {
        bool hasReport = Inventory.Inventory.Slots.Exists(slot => slot.Item.IsReport);

        Result.Value = hasReport;
        Continue();
    }
}
