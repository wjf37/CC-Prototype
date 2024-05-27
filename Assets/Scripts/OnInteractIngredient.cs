using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnInteractIngredient : OnInteract
{
    public IngredientItemData ingredient;

    public override void Interact()
    {
        bool itemAdded = false;

        itemAdded = player.GetComponent<InteractHandler>().AddItem(ingredient);

        if (itemAdded)
        {
            Destroy(gameObject);
        }
    }
}
