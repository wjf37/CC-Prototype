using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Ingredient Object", menuName = "Inventory System/Items/Ingredient")]
public class IngredientItemData : ItemData
{
    //specific properties of Ingredients: element system. specific properties of its leaves/roots/fruits. whether it can be separated into those parts
    //how it can be processed. that may be better defined with the tool scripts than the Ingredient scripts. 
    public void Reset()
    {
        type = ItemType.Ingredient;
    }
}
