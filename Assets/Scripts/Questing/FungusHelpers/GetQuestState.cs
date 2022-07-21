using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;


[CommandInfo("Questing", "Get quest state", "")]
public class GetQuestState : Command
{
    public Quest Quest;

    [VariableProperty(typeof(IntegerVariable))]
    public IntegerVariable Result;

    public override void OnEnter()
    {
        Result.Value = (int) Quest.State;
        Continue();
    }
}
