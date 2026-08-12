using UnityEngine;

namespace Sword
{
    public class Sword : MonoBehaviour
    {
        SwordInput m_SwordInput;
        SwordAnimator m_SwordAnimator;

        private void Awake()
        {
            m_SwordInput = GetComponent<SwordInput>();
            m_SwordAnimator = GetComponent<SwordAnimator>();
        }

        private void Update()
        {
            m_SwordAnimator.Animate(m_SwordInput.ClickInput);
        }
    }
}