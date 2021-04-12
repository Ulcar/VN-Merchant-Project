using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Naninovel;
using UniRx.Async;
public class DialogueHandler : MonoBehaviour
{
    // Start is called before the first frame update


    // Update is called once per frame
    void Update()
    {
        
    }


    public void RunDialogue(string scriptName, string label) 
    {
        var inputManager = Engine.GetService<IInputManager>();
        inputManager.ProcessInput = true;

        var scriptPlayer = Engine.GetService<IScriptPlayer>();
        scriptPlayer.PreloadAndPlayAsync(scriptName, label: label).Forget();
    }


    public void RunDialogue(string scriptName) 
    {
        Debug.Log("Starting dialogue");
        var inputManager = Engine.GetService<IInputManager>();
        inputManager.ProcessInput = true;

        var scriptPlayer = Engine.GetService<IScriptPlayer>();

        var CameraPlayer = Engine.GetService<ICameraManager>();

        CameraPlayer.UICamera.enabled = true;

        scriptPlayer.PreloadAndPlayAsync(scriptName);
    }


    public void DisableDialogue() 
    {
        var CameraPlayer = Engine.GetService<ICameraManager>();

        CameraPlayer.UICamera.enabled = false;
    }
}
