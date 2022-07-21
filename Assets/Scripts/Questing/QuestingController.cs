using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestingController : MonoBehaviour
{
    public List<Quest> Quests;
    public List<int> DialogsUnlocked;

    [SerializeField] QuestingUiController _questListUi = default;
    [SerializeField] InventoryWrapper _playerInventory = default;
    [SerializeField] InventoryWrapper _recycleInventory = default;

    private InteractionController _intCtl = default;

    private void Awake()
    {
        _intCtl = FindObjectOfType<InteractionController>();
    }

    private void Start()
    {
        InvokeRepeating(nameof(CheckAllQuests), 1f, 0.1f);
    }

    private void UpdateUi()
    {
        _questListUi.UpdateQuests(Quests);
    }

    // Iterate through all in progress quests and update their states.
    public void CheckAllQuests()
    {
        bool isChanged = false;
        foreach (Quest quest in Quests)
        {
            if (quest.State == QuestState.Started && IsQuestConditionMet(quest))
            {
                quest.State = QuestState.PendingSubmission;
                isChanged = true;
            }
            else if (quest.State == QuestState.PendingSubmission && !IsQuestConditionMet(quest))
            {
                quest.State = QuestState.Started;
                isChanged = true;
            }
        }
        if (isChanged) UpdateUi();
    }

    public bool IsQuestConditionMet(Quest quest)
    {
        bool isQuestConditionMet = false;

        // fetch quests
        if (quest.GetType() == typeof(FetchQuest))
        {
            if (Array.TrueForAll(((FetchQuest) quest).RequiredItems, item => _playerInventory.Inventory.ContainsItem(item)))
            {
                isQuestConditionMet = true;
            }
        }
        // recycle quests
        else if (quest.GetType() == typeof(RecycleQuest))
        {
            if (Array.TrueForAll(((RecycleQuest) quest).RecycledItems, item => _recycleInventory.Inventory.ContainsItem(item)))
            {
                isQuestConditionMet = true;
            }
        }
        else
        {
            throw new NotImplementedException();
        }

        return isQuestConditionMet;
    }

    public void StartQuest(Quest quest)
    {
        quest.State = QuestState.Started;
        Quests.Add(quest);
        _intCtl.ShowToast("Quest accepted!");
        UpdateUi();
    }

    public void CompleteQuest(Quest quest)
    {
        Debug.Assert(quest.State == QuestState.PendingSubmission);

        // submit items (remove items from player inventory)
        if (quest.GetType() == typeof(FetchQuest))
        {
            foreach (ItemObject item in ((FetchQuest) quest).RequiredItems)
            {
                _playerInventory.RemoveItem(item);
            }
        }

        // rewards
        //foreach (ItemObject item in quest.RewardItems)
        //{
        //    _playerInventory.AddItem(item);
        //}
        // rewards are handled by Fungus now

        // unlocking dialogs
        foreach (int dialogId in quest.UnlockingDialogIds)
        {
            DialogsUnlocked.Add(dialogId);
        }

        quest.State = QuestState.Completed;
        _intCtl.ShowToast("Quest completed!");
        UpdateUi();
    }

    private void OnApplicationQuit()
    {
        // Prevent runtime changes from saving to disk.
        foreach (Quest quest in Quests)
        {
            quest.Reset();
        }
    }
}
