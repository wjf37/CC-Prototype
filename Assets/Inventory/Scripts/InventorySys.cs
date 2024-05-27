using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Inv", menuName = "Inventory System/Inv")]
public class InventorySys : ScriptableObject
{
    public int maxItems = 5;
    public List<ItemData> items = new();
    
    public void InvInit()
    {
        items.Clear();
        for (int i = 0; i<maxItems; i++)
        {
            items.Add(null);
        }
    }

}


