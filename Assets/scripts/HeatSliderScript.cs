using UnityEngine;
using UnityEngine.UI;

public class HeatSliderScript : MonoBehaviour
{
    private Slider heatSlider; 
    [SerializeField] private ParticleSystem particleSystem;
    private ParticleSystem.MainModule mainModule;

    void Start()
    {
        heatSlider= GetComponent<Slider>();
        if (particleSystem != null)
        {
            mainModule = particleSystem.main;
        }
    }

    void Update()
    {
        if (heatSlider != null && particleSystem != null)
        {
            float heatValue = heatSlider.value;
            mainModule.startColor = new Color(heatValue, 0, 0);
        }
    }
}
