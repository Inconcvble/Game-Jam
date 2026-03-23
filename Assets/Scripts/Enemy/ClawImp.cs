using Interfaces;
using UnityEngine;

namespace Enemy
{
    public class ClawImp : Imp
    {
        [SerializeField] private float attackRange = 1.2f;
        [SerializeField] private int scratchDamage = 10;
        [SerializeField] private float attackCooldown = 1f;
        private float _lastAttackTime;
        
        protected override void FixedUpdate()
        {
            if (!PlayerDetected) 
            {
                Rb.linearVelocity = Vector2.zero;
                return;
            }

            float distance = Vector2.Distance(transform.position, Player.position);

            if (distance <= attackRange)
            {
                Rb.linearVelocity = Vector2.zero;
                TryAttack();
            }
            else
            {
                MoveTowardPlayer();
            }
        }
        
        private void TryAttack()
        {
            if (Time.time - _lastAttackTime >= attackCooldown)
            {
                _lastAttackTime = Time.time;
                IDamageable target = Player.GetComponent<IDamageable>();
                if (target != null)
                {
                    target.TakeDamage(scratchDamage);
                    Debug.Log("Damage Dealt");
                }
            }
        }
    }
}