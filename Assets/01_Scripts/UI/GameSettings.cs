using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class GameSettings : MonoBehaviour
{
    [SerializeField] private Slider m_AudioSlider;
    [SerializeField] private Slider m_LightSlider;

    [SerializeField] private float m_AudioSliderSensitivity = 1;
    [SerializeField] private float m_LightSliderSensitivity = 0.02f;

    private InputSystem_Actions m_Actions;

    private void OnEnable()
    {
        m_Actions = new InputSystem_Actions();
        m_Actions.Enable();

        m_Actions.UI.Volume.performed += OnVolumeScroll;
        m_Actions.UI.Light.performed += OnLightScroll;

        m_AudioSlider.value = -20;
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

        m_AudioSlider.value += scrollValue * m_AudioSliderSensitivity;
    }

    private void OnLightScroll(InputAction.CallbackContext context)
    {
        float scrollValue = context.ReadValue<float>();

        m_LightSlider.value += scrollValue * m_LightSliderSensitivity;
    }
}