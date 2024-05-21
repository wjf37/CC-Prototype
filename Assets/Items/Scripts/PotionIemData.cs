using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Potion Object", menuName = "Inventory System/Items/Potion")]
public class PotionItemData : ItemData
{
    public void Reset()
    {
        type = ItemType.Potion;
    }
}
