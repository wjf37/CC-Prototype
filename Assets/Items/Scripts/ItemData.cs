using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Ingredient,
    Potion,
    Misc
}
public abstract class ItemData : ScriptableObject
{
    public string itemName;
    public Sprite icon;
    public GameObject itemPrefab;
    public ItemType type;

    [TextArea(15,20)]
    public string description;
}
