using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuestItemUi : MonoBehaviour
{
    [SerializeField] private TMP_Text _questTitle;
    [SerializeField] private Image _questIcon;
    [SerializeField] private Sprite _questIconQuestion;
    [SerializeField] private Sprite _questIconQuestionGrey;

    private QuestingUiController _ui;
    private Quest _quest;

    private void Awake()
    {
        _ui = FindObjectOfType<QuestingUiController>();
    }

    public void ButtonClick()
    {
        _ui.ShowQuestPopup(_quest);
    }

    public void SetQuest(Quest quest)
    {
        _quest = quest;
        // Only "Started" and "PendingSubmission" are added.
        if (quest.State == QuestState.PendingSubmission)
        {
            _questIcon.sprite = _questIconQuestion;
            _questTitle.text = quest.TitlePendingSubmission;
        }
        else
        {
            _questIcon.sprite = _questIconQuestionGrey;
            _questTitle.text = quest.Title;
        }
    }
}
