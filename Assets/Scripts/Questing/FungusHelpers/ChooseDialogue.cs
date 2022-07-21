using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using Fungus;

[System.Serializable]
public struct QuestDialogue
{
    public Quest Quest;
    public int DialogueIdNotStarted;
    public int DialogueIdStarted;
    public int DialogueIdPendingSubmission;
}

[System.Serializable]
public struct ImportantDialogue
{
    public bool IsLocked;
    public int EasyId;
    public int NormalId;
    public int HardId;
}

[CommandInfo("Questing", "Choose dialogue", "")]
public class ChooseDialogue : Command
{
    [Header("Quest-related")]
    public List<QuestDialogue> QuestDialogues;

    [Header("Important")]
    public List<ImportantDialogue> ImportantDialogues;

    [Header("Casual")]
    public List<int> CasualDialogueIds;

    private QuestingController _qCtl;
    private UserManager _umgr;

    public override void OnEnter()
    {
        _qCtl = FindObjectOfType<QuestingController>();
        _umgr = FindObjectOfType<UserManager>();

        var random = new System.Random();
        int dialogueId = 0;
        string userGroup = (string) _umgr.Read("UserGroup", "1");

        // Use quest related dialogues if there are any.
        foreach (QuestDialogue qd in QuestDialogues)
        {
            // Choose quest-related dialogue based on quest state.
            dialogueId = qd.Quest.State switch
            {
                QuestState.NotStarted => qd.DialogueIdNotStarted,
                QuestState.Started => qd.DialogueIdStarted,
                QuestState.PendingSubmission => qd.DialogueIdPendingSubmission,
                QuestState.Completed => 0,
                _ => 0
            };

            // If the quest is in pending submission state, complete it.
            if (qd.Quest.State == QuestState.PendingSubmission) _qCtl.CompleteQuest(qd.Quest);
            if (dialogueId != 0) break;
        }


        // If there are no quest-related dialogues, pop an unlocked important dialogue.
        if (dialogueId == 0)
        {
            //foreach (ImportantDialogue d in ImportantDialogues)
            //{
            //    int did = userGroup switch
            //    {
            //        "1" => d.EasyId,
            //        "2" => d.NormalId,
            //        "3" => d.HardId,
            //        _ => d.EasyId
            //    };
            //    if (d.IsLocked)
            //    {
            //        if (_qCtl.DialogsUnlocked.Contains(did))
            //        {
            //            dialogueId = did;
            //            ImportantDialogues.Remove(d);
            //            break;
            //        }
            //    }
            //    else
            //    {
            //        dialogueId = did;
            //        ImportantDialogues.Remove(d);
            //        break;
            //    }
            //}
            if (ImportantDialogues.Count > 0)
            {
                var d = ImportantDialogues[0];
                int did = userGroup switch
                {
                    "1" => d.EasyId,
                    "2" => d.NormalId,
                    "3" => d.HardId,
                    _ => d.EasyId
                };
                if (!d.IsLocked || _qCtl.DialogsUnlocked.Contains(did))
                {
                    dialogueId = did;
                    ImportantDialogues.RemoveAt(0);
                }
            }
        }

        // Fallback to casual dialogues.
        if (dialogueId == 0)
        {
            dialogueId = CasualDialogueIds[random.Next(CasualDialogueIds.Count)];
        }

        string fungusMessage = dialogueId.ToString();
        MetricsUploader.LogEvent(MetricEventType.TALK, "FungusMessage", fungusMessage);
        Flowchart.BroadcastFungusMessage(fungusMessage);
        Continue();
    }
}
