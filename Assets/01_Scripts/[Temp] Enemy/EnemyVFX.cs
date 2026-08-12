using System;
using UnityEngine;

namespace TempEnemy
{
    public class EnemyVFX : MonoBehaviour
    {
        private ParticleSystem m_ParticleSystem;

        private void Awake()
        {
            m_ParticleSystem = GetComponent<ParticleSystem>();
        }

        public void DamagedEffect()
        {
            m_ParticleSystem.Play();
        }
    }
}