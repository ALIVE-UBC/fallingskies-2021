using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public enum QuestState
{
    NotStarted,
    Started,
    PendingSubmission,
    Completed
}

public abstract class Quest : ScriptableObject
{
    public int Id;
    public string Title;
    public string TitlePendingSubmission;
    [TextArea] public string Description;
    public QuestState State = QuestState.NotStarted;
    public ItemObject[] RewardItems;
    public int[] UnlockingDialogIds;

    public void Reset()
    {
        State = QuestState.NotStarted;
    }
}
