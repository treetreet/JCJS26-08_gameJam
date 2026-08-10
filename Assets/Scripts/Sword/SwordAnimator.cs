using System;
using UnityEngine;

namespace Sword
{
    public class SwordAnimator : MonoBehaviour
    {
        Animator m_Animator;
        
        private static string k_AttackState1 = "Attack1";
        private static string k_AttackState2 = "Attack2";
        
        public int AttackCombo { get; private set; }
        public void SetAttackState(int value)
        {
            Debug.Log($"SetAttackState 호출됨: {value}");
            
            AttackCombo = value;
        }
        private void Awake()
        {
            m_Animator = GetComponent<Animator>();
            AttackCombo = 1;
        }
        
        public void Animate(bool clicked)
        {
            if(!clicked) return;
            
            Debug.Log(AttackCombo);
            
            if (AttackCombo == 1)
            {
                // 1번 어택 하기
                m_Animator.SetTrigger(k_AttackState1);
            }
            else if (AttackCombo != 1 && AttackCombo != 2 && AttackCombo != 0)
            {
                // 오류
                Debug.LogError("잘못된 어택 콤보");
                AttackCombo = 1;
            }
            
            // 2번 어택 셋팅
            m_Animator.SetBool(k_AttackState2, AttackCombo == 2);
        }
    }
}