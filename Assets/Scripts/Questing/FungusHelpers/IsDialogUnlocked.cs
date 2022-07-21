using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

[CommandInfo("Questing", "Is dialog unlocked", "")]
public class IsDialogUnlocked : Command
{
    public QuestingController QuestingController;
    public int DiaglogId;

    [VariableProperty(typeof(BooleanVariable))]
    public BooleanVariable Result;

    public override void OnEnter()
    {
        Result.Value = QuestingController.DialogsUnlocked.Contains(DiaglogId);
        Continue();
    }
}
