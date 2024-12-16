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

    void Start()
    {
        GameObject stove = GameObject.Find("stove");
        if (stove != null)
        {
            heatSlider = stove.GetComponentInChildren<HeatSliderScript>();
            Debug.Log("heatSlider found: " + heatSlider);
        }
        else
        {
            Debug.LogError("Stove not found");
        }
        ingredientHeatSlider = GetComponentInChildren<Canvas>().gameObject;
        slider=ingredientHeatSlider.GetComponentInChildren<Slider>();
        ingredientHeatSlider.SetActive(false);

    }

    void Update()
    {
        Debug.Log("IsParentContainer()=" + IsParentContainer());

        if (IsParentContainer() && heatSlider != null)
        {
            ingredientHeatSlider.SetActive(true);
            temperature = heatSlider.GetTemperature();
            Debug.Log("temperature=" + temperature);

            cooked += temperature/10 * Time.deltaTime * 100;
            slider.value = cooked;
            if (cooked > 15000)
            {
                Debug.Log("c'est cramé");
            }
            else if (cooked > 5500)
            {
                Debug.Log("c'est cuit");
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
