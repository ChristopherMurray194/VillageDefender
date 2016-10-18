using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SliderText : MonoBehaviour
{
    Text text;
    Slider slider;
    float percentage = 0f;

    void Awake()
    {
        text = GetComponent<Text>();
        slider = GetComponentInParent<Slider>();
        percentage = slider.value * 100f;
        text.text = ((int)percentage).ToString() + "%";
    }

    void Update()
    {
        percentage = slider.value * 100f;
        text.text = ((int)percentage).ToString() + "%";
    }
}
