using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Slot
{
    public ItemObject Item;
    public int Count;

    public Slot(ItemObject item, int count)
    {
        Item = item;
        Count = count;
    }
}
