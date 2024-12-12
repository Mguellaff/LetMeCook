using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class RecipeManager : MonoBehaviour
{
    public List<Recipe> recipes;

    [SerializeField] private TextMeshProUGUI recipeTextLeft;
    [SerializeField] private Image recipeImageLeft;
    [SerializeField] private TextMeshProUGUI recipeTextRight;
    [SerializeField] private Image recipeImageRight;

    private void Start()
    {
        DisplayRecipes();
    }

    private void DisplayRecipes()
    {
            recipeTextLeft.text = recipes[0].title + "\n" + recipes[0].description + "\n" + IngredientsString(0);
            recipeImageLeft.sprite = recipes[0].image; 
        recipeTextRight.text = recipes[1].title + "\n" + recipes[1].description + "\n" + IngredientsString(1);
        recipeImageRight.sprite = recipes[1].image;
    }
    private string IngredientsString(int i)
    {
        string ingredients = "";
        foreach (string ingredient in recipes[i].ingredients)
        {
            ingredients += ingredient + "\n";
        }
        return ingredients;
    }

    private void TurnPages()
    {
        
    }
}
