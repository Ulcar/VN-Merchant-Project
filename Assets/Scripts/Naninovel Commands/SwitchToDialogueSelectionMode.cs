using UnityEngine;
using System.Collections;
using Naninovel.Commands;
using Naninovel;
using UniRx.Async;

[CommandAlias("DialogueSelection")]
public class SwitchToDialogueSelectionMode : Command
{
    [ParameterAlias("reset")]
    public BooleanParameter ResetState = false;

    public override async UniTask ExecuteAsync(CancellationToken cancellationToken = default)
    { 
    
    
    }


}
