using System.Collections;
using Interfaces;
using UnityEngine;

namespace Bases
{
    public class GrabHitBox : MonoBehaviour
    {
        private Rigidbody2D _playerRb;
        [SerializeField] private int damage = 10;
        [SerializeField] private float rootDuration = 1.5f;
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            _playerRb = other.GetComponent<Rigidbody2D>();
            IDamageable target = other.GetComponent<IDamageable>();
            if (target != null)
            {
                target.TakeDamage(damage);
                StartCoroutine(RootPlayer(_playerRb));
            }
        }
        
        private IEnumerator RootPlayer(Rigidbody2D playerRb)
        {
            playerRb.constraints = RigidbodyConstraints2D.FreezeAll;
            yield return new WaitForSeconds(rootDuration);
            playerRb.constraints = RigidbodyConstraints2D.FreezeRotation;
            Destroy(gameObject);
        }
    }
}