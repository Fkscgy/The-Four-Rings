using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderBehaviour : MonoBehaviour
{
    [SerializeField]
    Slider slider;
    [SerializeField]
    Text text;
    [SerializeField]
    IPlayer player;
    public void SetSlider(int value, int maxvalue)
    {
        slider.value = value;
        slider.maxValue = maxvalue;
        text.text = $"{value}/{maxvalue}";
    }
}
