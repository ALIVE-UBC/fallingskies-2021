using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using JWT.Builder;
using JWT.Algorithms;
using JWT.Exceptions;

public class LoginManager : MonoBehaviour
{

    [SerializeField] TMP_InputField _codeInput = default;
    [SerializeField] TMP_Text _errorText = default;

    const string Psk = "interior standby purge endogamy queued impanel matins leakage bunkum brackish";
    const string SurveyUrl = "https://alivelab.ca/alive-survey";

    private UserManager _userManager;

    private void Start()
    {
        _userManager = FindObjectOfType<UserManager>();
        ShowError("");
    }

    public void OpenSurveyPage()
    {
        Application.OpenURL(SurveyUrl);
    }

    public void PasteCode()
    {
        string code = GUIUtility.systemCopyBuffer;
        _codeInput.text = code;
    }

    public void VerifyCode()
    {
        // Prevent the code from being stripped in iOS builds.
        var _ = new JwtHeader();

        ShowError("");

        string code = _codeInput.text.Trim();

        try
        {
            _userManager.Data = JwtBuilder.Create()
                     .WithAlgorithm(new HMACSHA256Algorithm()) // symmetric
                     .WithSecret(Psk)
                     .MustVerifySignature()
                     .Decode<Dictionary<string, object>>(code);
        }
        catch (Exception e)
        {
            Debug.LogError(e);
            ShowError($"Invalid code. Ask your instructor for help.\n{e.Message}");
            return;
        }

        SceneManager.LoadScene("CharacterScene");
    }

    private void ShowError(string msg)
    {
        _errorText.text = msg;
    }

}
