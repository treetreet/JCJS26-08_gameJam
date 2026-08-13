using UnityEngine;
using UnityEngine.UI;

public class GimmickManager : MonoBehaviour
{
    public static GimmickManager instance = null;

    [Header("기믹 슬라이더")]
    [SerializeField] public Slider m_LightSlider;
    [SerializeField] public Slider m_SoundSlider;
    private AudioManager m_AudioManager;

    [SerializeField] private float m_MaxVolume = 0;
    [SerializeField] private float m_MinVolume = -40;

    [Header("기믹 이미지")]
    [SerializeField] private Image m_DarkImage;



    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        m_AudioManager = GetComponent<AudioManager>();
        m_SoundSlider.onValueChanged.AddListener(ChangeVolume);
        m_LightSlider.onValueChanged.AddListener(ChangeLight);

        m_SoundSlider.maxValue = m_MaxVolume;
        m_SoundSlider.minValue = m_MinVolume;
        
        ChangeVolume(m_SoundSlider.value);
        ChangeLight(m_LightSlider.value);
    }

    private void ChangeLight(float value)
    {
        m_DarkImage.color = new Color(0, 0, 0, 1 - value);
    }
    private void ChangeVolume(float value)
    {
        m_AudioManager.SetVolume(value);
    }
}
