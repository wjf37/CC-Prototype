using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class OnInteractCauldron : OnInteract
{
    private InteractHandler interactHandler;
    public Recipe currentRecipe = new();
    private int itemsNum = 0;
    private Transform cauldronItems;
    private Transform potionPos;
    public override void Start()
    {
        base.Start();
        interactHandler = player.GetComponent<InteractHandler>();
        cauldronItems = transform.GetChild(1);
        potionPos = cauldronItems.GetChild(4);
    }

    public override void Interact()
    {
        bool itemAdded = false;
        if (interactHandler.selectedInvSlot != 0 && interactHandler.invSlotFilled)
        {
            //remove selected item from inventory and instantiate in the cauldron.
            //add the item to the current recipe
            //need to work out if I should modify the recipe type so that it is a list of items so I can iterate through the items in the current recipe easier
            //sort the items in the recipe and then compare the recipe to the book.
            ItemData remItem;
            remItem = interactHandler.GetSelItem(interactHandler.selectedInvSlot);

            if (remItem.itemName == "Water" && !currentRecipe.water)
            {
                currentRecipe.water = true;
                cauldronItems.GetChild(0).gameObject.SetActive(true);
                interactHandler.RemoveItem(interactHandler.selectedInvSlot);
            }

            else if (remItem.itemName != "Water" && itemsNum < 3)
            {
                currentRecipe.itemList.Add(remItem);
                itemsNum ++;
                itemAdded = true;
            }
            
            if (itemAdded)
            {
                remItem = interactHandler.RemoveItem(interactHandler.selectedInvSlot);
                Instantiate(remItem.itemPrefab, cauldronItems.GetChild(itemsNum));
            }
        }

        else if (!interactHandler.invSlotFilled && itemsNum > 0) //if there are items currently in the cauldron and free inventory slots take out the last item put into the cauldron.
        {
            ItemData caulItem = cauldronItems.GetChild(itemsNum).GetChild(0).gameObject.GetComponent<OnInteractIngredient>().ingredient;
            itemAdded = player.GetComponent<InteractHandler>().AddItem(caulItem);
            if (itemAdded)
            {
                Destroy(cauldronItems.GetChild(itemsNum).GetChild(0).gameObject);
                itemsNum --;
            }
        }
    }

    public void ResetRecipe()
    {
        currentRecipe = new();
        itemsNum = 0;
        cauldronItems.GetChild(0).gameObject.SetActive(false);
        for (int i = 1; i < 4; i ++)
        {
            if (cauldronItems.GetChild(i).childCount != 0)
            {
                Destroy(cauldronItems.GetChild(i).GetChild(0).gameObject);
            }
        }
    }

    public void SpawnPotion(PotionItemData potion)
    {
        Instantiate(potion.itemPrefab, potionPos);
    }
}
