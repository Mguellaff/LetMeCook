using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;   
public class Ingredient : MonoBehaviour
{
    private HeatSliderScript heatSlider;
    private float temperature;
    private float cooked;
    private GameObject ingredientHeatSlider;
    private Slider slider;
    private MeshFilter meshFilter;
    void Start()
    {
        GameObject stove = GameObject.Find("stove");
        if (stove != null)
        {
            heatSlider = stove.GetComponentInChildren<HeatSliderScript>();
        }
        else
        {
        }
        ingredientHeatSlider = GetComponentInChildren<Canvas>().gameObject;
        slider=ingredientHeatSlider.GetComponentInChildren<Slider>();
        ingredientHeatSlider.SetActive(false);

        slider.value = cooked; 
        meshFilter = GetComponent<MeshFilter>();
    }

    void Update()
    {

        if (IsParentContainer() && heatSlider != null)
        {
            ingredientHeatSlider.SetActive(true);
            temperature = heatSlider.GetTemperature();
            Debug.Log(temperature);
            cooked += temperature/10 * Time.deltaTime * 100;

            if (cooked > 15000)
            {
                meshFilter.mesh.name = meshFilter.mesh.name.Replace("Raw", "Burned");
            }
            else if (cooked > 5500)
            {
                meshFilter.mesh.name = meshFilter.mesh.name.Replace("Raw", "Cooked");
            }
        }
        else
        {
            ingredientHeatSlider.SetActive(false);
        }
    }

    private bool IsParentContainer()
    {
        return transform.parent != null && transform.parent.CompareTag("Container");
    }

}
