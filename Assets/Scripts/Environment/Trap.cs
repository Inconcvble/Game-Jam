using Interfaces;
using UnityEngine;

namespace Environment
{
    public class Trap : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            IDamageable target = other.GetComponent<IDamageable>();
            if (target != null)
            {
                target.TakeDamage(10);
                Debug.Log("Damage Dealt");
            }
        }
    }
}