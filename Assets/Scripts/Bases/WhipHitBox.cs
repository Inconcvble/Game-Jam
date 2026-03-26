using Interfaces;
using UnityEngine;

namespace Bases
{
    public class WhipHitBox : MonoBehaviour
    {
        [SerializeField] private int damage = 15;

        private void OnTriggerEnter2D(Collider2D other)
        {
            IDamageable target = other.GetComponent<IDamageable>();
            if (target != null)
            {
                target.TakeDamage(damage);
            }
            Destroy(gameObject);
        }
    }
}