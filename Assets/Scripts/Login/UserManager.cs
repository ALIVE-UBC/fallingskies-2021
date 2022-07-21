using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserManager : MonoBehaviour
{
    public Dictionary<string, object> Data = new Dictionary<string, object>();

    private void Awake()
    {
        if (FindObjectsOfType<UserManager>().Length > 1)
        {
            this.gameObject.SetActive(false);
            Destroy(this.gameObject);
        }
        else
        {
            DontDestroyOnLoad(this.gameObject);
        }
    }

    public object Read(string key, object defaultValue = null)
    {
        return Data.TryGetValue(key, out object value) ? value : defaultValue;
    }

    public void Write(string key, object value)
    {
        Data[key] = value;
    }
}
