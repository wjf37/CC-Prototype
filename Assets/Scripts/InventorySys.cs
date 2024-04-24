using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySys : ScriptableObject
{
    public int maxItems = 5;
    public List<ItemData> items = new();
    
    private void Start()
    {
        for (int i = 0; i<maxItems; i++)
        {
            items.Add(null);
        }
    }
    public bool AddItem(ItemData item)
    {
        //find an empty inventory spot that does not go over max item count and add
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i] == null)
            {
                items[i] = item;
                return true;
            }
        }

        return false;  
    }

    public ItemData RemoveItem(int index)
    {
        //selected item from inv/hotbar is removed. This is used when using an item like using an item in a recipe. The item should be transferred to 
        //wherever it is used like into the cauldron.
        ItemData remItem = items[index];
        items[index] = null;
        return remItem;
    }

    public void DropItem(ItemData item)
    {
        //selected item is spawned in a suitable area near player, ideally in front and dropped. This item should have the same properties as it did in the inv
        //and before it was put into the inv.
    }
}
