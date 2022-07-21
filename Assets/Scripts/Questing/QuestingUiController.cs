using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuestingUiController : MonoBehaviour
{
    [SerializeField] GameObject _questList = default;
    [SerializeField] GameObject _questItem = default;
    [SerializeField] Popup _questPopup = default;
    [SerializeField] private Sprite _questIconQuestion = default;
    [SerializeField] private Sprite _questIconQuestionGrey = default;

    void Start()
    {
        Clear();
    }

    private void Clear()
    {
        foreach (Transform qItem in _questList.transform)
        {
            Destroy(qItem.gameObject);
        }
    }

    public void AddQuest(Quest quest)
    {
        var newItem = Instantiate(_questItem, _questList.transform);
        newItem.GetComponent<QuestItemUi>().SetQuest(quest);
    }

    public void UpdateQuests(List<Quest> quests)
    {
        Clear();
        foreach (Quest q in quests)
        {
            if (q.State == QuestState.Started || q.State == QuestState.PendingSubmission)
            {
                AddQuest(q);
            }
        }
    }

    public void ShowQuestPopup(Quest quest)
    {
        _questPopup.SetTitleDesc(quest.Title, quest.Description)
            .gameObject.SetActive(true);
    }
}
