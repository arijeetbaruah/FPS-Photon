using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using TMPro;

public class PlayerHPUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI txt;
    [SerializeField] private Slider slider;
    
    private float maxValue;

    public float MaxValue
    {
        get => maxValue;
        set
        {
            maxValue = value;
            slider.maxValue = maxValue;
        }
    }

    public void SetValue(float val)
    {
        slider.value = val;

        txt.SetText($"{val}/{MaxValue}");
    }
}
