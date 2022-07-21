using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

[CommandInfo("Questing", "Start quest", "")]
public class StartQuest : Command
{
    public Quest Quest;

    private QuestingController _qCtl;

    public override void OnEnter()
    {
        _qCtl = FindObjectOfType<QuestingController>();

        _qCtl.StartQuest(Quest);
        Continue();
    }
}
