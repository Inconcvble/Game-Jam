using System.Collections;
using Interfaces;
using UnityEngine;

namespace Enemy
{
    public class BatImp : Imp
    {
        [SerializeField] private int diveDamage = 20;
        [SerializeField] private float diveSpeed = 12f;
        [SerializeField] private float patrolSpeed = 2f;
        [SerializeField] private float patrolRange = 4f;
        [SerializeField] private float pullUpSpeed = 5f;
        [SerializeField] private float diveCooldown = 3f;

        private float _lastDiveTime;
        private bool _isDiving;
        private bool _isPullingUp;
        private Vector2 _startPosition;
        private int _patrolDirection = 1;

        protected override void Awake()
        {
            base.Awake();
            _startPosition = transform.position;
        }
        
        protected override void FixedUpdate()
        {
            if (_isDiving)
            {
                Rb.linearVelocity = Vector2.down * diveSpeed;
            }
            else if (_isPullingUp)
            {
                Vector2 directionUp = (_startPosition - (Vector2)transform.position).normalized;
                Rb.linearVelocity = directionUp * pullUpSpeed;

                if (Vector2.Distance(transform.position, _startPosition) < 0.3f)
                {
                    _isPullingUp = false;
                    Rb.linearVelocity = Vector2.zero;
                }
            }
            else
            {
                Rb.linearVelocity = new Vector2(_patrolDirection * patrolSpeed, 0);

                float distanceFromStart = transform.position.x - _startPosition.x;
                if (Mathf.Abs(distanceFromStart) >= patrolRange)
                {
                    _patrolDirection *= -1;
                }
                
                if (PlayerDetected && Time.time - _lastDiveTime >= diveCooldown)
                {
                    _lastDiveTime = Time.time;
                    _isDiving = true;
                    StartCoroutine(DiveRoutine());
                }
            }
        }
        
        private IEnumerator DiveRoutine()
        {
            yield return new WaitForSeconds(0.8f);
            _isDiving = false;
            _isPullingUp = true;
        }
        
        private void OnCollisionEnter2D(Collision2D col)
        {
            if (!_isDiving) return;
            IDamageable target = col.gameObject.GetComponent<IDamageable>();
            if (target != null)
                target.TakeDamage(diveDamage);
        }
    }
}