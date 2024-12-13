using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HeatSliderScript : MonoBehaviour
{
    private Slider heatSlider;
    [SerializeField] private ParticleSystem particleSystem;
    private ParticleSystem.MainModule mainModule;
    private float temperature;
    private TextMeshProUGUI temperatureText;
    [SerializeField] private Transform switchTransform;
    void Start()
    {
        heatSlider = GetComponentInChildren<Slider>();
        temperatureText = GetComponentInChildren<TextMeshProUGUI>();
        mainModule = particleSystem.main;
heatSlider.onValueChanged.AddListener(OnSliderValueChanged);
    }

    void OnSliderValueChanged(float value)
    {
        temperature = value * 100 + 50;
        switchTransform.rotation= Quaternion.Euler(0, 0, 180 * value);
        Color startColor;
        Color endColor;

        if (value < 0.5f)
        {
            startColor = Color.red;
            endColor = Color.yellow;
            mainModule.startColor = Color.Lerp(startColor, endColor, value * 2);
        }
        else
        {
            startColor = Color.yellow;
            endColor = Color.cyan;
            mainModule.startColor = Color.Lerp(startColor, endColor, (value - 0.5f) * 2);
        }

        temperatureText.text = temperature.ToString("F0") + "°C";
    }

    public float GetTemperature()
    {
        return temperature;
    }

}
