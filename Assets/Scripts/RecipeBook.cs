using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Recipe Book", menuName = "Recipe Book")]
public class RecipeBook : ScriptableObject
{
    public List<Recipe> recipeBook = new();
}
