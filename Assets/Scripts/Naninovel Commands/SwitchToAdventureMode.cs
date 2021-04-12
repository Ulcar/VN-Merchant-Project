using Naninovel;
using Naninovel.Commands;
using UniRx.Async;
using UnityEngine;

[CommandAlias("adventure")]
public class SwitchToAdventureMode : Command
{
    [ParameterAlias("reset")]
    public BooleanParameter ResetState = false;

    public override async UniTask ExecuteAsync (CancellationToken cancellationToken = default)
    {
        // 1. Disable Naninovel input.
        var inputManager = Engine.GetService<IInputManager>();
        inputManager.ProcessInput = false;

        // 2. Stop script player.
        var scriptPlayer = Engine.GetService<IScriptPlayer>();
        scriptPlayer.Stop();

        // 3. Hide text printer.
        var hidePrinter = new HidePrinter();
        hidePrinter.ExecuteAsync(cancellationToken).Forget();

        // 4. Reset state (if required).
        if (ResetState)
        {
            var stateManager = Engine.GetService<IStateManager>();
            await stateManager.ResetStateAsync();
        }


        Engine.GetService<ICameraManager>().Camera.enabled = false;
        Engine.GetService<ICameraManager>().UICamera.enabled = false;
        Debug.Log("Camera disabled");

        // 6. Enable character control.
        var controller = Object.FindObjectOfType<CameraController>();
        Cursor.lockState = CursorLockMode.Locked;
        controller.enableInputNextFrame = true;
    }
}
