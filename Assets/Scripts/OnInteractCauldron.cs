using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class OnInteractCauldron : OnInteract
{
    InteractHandler interactHandler;
    Recipe currentRecipe = new();
    void Start()
    {
        interactHandler = player.GetComponent<InteractHandler>();
    }

    public override void Interact()
    {
        if (interactHandler.selectedInvSlot != 0 && interactHandler.invSlotFilled)
        {
            //remove selected item from inventory and instantiate in the cauldron.
            //add the item to the current recipe
            //need to work out if I should modify the recipe type so that it is a list of items so I can iterate through the items in the current recipe easier
            //sort the items in the recipe and then compare the recipe to the book.
            ItemData remItem;
            remItem = interactHandler.RemoveItem(interactHandler.selectedInvSlot);
            Instantiate(remItem.itemPrefab, gameObject.transform);
            if (remItem.itemName == "Water")
            {
                currentRecipe.water = true;
            }
        }

        else if (!interactHandler.invSlotFilled) //if there are items currently in the cauldron and free inventory slots take out the last item put into the cauldron.
        {

        }
    }

    private void ResetRecipe()
    {
        currentRecipe = new();
    }
}
