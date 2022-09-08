using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Fungus;

[RequireComponent(typeof(Button))]
public class SubmitButton : MonoBehaviour
{
    [SerializeField] private InventoryUi _cellsWithToggles = default;

    private UserManager _umgr;
    private InteractionController _intCtl;
    private Button _button;
    private Toggle[] _toggles;
    private SubmissionUi _ui;
    private PopupController _pCtl;

    private void Awake()
    {
        _button = GetComponent<Button>();
        _umgr = FindObjectOfType<UserManager>();
        _intCtl = FindObjectOfType<InteractionController>();
        _toggles = _cellsWithToggles.GetComponentsInChildren<Toggle>();
        _pCtl = FindObjectOfType<PopupController>();
    }

    private void OnEnable()
    {
        ClearSelection();
        _ui = FindObjectOfType<SubmissionUi>();
    }

    private void Start()
    {
        UpdateButtonStatus();
    }


    private void UpdateButtonStatus()
    {
        foreach (Toggle t in _toggles)
        {
            if (t.isOn)
            {
                _button.interactable = true;
                return;
            }
        }
        _button.interactable = false;
    }

    public void ToggleSelection(ItemObject item)
    {
        foreach (Toggle t in _toggles)
        {
            if (t.GetComponentInChildren<ItemWrapper>().Item == item)
            {
                t.isOn = !t.isOn;
                break;
            }
        }
        UpdateButtonStatus();
    }

    public void ClearSelection()
    {
        foreach (Toggle t in _toggles) t.isOn = false;
    }

    public void SubmitEvidence()
    {
        _pCtl
            .GetPopup(PopupType.GenericYesNo)
            .SetTitleDesc("Attention", "Are you sure to submit?")
            .SetButtonPrimary(DoSubmitEvidence)
            .Show();
    }

    private void DoSubmitEvidence()
    {
        var items = new List<ItemObject>();
        foreach (Toggle t in _toggles)
        {
            if (t.isOn)
            {
                var item = t.GetComponentInChildren<ItemWrapper>().Item;
                items.Add(item);
            }
        }

        // handle final results
        foreach (var item in items)
        {
            MetricsUploader.LogEvent(MetricEventType.ASSESSMENT_UPDATE, "ItemId", item.ItemId);
        }

        // show feedback screen
        _ui.ShowFeedbackSection()
            .SetStoryText("Thank you! Can you tell me more about your ideas?");
    }

    public void SubmitFeedback()
    {
        // handle feedback
        string feedback = _ui.Feedback.text;
        MetricsUploader.LogEvent(MetricEventType.ASSESSMENT_UPDATE, "Feedback", feedback);

        // close the submission screen
        _ui.gameObject.SetActive(false);

        MetricsUploader.LogEvent(MetricEventType.ASSESSMENT_END);

        Flowchart.BroadcastFungusMessage("9999");

        MetricsUploader.LogEvent(MetricEventType.GAME_END);
    }
}
