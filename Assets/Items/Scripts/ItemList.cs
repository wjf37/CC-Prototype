using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item List", menuName = "Item List")]
public class ItemList : ScriptableObject
{
    public List<ItemData> itemList = new();
}
