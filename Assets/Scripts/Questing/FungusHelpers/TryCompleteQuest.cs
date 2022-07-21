using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

[CommandInfo("Questing", "Try complete quest", "")]
public class TryCompleteQuest : Command
{
    public Quest Quest;

    [VariableProperty(typeof(IntegerVariable))]
    public IntegerVariable Result;

    private QuestingController _qCtl;

    public override void OnEnter()
    {
        _qCtl = FindObjectOfType<QuestingController>();

        if (Quest.State == QuestState.PendingSubmission) _qCtl.CompleteQuest(Quest);

        Result.Value = (int) Quest.State;
        Continue();
    }
}

