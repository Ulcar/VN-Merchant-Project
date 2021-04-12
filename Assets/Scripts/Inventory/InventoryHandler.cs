using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Naninovel;
using UniRx.Async;
using UnityEngine;


[InitializeAtRuntime]
[Naninovel.Commands.Goto.DontReset] // skips the service reset on @goto commands (to preserve the inventory when navigating scripts)
public class InventoryHandler : IEngineService
{
    private readonly InputManager inputManager;
    private readonly ScriptPlayer scriptPlayer;
    private LocalizableResourceLoader<InventoryItem> itemLoader;

    public InventoryConfiguration Configuration { get; }
    public InventoryHandler(InventoryConfiguration config, InputManager inputManager, ScriptPlayer scriptPlayer)
    {
        // The services are potentially not yet initialized here, 
        // refrain from using them.
        Configuration = config;
        
        this.inputManager = inputManager;
        this.scriptPlayer = scriptPlayer;
    }

    public void DestroyService()
    {
    }

    public UniTask InitializeServiceAsync() {

        return UniTask.CompletedTask;
    
    }

    public void ResetService()
    {
    }


   

    /// <summary>
    /// Attempts to retrieve item Scriptable object with the specified identifier.
    /// </summary>
    public async UniTask<InventoryItem> GetItemAsync(string itemId)
    {
        // If item resource is already loaded, get it; otherwise load asynchronously.
        var itemResource = itemLoader.GetLoadedOrNull(itemId) ?? await itemLoader.LoadAndHoldAsync(itemId, this);
        if (!itemResource.Valid) throw new Exception($"Failed to load `{itemId}` item resource.");
        return itemResource.Object;
    } 
}
