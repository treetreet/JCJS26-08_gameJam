using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI_Scripts
{
    public class AudioPresenter : MonoBehaviour
    {
        [SerializeField] private Slider m_VolumeSlider;
        private AudioManager m_AudioManager;
        
        [SerializeField] private float m_MaxVolume = 0;
        [SerializeField] private float m_MinVolume = -40;

        private void Awake()
        {
            m_AudioManager = GetComponent<AudioManager>();
            m_VolumeSlider.onValueChanged.AddListener(ChangeVolume);
            ChangeVolume(m_VolumeSlider.value);
            m_VolumeSlider.maxValue = m_MaxVolume;
            m_VolumeSlider.minValue = m_MinVolume;
        }

        private void ChangeVolume(float value)
        {
            m_AudioManager.SetVolume(value);
        }
    }
}