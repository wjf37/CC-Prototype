using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnInteractMisc : OnInteract
{
    public MiscItemData misc;

    public override void Interact()
    {
        bool itemAdded = false;

        itemAdded = player.GetComponent<InteractHandler>().AddItem(misc);

        if (itemAdded)
        {
            Destroy(gameObject);
        }
    }
}
