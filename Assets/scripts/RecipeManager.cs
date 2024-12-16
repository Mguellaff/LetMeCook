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
    [SerializeField] private Canvas recipeCanvas;
    [SerializeField] private Canvas createRecipeCanvas;
    private bool recipeCanvasOpen=false;
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
            recipeTextLeft.text = recipes[i].title;//recipes[i].description + "\n\n" + IngredientsString(i);
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
                recipeTextRight.text = recipes[i + 1].title;
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
        if (i == 0 && recipeNumber > 0)
        {
            recipeNumber -= 2;
            DisplayRecipes(recipeNumber);
        }
        else if (i == 1 && recipeNumber + 2 < maxRecipes)
        {
            recipeNumber += 2;
            DisplayRecipes(recipeNumber);
        }
    }


    public void OpenRecipe(bool isLeft)
    {
        Debug.Log("openrecipe");
        recipeCanvasOpen = !recipeCanvasOpen;
        recipeCanvas.gameObject.SetActive(recipeCanvasOpen);
        GameObject recipeTitleObject = GameObject.Find("recipeTitle");
        TextMeshProUGUI recipeTitle = recipeTitleObject.GetComponent<TextMeshProUGUI>();
        GameObject recipeTextObject = GameObject.Find("recipeText");
        TextMeshProUGUI recipeText = recipeTextObject.GetComponent<TextMeshProUGUI>();
        GameObject recipeImageObject = GameObject.Find("recipeImage");
        Image recipeImage = recipeImageObject.GetComponent<Image>();

        if (isLeft)
        {
            if (recipeNumber < recipes.Count)
            {
                recipeTitle.text = recipes[recipeNumber].title;
                recipeText.text = recipes[recipeNumber].description + "\n\n" + IngredientsString(recipeNumber);
                recipeImage.sprite = recipes[recipeNumber].image;
            }
            else
            {
                Debug.LogWarning("Index out of range: recipeNumber is greater than the number of recipes.");
            }
        }
        else
        {
            if (recipeNumber + 1 < recipes.Count)
            {
                recipeTitle.text = recipes[recipeNumber + 1].title;
                recipeText.text = recipes[recipeNumber + 1].description + "\n\n" + IngredientsString(recipeNumber + 1);
                recipeImage.sprite = recipes[recipeNumber + 1].image;
            }
            else
            {
                Debug.LogWarning("Index out of range: recipeNumber + 1 is greater than the number of recipes.");
            }
        }
    }


    public void CloseRecipe()
    {
        recipeCanvasOpen = false;
        recipeCanvas.gameObject.SetActive(recipeCanvasOpen);
    }

    public void CloseCreateRecipe()
    {
        createRecipeCanvas.gameObject.SetActive(false);
    }

    public void OpenCreateRecipe()
    {
        createRecipeCanvas.gameObject.SetActive(true);
    }

    public List<Recipe> GetRecipes()
    {
        return recipes;
    }
}
