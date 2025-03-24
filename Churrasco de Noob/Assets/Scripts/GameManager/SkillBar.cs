using UnityEngine;
using UnityEngine.UI;

public class SkillBar : MonoBehaviour
{
    public static SkillBar Instance;
    public Slider slider;
    public float decreaseRate = 1f; 
    private float currentValue;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        if (slider == null)
        {
            return;
        }

        currentValue = slider.maxValue;
        slider.value = currentValue;
    }

    void Update()
    {
        if (currentValue > 0)
        {
            currentValue -= decreaseRate * Time.deltaTime;
            slider.value = currentValue;
        }

        if (currentValue <= 0)
        {
            OnSliderEmpty();
        }
    }

    public void IncreaseSlider(float amount)
    {
        currentValue += amount;
        if (currentValue > slider.maxValue)
        {
            currentValue = slider.maxValue;
        }
        slider.value = currentValue;
    }
    public void DecreaseSlider(float amount)
    {
        currentValue -= amount;
        if (currentValue > slider.maxValue)
        {
            currentValue = slider.maxValue;
        }
        slider.value = currentValue;
    }

    private void OnSliderEmpty()
    {
        Debug.Log("befana");
    }
}
