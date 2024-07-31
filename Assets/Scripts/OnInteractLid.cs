using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class OnInteractLid : OnInteract
{
    private OnInteractCauldron cauldronScript;
    private Recipe cauldronRecipe;
    private bool isBrewing = false;
    [SerializeField] Transform GlidPos;//ground
    [SerializeField] Transform ClidPos;//cauldron
    [SerializeField] PotionItemData trashPotion; 
    public RecipeBook recipeBook;
    public PotionItemData craftedPotion;
    //used to initiate potion brewing.
    //when interacted with, pos shifts to on top of the cauldron.
    //if matching recipe is found to recipe entered, reset recipe and then instantiate finished potion into the cauldron and reshift pos.
    //else perhaps some sort of trash is produced instead.
    //need some function to compare between entered recipe and list of possible recipes.
    //perhaps some sort of timed function that simulates a brewing time. at this stage probably 5 seconds will do.
    public override void Start()
    {
        base.Start();
        cauldronScript = GameObject.Find("Cauldron").GetComponent<OnInteractCauldron>();
    }
    public override void Interact()
    {
        StartCoroutine(BrewFunction());
        /*
        gameObject.transform.position = ClidPos.position;
        cauldronRecipe = cauldronScript.currentRecipe;
        craftedPotion = RecipeCompare(cauldronRecipe);
        StartCoroutine(WaitForFunction());
        cauldronScript.ResetRecipe();

        cauldronScript.SpawnPotion(craftedPotion);
        gameObject.transform.position = GlidPos.position;*/
    }

    private PotionItemData RecipeCompare(Recipe cauldronRecipe)
    {
        foreach (Recipe recipes in recipeBook.recipeList)
        {
            if (!cauldronRecipe.itemList.Except(recipes.itemList).Any() && cauldronRecipe.itemList.Count == 3) //if the list of differences in the recipe being checked against is 0, then the recipes are the same and it can return the recipe.
            {
                return recipes.potion;
            }
        }
        
        return trashPotion;
    }

    IEnumerator BrewFunction()
    {
        //isBrewing = true;
        //isBrewing = false;
        gameObject.transform.position = ClidPos.position;
        cauldronRecipe = cauldronScript.currentRecipe;
        craftedPotion = RecipeCompare(cauldronRecipe);
        cauldronScript.ResetRecipe();
        yield return new WaitForSeconds(3);
        cauldronScript.SpawnPotion(craftedPotion);
        gameObject.transform.position = GlidPos.position;
    }
}
