using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnInteractWell : OnInteract
{
    public MiscItemData water;
    public override void Interact()
    {
        bool itemAdded = false;

        itemAdded = player.GetComponent<InteractHandler>().AddItem(water);

    }
}
