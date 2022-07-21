using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

[CommandInfo("User manager", "Read user data", "")]
public class ReadUserData : Command
{
    public string UserDataKey;

    [VariableProperty(typeof(StringVariable))]
    public StringVariable Result;

    private UserManager _umgr;

    public override void OnEnter()
    {
        if (!_umgr) _umgr = FindObjectOfType<UserManager>();
        Result.Value = (string) _umgr.Read(UserDataKey, "");
        Continue();
    }
}
