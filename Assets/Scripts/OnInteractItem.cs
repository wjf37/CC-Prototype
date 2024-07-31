using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnInteractItem : OnInteract
{
    public ItemData item;

    public override void Interact()
    {
        bool itemAdded = false;

        itemAdded = player.GetComponent<InteractHandler>().AddItem(item);

        if (itemAdded)
        {
            Destroy(gameObject);
        }
    }
}
