using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SubmissionUi : MonoBehaviour
{
    public TMP_Text FinalClaim;
    public TMP_Text StoryText;
    public GameObject EvidenceSection;
    public GameObject FeedbackSection;
    public TMP_InputField Feedback;

    private UserManager _umgr;
    private InventoryController _invCtl;
    private InventoryUi _invUi;

    private void Awake()
    {
        _umgr = FindObjectOfType<UserManager>();
        _invCtl = FindObjectOfType<InventoryController>();
    }

    private void OnEnable()
    {
        this.ShowEvidenceSection().UpdateFinalClaim();
        _invUi = GetComponentInChildren<InventoryUi>();
        _invUi.PopulateCells(_invCtl.PlayerInventory.Inventory, InventoryType.Submission);
    }

    private void UpdateFinalClaim()
    {
        string claim = (string) _umgr.Read("FinalClaim", "FinalClaim is not set");
        SetFinalClaim(claim);
        MetricsUploader.LogEvent(MetricEventType.ASSESSMENT_UPDATE, "FinalClaim", claim);
    }

    public SubmissionUi SetFinalClaim(string text)
    {
        FinalClaim.text = text;
        return this;
    }

    public SubmissionUi SetStoryText(string text)
    {
        StoryText.text = text;
        return this;
    }

    public SubmissionUi ShowEvidenceSection()
    {
        EvidenceSection.SetActive(true);
        FeedbackSection.SetActive(false);
        return this;
    }

    public SubmissionUi ShowFeedbackSection()
    {
        FeedbackSection.SetActive(true);
        EvidenceSection.SetActive(false);
        return this;
    }
}
