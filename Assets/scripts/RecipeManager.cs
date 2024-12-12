using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Unity.VisualScripting;

public class RecipeManager : MonoBehaviour
{
    public List<Recipe> recipes;

    [SerializeField] private TextMeshProUGUI recipeTextLeft;
    [SerializeField] private Image recipeImageLeft;
    [SerializeField] private TextMeshProUGUI recipeTextRight;
    [SerializeField] private Image recipeImageRight;
    private int recipeNumber;
    private int maxRecipes;
    private void Start()
    {
        maxRecipes = recipes.Count;
        Debug.Log("maxRecipes=" + maxRecipes);
        DisplayRecipes(recipeNumber);
    }

    private void DisplayRecipes(int i)
    {
        if(i>maxRecipes)
        {
            recipeTextLeft.text = null;
            recipeImageLeft.enabled = false;
            recipeTextRight.text = null;
            recipeImageRight.enabled = false; 
        }
        else
        {
            recipeTextLeft.text = recipes[i].title + "\n\n" + recipes[i].description + "\n\n" + IngredientsString(i);
            recipeImageLeft.enabled = true;
            recipeImageLeft.sprite = recipes[i].image;
            if (i + 1 >= maxRecipes)
            {
                recipeTextRight.text = null;
                recipeImageRight.enabled = false;
                return;
            }
            else
            {
                recipeTextRight.text = recipes[i + 1].title + "\n\n" + recipes[i + 1].description + "\n\n" + IngredientsString(i + 1);
                recipeImageRight.enabled = true;
                recipeImageRight.sprite = recipes[i + 1].image;
            }
        }
    }
    private string IngredientsString(int i)
    {
        string ingredients = "";
        foreach (string ingredient in recipes[i].ingredients)
        {
            ingredients += "- "+ ingredient + "\n";
        }
        return ingredients;
    }

    public void TurnPages(int i)
    {
        if (i==0 && recipeNumber > 0)
        {
            recipeNumber -= 2;
            DisplayRecipes(recipeNumber);
        }
        else if (i == 1 && recipeNumber < maxRecipes)
        {
            recipeNumber += 2;
            DisplayRecipes(recipeNumber);
        }
    }
}
