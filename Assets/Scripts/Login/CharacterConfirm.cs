using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CharacterConfirm : MonoBehaviour
{
    [SerializeField] private int _characterId = -1;
    [SerializeField] private ToggleGroup _toggleGroup = default;

    private Button _button;
    private UserManager _userManager;
    private SceneLoader _sceneLoader;

    public void SetSelectedChracter(int characterId)
    {
        _characterId = characterId;

        // If any character is selected, make this button clickable.
        if (_toggleGroup.AnyTogglesOn())
        {
            _button.interactable = true;
        }
        else
        {
            _button.interactable = false;
        }
    }

    public void ConfirmSelection()
    {
        // Set data if user manager is available
        if (_userManager) _userManager.Write("CharacterId", _characterId);
        // Load next scene
        _sceneLoader.LoadScene("PrologueScene");
    }

    private void Start()
    {
        _button = GetComponent<Button>();

        _sceneLoader = FindObjectOfType<SceneLoader>();

        // Find UserManager if possible
        _userManager = FindObjectOfType<UserManager>();
    }
}
