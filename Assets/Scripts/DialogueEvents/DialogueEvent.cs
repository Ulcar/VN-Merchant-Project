using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Naninovel;
namespace Assets.Scripts.DialogueEvents
{
   

    [CreateAssetMenu]
    public class DialogueEvent:ScriptableObject
    {
        // Use this for initialization
        [SerializeField]
        private string script;
        [SerializeField]
        private string label;

        [SerializeField]
        Naninovel.Script scriptFile;

        [SerializeField]
        List<Trigger> triggers;


        public string eventText;

        public string Script { get { return script; } private set { script = scriptFile.name; } }


        public bool CheckTriggers() 
        {
            foreach (Trigger trigger in triggers) 
            {
                if (!trigger.CheckIfTriggerMet()) 
                {
                    return false;
                }
            }
            return true;
        }


        public void PlayEvent()
        {
            // TODO: Move Engine logic somewhere else?
            
            Debug.Log("Starting dialogue");
            var inputManager = Engine.GetService<IInputManager>();
            inputManager.ProcessInput = true;

            var scriptPlayer = Engine.GetService<IScriptPlayer>();

            var CameraPlayer = Engine.GetService<ICameraManager>();

            CameraPlayer.UICamera.enabled = true;

            scriptPlayer.PreloadAndPlayAsync(script);
        }



    }
}
