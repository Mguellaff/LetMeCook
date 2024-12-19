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
    private string meshName;
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
 
        meshFilter = GetComponent<MeshFilter>();
        meshName= meshFilter.mesh.name;
    }

    void Update()
    {
        if (IsParentContainer() && heatSlider != null)
        {
            ingredientHeatSlider.SetActive(true);
            temperature = heatSlider.GetTemperature();
            cooked += temperature / 10 * Time.deltaTime * 100;
            slider.value = cooked;

            if (cooked > 15000)
            {
                Debug.Log("Burned");
                Mesh burntMesh = Resources.Load<Mesh>($"Meshes/{meshName}Burnt");
                if (burntMesh != null)
                {
                    meshFilter.mesh = burntMesh;
                }
            }
            else if (cooked > 5500)
            {
                Debug.Log("Cooked");
                Mesh cookedMesh = Resources.Load<Mesh>($"Meshes/{meshName}Cooked");
                if (cookedMesh != null)
                {
                    meshFilter.mesh = cookedMesh;
                }
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
