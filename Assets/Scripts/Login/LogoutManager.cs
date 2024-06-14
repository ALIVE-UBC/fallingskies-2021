using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class LogoutManager : MonoBehaviour
{
    const string EndSurveyURL = "https://alivelab.ca/alive-post-survey";
    private UserManager _userManager;
    public IEnumerator GetRedirectURL(string url)
    {
        UnityWebRequest request = UnityWebRequest.Get(url);
        yield return request.SendWebRequest();
        int userId = GetUserId();
        Application.OpenURL(request.url + "?UserID=" + userId);
    }

    private void Start()
    {
        _userManager = FindObjectOfType<UserManager>();
    }

    public void OpenEndSurveyPage()
    {
        StartCoroutine(GetRedirectURL(EndSurveyURL));
    }

    private static int GetUserId()
    {
        UserManager userManager = FindObjectOfType<UserManager>();
        int userId = int.Parse((string) userManager.Read("UserId", "0"));
        return userId;
    }
}