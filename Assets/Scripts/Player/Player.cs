using System;
using Interfaces;
using UnityEngine;
using UnityEngine.Serialization;

namespace Player
{
    public class Player : MonoBehaviour, IDamageable
    {
        [SerializeField] public HealthSystem health;

        private void Awake()
        {
            if (health == null)
            {
                health = GetComponent<HealthSystem>();
            }

            health.OnHealthChanged += OnHealthChanged;
        }

        // ReSharper disable Unity.PerformanceAnalysis
        private void OnHealthChanged()
        {
            Debug.Log("Player Health: " + health.CurrentHealth);
        }

        public void TakeDamage(int damageAmount)
        {
            health.Damage(damageAmount);
        }

        public void Heal(int healAmount)
        {
            health.Heal(healAmount);
        }
    }
}