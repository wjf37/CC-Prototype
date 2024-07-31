using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Ingredient,
    Potion,
    Misc
}
public abstract class ItemData : ScriptableObject, IEquatable<ItemData>
{
    public string itemName;
    public Sprite icon;
    public GameObject itemPrefab;
    public ItemType type;

    [TextArea(15,20)]
    public string description;

    public bool Equals(ItemData other)
    {
        if (other == null) return false;
        return itemName == other.itemName;
    }

    public override bool Equals(object obj)
    {
        return Equals(obj as ItemData);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(itemName);
    }
}
