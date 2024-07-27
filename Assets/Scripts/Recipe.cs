using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[Serializable, Inspectable]
public class Recipe
{
    public string name;
    public List<ItemData> itemList = new();
    public bool water = false;
    public PotionItemData potion;
}
