using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum InteractButtonType
{
    Collect,
    Recycle,
    Talk,
    Submit,
    Use,
}

[System.Serializable]
public class InteractButtonConfig
{
    public Sprite Icon;
    public string Title;
}

public class InteractButtonController : MonoBehaviour
{
    [SerializeField] private GameObject _buttonPrefab = default;
    [SerializeField] private Transform _buttonParent = default;

    [Header("Sprites")]
    [SerializeField] private Sprite _collectIcon = default;
    [SerializeField] private Sprite _cleanIcon = default;
    [SerializeField] private Sprite _talkIcon = default;
    [SerializeField] private Sprite _submitIcon = default;
    [SerializeField] private Sprite _useIcon = default;

    private Dictionary<InteractButtonType, InteractButtonConfig> _buttonConfig;
    private Dictionary<Interactable, GameObject> _buttons = new Dictionary<Interactable, GameObject>();

    private void Awake()
    {
        InitButtonConfig();
    }

    private void Start()
    {
        // Destroy existing buttons.
        foreach (InteractButton b in _buttonParent.GetComponentsInChildren<InteractButton>())
        {
            Destroy(b.gameObject);
        }
    }

    private void InitButtonConfig()
    {
        _buttonConfig = new Dictionary<InteractButtonType, InteractButtonConfig>() {
            {InteractButtonType.Collect, new InteractButtonConfig { Icon = _collectIcon, Title = "Collect" } },
            {InteractButtonType.Recycle, new InteractButtonConfig { Icon = _cleanIcon, Title = "Clean" } },
            {InteractButtonType.Talk, new InteractButtonConfig { Icon = _talkIcon, Title = "Talk" } },
            {InteractButtonType.Submit, new InteractButtonConfig { Icon = _submitIcon, Title = "Submit Evidence" } },
            {InteractButtonType.Use, new InteractButtonConfig { Icon = _useIcon, Title = "Use" } },
        };
    }

    public void AddButton(Interactable instance, InteractButtonType type, UnityAction action, InteractButtonConfig config = null)
    {
        if (_buttons.ContainsKey(instance)) return;

        var newButton = Instantiate(_buttonPrefab, _buttonParent);
        var newButtonConfig = config ?? _buttonConfig[type];
        newButton.GetComponent<InteractButton>().SetButton(newButtonConfig, action);
        _buttons.Add(instance, newButton);
    }

    public void RemoveButton(Interactable instance)
    {
        try
        {
            Destroy(_buttons[instance]);
            _buttons.Remove(instance);
        }
        catch (KeyNotFoundException)
        {
        }
    }

    public void RemoveAllButtons()
    {
        foreach (GameObject buttonObj in _buttons.Values)
        {
            Destroy(buttonObj);
        }
        _buttons.Clear();
    }
}
