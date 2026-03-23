using Interfaces;
using UnityEngine;

namespace Enemy
{
    public class ThrowerImp : Imp
    {
        [SerializeField] private float preferredRange = 5f;
        [SerializeField] private float retreatSpeed = 2f;
        [SerializeField] private float throwCooldown = 2f;
        [SerializeField] private GameObject gemPrefab;
        private float _lastThrowTime;
        
        protected override void FixedUpdate()
        {
            if (!PlayerDetected)
            {
                Rb.linearVelocity = Vector2.zero;
                return;
            }

            float distance = Vector2.Distance(transform.position, Player.position);

            if (distance < preferredRange)
            {
                // move away from player
                Vector2 retreatDir = (transform.position - Player.position).normalized;
                Rb.linearVelocity = retreatDir * retreatSpeed;
            }
            else
            {
                Rb.linearVelocity = Vector2.zero;
                TryThrow();
            }
        }

        void TryThrow()
        {
            if (Time.time - _lastThrowTime >= throwCooldown)
            {
                _lastThrowTime = Time.time;
                
                GameObject gem = Instantiate(gemPrefab, transform.position, Quaternion.identity);
                GemThrowable gemScript = gem.GetComponent<GemThrowable>();
                gemScript.SetDirection(Player.position - transform.position);
            }
        }
    }
}