using System;
using UnityEngine;
using UnityEngine.UI;

public class LightPresenter : MonoBehaviour
{
    [SerializeField] private Slider m_LightSlider;
    [SerializeField] private Image m_DarkImage;

    private void Update()
    {
        m_DarkImage.color = new Color(0,0,0,1-m_LightSlider.value);
    }
}
