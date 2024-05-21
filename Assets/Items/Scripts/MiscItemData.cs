using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Misc Object", menuName = "Inventory System/Items/Misc")]
public class MiscItemData : ItemData
{
    public void Reset()
    {
        type = ItemType.Misc;
    }
}
