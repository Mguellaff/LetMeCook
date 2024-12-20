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
        ingredientHeatSlider = GetComponentInChildren<Canvas>().gameObject;
        slider = ingredientHeatSlider.GetComponentInChildren<Slider>();
        ingredientHeatSlider.SetActive(false);

        meshFilter = GetComponent<MeshFilter>();
        meshName = meshFilter.mesh.name.Replace("Raw", "").Replace(" Instance", "");
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
                string burntPrefabPath = $"Meshes/{meshName}Burnt";
                Debug.Log($"Trying to load prefab at path: {burntPrefabPath}");
                GameObject burntPrefab = Resources.Load<GameObject>(burntPrefabPath);
                Debug.Log(burntPrefab);
                if (burntPrefab != null)
                {
                    MeshFilter burntMeshFilter = burntPrefab.GetComponent<MeshFilter>();
                    if (burntMeshFilter != null)
                    {
                        meshFilter.mesh = burntMeshFilter.sharedMesh;
                    }
                }
            }
            else if (cooked > 5500)
            {
                Debug.Log("Cooked");
                string cookedPrefabPath = $"Meshes/{meshName}Cooked";
                Debug.Log($"Trying to load prefab at path: {cookedPrefabPath}");
                GameObject cookedPrefab = Resources.Load<GameObject>(cookedPrefabPath);
                Debug.Log(cookedPrefab);
                if (cookedPrefab != null)
                {
                    MeshFilter cookedMeshFilter = cookedPrefab.GetComponent<MeshFilter>();
                    if (cookedMeshFilter != null)
                    {
                        meshFilter.mesh = cookedMeshFilter.sharedMesh;
                    }
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
