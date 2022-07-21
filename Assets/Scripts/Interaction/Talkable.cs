using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

[RequireComponent(typeof(Character))]
public class Talkable : Interactable
{
    [SerializeField] Quest[] _quests = default;
    [SerializeField] string _fungusMessage = default;

    [Header("Icons")]
    [SerializeField] Sprite _exclaimationIcon = default;
    [SerializeField] Sprite _questionIcon = default;
    [SerializeField] Sprite _questionGreyIcon = default;

    private Character _character;
    private InteractionController _intCtl;
    private Animator _animator;

    protected override void Awake()
    {
        base.Awake();
        _character = GetComponent<Character>();
        _animator = this.transform.parent.GetComponentInChildren<Animator>();
        ButtonType = InteractButtonType.Talk;
    }

    private void Update()
    {
        if (!Nametag) return;
        UpdateName();
        UpdateIcon();
    }

    private void UpdateName()
    {
        Nametag.UpdateName(_character.NameText);
    }

    private void UpdateIcon()
    {
        bool hasQuests = false;
        bool hasStartedQuests = false;
        bool hasPendingQuests = false;

        foreach (Quest q in _quests)
        {
            switch (q.State)
            {
                case QuestState.NotStarted:
                    hasQuests = true;
                    break;
                case QuestState.Started:
                    hasStartedQuests = true;
                    break;
                case QuestState.PendingSubmission:
                    hasPendingQuests = true;
                    break;
            }
        }


        // has pending
        if (hasPendingQuests)
        {
            Nametag.UpdateIcon(_questionIcon);
        }
        // no pending
        else
        {
            // ... but has started quests
            if (hasStartedQuests)
            {
                Nametag.UpdateIcon(_questionGreyIcon);
            }
            // ... no pending + no started, but has quests
            else
            {
                if (hasQuests)
                {
                    Nametag.UpdateIcon(_exclaimationIcon);
                }
                // has nothing
                else
                {
                    Nametag.HideIcon();
                }
            }
        }
    }

    public override void HandleInteraction()
    {
        if (IsPlayerInRange)
        {
            MetricsUploader.LogEvent(MetricEventType.TALK, "FungusMessage", _fungusMessage);
            Flowchart.BroadcastFungusMessage(_fungusMessage);
            // Remove all interact buttons before a conversation.
            IntCtl.InteractButton.RemoveAllButtons();
            PlayTalkingAnimation();
        }
    }

    private void PlayTalkingAnimation()
    {
        if (_animator == null) return;

        _animator.SetBool("isTalking", true);
        // Stop talking animation after a few seconds.
        Invoke(nameof(StopTalkingAnimation), 5f);
    }

    private void StopTalkingAnimation()
    {
        _animator.SetBool("isTalking", false);
    }
}
