using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioMixer m_AudioMixer;
    private static string k_ParameterName = "Master";
    public void SetVolume(float volume)
    {
        m_AudioMixer.SetFloat(k_ParameterName, volume);
    }
    
    public float GetVolume()
    {
        m_AudioMixer.GetFloat(k_ParameterName, out float volume);
        return volume;
    }
}