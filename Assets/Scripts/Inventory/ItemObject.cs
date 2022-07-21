using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

[CreateAssetMenu]
public class ItemObject : ScriptableObject
{
    public int ItemId;
    public int ItemStackSize = 1;
    public string ItemName;
    public bool IsReport;
    [TextArea]
    public string ItemDescription;
    public Sprite ItemThumbnail;
    public bool IsComputerCompatible;
    public bool IsMicroscopeCompatible;
    public ItemObject ReportItem;
}
