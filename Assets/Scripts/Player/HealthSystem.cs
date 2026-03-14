using System;
using UnityEngine;

namespace Player
{
    public class HealthSystem : MonoBehaviour
    {
        public int maxHealth = 100;
        private int _currentHealth;

        public event Action OnHealthChanged;

        public int CurrentHealth => _currentHealth;

        private void Awake()
        {
            _currentHealth = maxHealth;
        }

        public void Damage(int amount)
        {
            _currentHealth -= amount;
            _currentHealth = Mathf.Max(_currentHealth, 0);
            OnHealthChanged?.Invoke();
        }

        public void Heal(int amount)
        {
            _currentHealth += amount;
            _currentHealth = Mathf.Min(_currentHealth, maxHealth);
            OnHealthChanged?.Invoke();
        }
    }
}