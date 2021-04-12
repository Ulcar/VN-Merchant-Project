using Naninovel;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class SetupGame : MonoBehaviour
{
    public Camera AdventureModeCamera;

    private async void Start ()
    {
        // 1. Initialize Naninovel.
        await RuntimeInitializer.InitializeAsync();

        // 2. Enter adventure mode.
        var switchCommand = new SwitchToAdventureMode { ResetState = false, ConditionalExpression = "" };
        await switchCommand.ExecuteAsync();

        var path = new NamedString();
        path.Value = "label1";
        path.Name = "";

        

        await new Naninovel.Commands.Goto { Path = path}.ExecuteAsync();

        var data = AdventureModeCamera.GetUniversalAdditionalCameraData();
        data.cameraStack.Add(Engine.GetService<ICameraManager>().Camera);
        data.cameraStack.Add(Engine.GetService<ICameraManager>().UICamera);

     //   Engine.GetService<ICameraManager>().Camera.transform.SetParent(AdventureModeCamera.transform);
      //  Engine.GetService<ICameraManager>().UICamera.transform.SetParent(AdventureModeCamera.transform);
    }

}
