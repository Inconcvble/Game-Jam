using Interfaces;
using UnityEngine;

namespace Environment
{
    public class Trap : MonoBehaviour
    {
        [SerializeField] private int damage;
        private void OnTriggerEnter2D(Collider2D other)
        {
            IDamageable target = other.GetComponent<IDamageable>();
            if (target != null)
            {
                target.TakeDamage(damage);
                Debug.Log("Damage Dealt");
            }
        }
    }
}