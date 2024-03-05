using UnityEngine;
using UnityEngine.UI;

public class GateCounter : MonoBehaviour
{
    public Text text;
    public Slider slider;

    public void UpdateUI(int value, int maxGatesCount)
    {
        slider.value = value;
        slider.minValue = 0;
        slider.maxValue = maxGatesCount;
        text.text = $"{value} / {maxGatesCount}";
    }
}
