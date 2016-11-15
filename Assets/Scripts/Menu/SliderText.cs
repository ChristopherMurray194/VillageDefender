using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SliderText : MonoBehaviour
{
    Text text;
    Slider slider;
    /// <summary> percentage of the slider that is filled </summary>
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
