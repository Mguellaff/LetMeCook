using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Order : MonoBehaviour
{
    RecipeManager recipeManager;
    List<Recipe> order;
    private TextMeshProUGUI orderText;
    private Image orderImage;

    void Start()
    {
        order=recipeManager.GetRecipes();
        orderText = GetComponentInChildren<TextMeshProUGUI>();
        orderImage = GetComponentInChildren<Image>();
        ChooseRandomRecipe();
    }

    void Update()
    {
        
    }

    private void ChooseRandomRecipe()
    {
        int randomRecipe = Random.Range(0, order.Count);
        orderText.text = order[randomRecipe].title;
        orderImage.sprite = order[randomRecipe].image;
    }
}
