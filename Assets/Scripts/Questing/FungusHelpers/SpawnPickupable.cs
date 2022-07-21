using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

[CommandInfo("Questing", "Spawn Pickupable", "")]
public class SpawnPickupable : Command
{
    [SerializeField] private Talkable _npc;
    [SerializeField] private Pickupable _pickupable;

    [VariableProperty(typeof(BooleanVariable))]
    public BooleanVariable ResultSetFlag;

    public override void OnEnter()
    {
        Vector3 npcPos = _npc.transform.position;
        Vector3 newObjPos = npcPos + _npc.transform.forward + Vector3.up;
        _pickupable.transform.position = newObjPos;
        _pickupable.gameObject.SetActive(true);

        ResultSetFlag.Value = true;
        Continue();
    }
}
