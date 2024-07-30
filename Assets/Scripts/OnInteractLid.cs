using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class OnInteractLid : OnInteract
{
    private GameObject lidPos;
    private OnInteractCauldron cauldronScript;
    private Recipe cauldronRecipe;
    public RecipeBook recipeBook;
    private Transform GlidPos;//ground
    private Transform ClidPos;//cauldron
    //used to initiate potion brewing.
    //when interacted with, pos shifts to on top of the cauldron.
    //if matching recipe is found to recipe entered, reset recipe and then instantiate finished potion into the cauldron and reshift pos.
    //else perhaps some sort of trash is produced instead.
    //need some function to compare between entered recipe and list of possible recipes.
    //perhaps some sort of timed function that simulates a brewing time. at this stage probably 5 seconds will do.
    public override void Start()
    {
        base.Start();
        lidPos = GameObject.Find("Lid Pos");
        GlidPos = lidPos.transform.GetChild(0);
        ClidPos = lidPos.transform.GetChild(1);
        cauldronScript = GameObject.Find("Cauldron").GetComponent<OnInteractCauldron>();
    }
    public override void Interact()
    {
        gameObject.transform.Translate(ClidPos.position);
        cauldronRecipe = cauldronScript.currentRecipe;
        RecipeCompare(cauldronRecipe);
    }

    private void RecipeCompare(Recipe cauldronRecipe)
    {
        foreach (Recipe recipes in recipeBook.recipeList)
        {

        }
    }
}
