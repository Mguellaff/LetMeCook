using UnityEngine;
using TMPro;
using System.Collections.Generic;
using UnityEditor;
using System;
public class RecipeCreator : MonoBehaviour
{
    [SerializeField] private TMP_InputField titleInputField;
    [SerializeField] private TMP_InputField descriptionInputField;
    [SerializeField] private TMP_InputField ingredientsInputField;
    private RecipeManager recipeManager;

    private void Start()
    {
        recipeManager = FindObjectOfType<RecipeManager>();
        if (recipeManager == null)
        {
            Debug.LogError("RecipeManager not found in the scene.");
        }

        if (titleInputField == null || descriptionInputField == null || ingredientsInputField == null)
        {
            Debug.LogError("One or more InputFields are not assigned.");
        }
    }

    public void CreateRecipe()
    {
        if (recipeManager == null)
        {
            Debug.LogError("RecipeManager is not assigned.");
            return;
        }

        if (titleInputField == null || descriptionInputField == null || ingredientsInputField == null)
        {
            Debug.LogError("One or more InputFields are not assigned.");
            return;
        }

        Recipe newRecipe = ScriptableObject.CreateInstance<Recipe>();
        newRecipe.title = titleInputField.text;
        newRecipe.description = descriptionInputField.text;
        newRecipe.ingredients = new List<string>(ingredientsInputField.text.Split('\n'));
        newRecipe.image = null;
        string path = "Assets/Recipes/" + newRecipe.title + ".asset";
        AssetDatabase.CreateAsset(newRecipe, path);
        AssetDatabase.SaveAssets();
        recipeManager.recipes.Add(newRecipe);
        Debug.Log(recipeManager.recipes.Count);
        recipeManager.DisplayRecipes(Math.Max(0, recipeManager.recipes.Count - 2));

    }
}
