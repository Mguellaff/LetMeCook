using System.Collections.Generic;
using UnityEngine;

public class RecipeManager : MonoBehaviour
{
    public List<Recipe> recipes;

    private void Start()
    {
        recipes = new List<Recipe>();
    }
}
