using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class GameSettings : MonoBehaviour
{
    [SerializeField] private Slider m_AudioSlider;
    [SerializeField] private Slider m_LightSlider;

    [SerializeField] private float m_AudioSliderSensitivity = 1f;
    [SerializeField] private float m_LightSliderSensitivity = 0.02f;

    [SerializeField] private Material m_BrightnessMaterial;

    private InputSystem_Actions m_Actions;

    private void OnEnable()
    {
        m_Actions = new InputSystem_Actions();
        m_Actions.Enable();

        m_Actions.UI.Volume.performed += OnVolumeScroll;
        m_Actions.UI.Light.performed += OnLightScroll;

        m_AudioSlider.value = -15;
        m_LightSlider.value = 0.5f;
    }

    private void OnDisable()
    {
        m_Actions.UI.Volume.performed -= OnVolumeScroll;
        m_Actions.UI.Light.performed -= OnLightScroll;

        m_Actions.Disable();
        m_Actions.Dispose();
    }

    private void OnVolumeScroll(InputAction.CallbackContext context)
    {
        // ctrl + scroll != volume -> return;
        if (Keyboard.current.ctrlKey.isPressed) return;
        float scrollValue = context.ReadValue<float>();

        Debug.Log($"ScrollValue : {scrollValue}");

        m_AudioSlider.value += scrollValue * m_AudioSliderSensitivity;
        m_LightSlider.value -= scrollValue * m_LightSliderSensitivity;

        m_BrightnessMaterial.SetFloat("_Radius", m_LightSlider.value);
    }

    private void OnLightScroll(InputAction.CallbackContext context)
    {
        float scrollValue = context.ReadValue<float>();

        Debug.Log($"ScrollValue : {scrollValue}");

        m_LightSlider.value += scrollValue * m_LightSliderSensitivity;
        m_AudioSlider.value -= scrollValue * m_AudioSliderSensitivity;

        m_BrightnessMaterial.SetFloat("_Radius", m_LightSlider.value);
    }
}