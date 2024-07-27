using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnInteractLid : OnInteract
{
    //used to initiate potion brewing.
    //when interacted with, pos shifts to on top of the cauldron.
    //if matching recipe is found to recipe entered, reset recipe and then instantiate finished potion into the cauldron and reshift pos.
    //else perhaps some sort of trash is produced instead.
    //need some function to compare between entered recipe and list of possible recipes.
    //perhaps some sort of timed function that simulates a brewing time. at this stage probably 5 seconds will do.
    public override void Interact()
    {

    }

    private bool RecipeCompare(Recipe cauldronRecipe)
    {
        return false;
    }
}
