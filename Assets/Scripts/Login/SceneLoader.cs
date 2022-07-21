using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void LoadLoginScene()
    {
        var mgr = FindObjectOfType<UserManager>();
        if (mgr)
        {
            Destroy(mgr.gameObject);
        }
        SceneManager.LoadScene("LoginScene");
    }
}
