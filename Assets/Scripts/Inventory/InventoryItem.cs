using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Naninovel;
using UniRx.Async;
using UnityEngine;


   [CreateAssetMenu]
    public class InventoryItem:ScriptableObject
    {

    public string ItemID;

    public string Name;

    public float ItemValue;


    public ItemUI itemUI;

    }


public class ItemUI
{

    public string UIName;

    public Sprite itemSprite;

}


[Serializable]
public class InventoryItemInstance 
{


    public InventoryItemInstance(InventoryItem data) 
    {
        itemData = data;
    }
    
    
    public InventoryItem itemData;

    public int stackCount;
}

