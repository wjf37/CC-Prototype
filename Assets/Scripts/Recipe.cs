using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[Serializable, Inspectable]
public class Recipe
{
    public string name;
    public ItemData item1;
    public ItemData item2;
    public ItemData item3;
    public bool water = false;
    public PotionItemData potion;
}
