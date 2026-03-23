using System.Collections;
using Interfaces;
using UnityEngine;

namespace Enemy
{
    public class RollingImp : Imp
    {
        [SerializeField] private int contactDamage = 15;
        [SerializeField] private float chargeSpeed = 10f;
        [SerializeField] private float chargeWindup = 0.8f;
        [SerializeField] private float chargeDuration = 0.6f;
        [SerializeField] private float chargeCooldown = 2f;
        [SerializeField] private float chargeRange = 1.2f;

        private float _lastChargeTime;
        private bool _isCharging;
        private bool _isWindingUp;
        
        protected override void FixedUpdate()
        {
            if (!PlayerDetected)
            {
                Rb.linearVelocity = Vector2.zero;
                return;
            }

            if (!_isCharging && !_isWindingUp)
            {
                float timeSinceLastCharge = Time.time - _lastChargeTime;
                if (timeSinceLastCharge >= chargeCooldown)
                {
                    MoveTowardPlayer();
                    
                    float distance = Vector2.Distance(transform.position, Player.position);
                    
                    if (distance <= chargeRange)
                    {
                        Rb.linearVelocity = Vector2.zero;
                        StartCoroutine(ChargeRoutine());
                    }
                }
            }
        }
        
        private IEnumerator ChargeRoutine()
        {
            _isWindingUp = true;
            Rb.linearVelocity = Vector2.zero;
            yield return new WaitForSeconds(chargeWindup); // pause here

            _isWindingUp = false;
            _isCharging = true;

            Vector2 direction = (Player.position - transform.position).normalized;
            Rb.linearVelocity = direction * chargeSpeed;
            yield return new WaitForSeconds(chargeDuration); // charge lasts this long

            _isCharging = false;
            _lastChargeTime = Time.time;
            Rb.linearVelocity = Vector2.zero;
        }
        
        private void OnCollisionEnter2D(Collision2D col)
        {
            if (!_isCharging) return;
            IDamageable target = col.gameObject.GetComponent<IDamageable>();
            if (target != null)
                target.TakeDamage(contactDamage);
        }
    }
}