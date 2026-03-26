using Interfaces;
using UnityEngine;

namespace Bases
{
    public class ShieldHitBox : MonoBehaviour
    {
        [SerializeField] private int damage = 25;
        [SerializeField] private float lifespan = 0.5f;

        private void Awake()
        {
            Destroy(gameObject, lifespan);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            IDamageable target = other.GetComponent<IDamageable>();

            if (target != null)
            {
                target.TakeDamage(damage);
            }
        }
    }
}