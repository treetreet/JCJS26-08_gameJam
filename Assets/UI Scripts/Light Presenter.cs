using System;
using UnityEngine;
using UnityEngine.UI;

public class LightPresenter : MonoBehaviour
{
    [SerializeField] private Slider m_LightSlider;
    [SerializeField] private Image m_DarkImage;

    private void Awake()
    {
        m_LightSlider.onValueChanged.AddListener(ChangeLight);
        ChangeLight(m_LightSlider.value);
    }

    private void ChangeLight(float value)
    {
        m_DarkImage.color = new Color(0,0,0,1-value);
    }
}
