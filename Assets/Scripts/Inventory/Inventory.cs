using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UniRx.Async;
using Naninovel;

public class Inventory : MonoBehaviour
    {




    [System.Serializable]
    private class InventoryState
    {
        public GenericDictionary<string, InventoryItemInstance> Items;

    }



    public GenericDictionary<string, InventoryItemInstance> items;
    private IStateManager stateManager;


    private void GetEngineServices() 
    {
        stateManager = Engine.GetService<IStateManager>();
    }


    private void Awake()
    {

        if (Engine.Initialized) GetEngineServices();
        else Engine.OnInitializationFinished += GetEngineServices;
    }

    private void AddSerializeTask() 
    {
        stateManager.AddOnGameSerializeTask(SerializeState);
        stateManager.AddOnGameDeserializeTask(DeserializeState);
    }

    private void OnEnable()
    {

        if (Engine.Initialized)
        {
            stateManager.AddOnGameSerializeTask(SerializeState);
            stateManager.AddOnGameDeserializeTask(DeserializeState);
        }

        else
        {
            Engine.OnInitializationFinished += AddSerializeTask;
        }
    }

    private void OnDisable()
    {
        stateManager.RemoveOnGameSerializeTask(SerializeState);
        stateManager.RemoveOnGameDeserializeTask(DeserializeState);
    }




    private void SerializeState(GameStateMap stateMap)
    {
        var state = new InventoryState()
        {
            Items = this.items
        };
        stateMap.SetState(state);
    }

    private UniTask DeserializeState(GameStateMap stateMap)
    {
        var state = stateMap.GetState<InventoryState>();
        if (state is null) return UniTask.CompletedTask;

        items = state.Items;
        return UniTask.CompletedTask;
    }


    public void AddItem(InventoryItem item) 
    {
        if (!items.ContainsKey(item.ItemID)) 
        {
            items.Add(item.ItemID, new InventoryItemInstance(item));
        }
    }
    }