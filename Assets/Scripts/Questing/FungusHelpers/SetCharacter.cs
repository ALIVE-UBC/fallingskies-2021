using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using Fungus;


[CommandInfo("Questing", "Set character", "")]
public class SetCharacter : Command
{
    public Character Character;

    private SayDialog _sayDialog;

    public override void OnEnter()
    {
        _sayDialog = FindObjectOfType<SayDialog>(true);
        _sayDialog.SetCharacter(Character);

        Continue();
    }
}
