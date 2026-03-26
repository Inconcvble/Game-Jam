using Bases;
using UnityEngine;

namespace Enemy
{
    public class Imp : EnemyBase
    {
        [SerializeField] private float detectionRange = 8f;
        [SerializeField] private float moveSpeed = 3f;
        [SerializeField] private LayerMask playerLayer;
        protected Rigidbody2D Rb;

        protected Transform Player;
        protected bool PlayerDetected;

        protected override void Awake()
        {
            base.Awake();
            Rb = GetComponent<Rigidbody2D>();
            Player = GameObject.FindWithTag("Player").transform;
        }
        
        void Update()
        {
            if (Player == null) return;
    
            float distance = Vector2.Distance(transform.position, Player.position);
    
            if (distance <= detectionRange)
            {
                RaycastHit2D hit = Physics2D.Raycast(
                    transform.position,
                    Player.position - transform.position,
                    detectionRange,
                    playerLayer
                );
        
                PlayerDetected = hit.collider != null;
            }
            else
            {
                PlayerDetected = false;
            }
        }
        
        protected virtual void FixedUpdate()
        {
            if (PlayerDetected)
                MoveTowardPlayer();
            else
                Rb.linearVelocity = Vector2.zero;
        }

        protected void MoveTowardPlayer()
        {
            Vector2 direction = (Player.position - transform.position).normalized;
            Rb.linearVelocity = direction * moveSpeed;
        }
    }
}