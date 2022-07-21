using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

[CommandInfo("User manager", "Write user data", "")]
public class WriteUserData : Command
{
    public string UserDataKey;

    [VariableProperty(typeof(StringVariable))]
    public StringVariable UserDataValue;

    private UserManager _umgr;

    public override void OnEnter()
    {
        if (!_umgr) _umgr = FindObjectOfType<UserManager>();
        _umgr.Write(UserDataKey, UserDataValue.Value);
        Continue();
    }
}
