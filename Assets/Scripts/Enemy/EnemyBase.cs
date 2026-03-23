using Core;
using UnityEngine;
using Interfaces;

namespace Enemy
{
    public class EnemyBase : MonoBehaviour, IDamageable
    {
        [SerializeField] private int maxHealth = 50;
        private int _currentHealth;
        private bool _isDead;
        
        [SerializeField] private LootTable _lootTable;
        
        protected virtual void Awake()
        {
            _currentHealth = maxHealth;
        }
        
        public void TakeDamage(int amount)
        {
            _currentHealth -= amount;
            _currentHealth = Mathf.Max(_currentHealth, 0);
            if (_currentHealth <= 0 && !_isDead)
            {
                _isDead = true;
                Die();
            }
        }
        
        void Die()
        {
            Destroy(gameObject);
        }
    }
}