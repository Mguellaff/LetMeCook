using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Order : MonoBehaviour
{
    private RecipeManager recipeManager;
    List<Recipe> order;
    private TextMeshProUGUI orderText;
    private Image orderImage;
    private string recipeToMake;
    [SerializeField] private GameObject plate;
    [SerializeField] private TextMeshProUGUI successText; // Assurez-vous de lier ce TextMeshProUGUI dans l'inspecteur
    [SerializeField] private AudioSource successAudio; // Assurez-vous de lier cette AudioSource dans l'inspecteur

    void Start()
    {
        recipeManager = FindObjectOfType<RecipeManager>();
        order = recipeManager.GetRecipes();
        orderText = GetComponentInChildren<TextMeshProUGUI>();
        orderImage = GetComponentInChildren<Image>();
        ChooseRandomRecipe();
    }

    void Update()
    {
        CheckRecipe();
    }

    private void ChooseRandomRecipe()
    {
        int randomRecipe = Random.Range(0, order.Count);
        orderText.text = order[randomRecipe].title;
        orderImage.sprite = order[randomRecipe].image;

        if (order[randomRecipe].title == "poulet cramé")
        {
            recipeToMake = "chicken_Leg_Meat_Burnt";
        }
        else if (order[randomRecipe].title == "ton poisson rouge")
        {
            recipeToMake = "fish_Cooked";
        }
        else
        {
            recipeToMake = "beef_Steak_Raw";
        }
    }

    private void CheckRecipe()
    {
        foreach (Transform child in plate.transform)
        {
            MeshFilter meshFilter = child.GetComponent<MeshFilter>();
            if (meshFilter != null && meshFilter.mesh.name.Replace(" Instance", "") == recipeToMake)
            {
                successText.gameObject.SetActive(true);
                if (!successAudio.isPlaying)
                {
                    successAudio.Play();
                    StartCoroutine(ReloadSceneAfterSound(successAudio.clip.length));
                }
                return;
            }
        }
        successText.gameObject.SetActive(false);
    }

    private IEnumerator ReloadSceneAfterSound(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
