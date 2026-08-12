using System;
using UnityEngine;

namespace Sword
{
    public class SwordAudio : MonoBehaviour
    {
        private AudioSource m_SwordSwingSound;

        private void Awake()
        {
            m_SwordSwingSound = GetComponent<AudioSource>();
        }

        private void StartSwingSound()
        {
            m_SwordSwingSound.Play();
        }
    }
}