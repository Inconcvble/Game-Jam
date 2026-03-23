using Interfaces;
using UnityEngine;

namespace Enemy
{
    public class GemThrowable : MonoBehaviour
    {
        [SerializeField] private int _damage = 10;
        [SerializeField] private float _speed = 8f;
        private Rigidbody2D _rb;
        private Vector2 _direction;

        void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
        }

        public void SetDirection(Vector2 direction)
        {
            _direction = direction.normalized;
        }

        void FixedUpdate()
        {
            _rb.linearVelocity = _direction * _speed;
        }
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            IDamageable target = other.GetComponent<IDamageable>();
            if (target != null)
            {
                target.TakeDamage(_damage);
                Debug.Log("Damage Dealt");
            }
            Destroy(gameObject);
        }
    }
}