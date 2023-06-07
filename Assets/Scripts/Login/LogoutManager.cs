using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LogoutManager : MonoBehaviour
{
    const string EndSurveyUrl = "https://ubc.ca1.qualtrics.com/jfe/form/SV_d4MNs2XTc2DRRie?UserID=";

    private UserManager _userManager;

    private void Start()
    {
        _userManager = FindObjectOfType<UserManager>();
    }

    public void OpenEndSurveyPage()
    {
        Application.OpenURL(EndSurveyUrl + GetUserId());
    }

    private static int GetUserId()
    {
        UserManager userManager = FindObjectOfType<UserManager>();
        int userId = int.Parse((string) userManager.Read("UserId", "0"));
        return userId;
    }
}
