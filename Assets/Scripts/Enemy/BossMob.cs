using System.Collections;
using Bases;
using UnityEngine;

namespace Enemy
{
    public class BossMob : EnemyBase
    {
        [SerializeField] private float moveSpeed = 3f;
        [SerializeField] private float attackCooldown = 2f;
        [SerializeField] private float grabDistance = 8f;
        [SerializeField] private float smashDistance = 2f;
        [SerializeField] private GameObject gemPrefab;
        [SerializeField] private GameObject grabPrefab;
        [SerializeField] private float grabTelegraphDuration = 0.5f;
        [SerializeField] private GameObject whipPrefab;
        [SerializeField] private float whipTelegraphDuration = 0.4f;
        [SerializeField] private GameObject shieldSmashPrefab;
        [SerializeField] private float smashTelegraphDuration = 0.8f;
        
        private Transform _player;
        private Rigidbody2D _rb;
        private float _lastAttackTime;
        private int _consecutiveRangedHits;
        private int _consecutiveMeleeHits;
        private bool _isAttacking;

        protected override void Awake()
        {
            base.Awake();   
            _rb = GetComponent<Rigidbody2D>();
            _player = GameObject.FindWithTag("Player").transform;
        }

        void Update()
        {
                if (_player == null || _isAttacking) return;

                MoveTowardPlayer();

                if (Time.time - _lastAttackTime >= attackCooldown)
                {
                    ChooseAttack();
                }
        }
        
        private void MoveTowardPlayer()
        {
            float distance = Vector2.Distance(transform.position, _player.position);
            if (distance <= smashDistance) 
            {
                _rb.linearVelocity = Vector2.zero;
                return;
            }
            Vector2 direction = (_player.position - transform.position).normalized;
            _rb.linearVelocity = direction * moveSpeed;
        }
        
        private void ChooseAttack()
        {
            _lastAttackTime = Time.time;
            float distance = Vector2.Distance(transform.position, _player.position);

            if (_consecutiveRangedHits >= 2 && distance > grabDistance)
            {
                StartCoroutine(GrabAttack());
                return;
            }

            if (_consecutiveMeleeHits >= 2 && distance < smashDistance)
            {
                StartCoroutine(ShieldSmashAttack());
                return;
            }

            int roll = Random.Range(0, 3);
            switch (roll)
            {
                case 0: StartCoroutine(GemThrowAttack()); break;
                case 1: StartCoroutine(WhipAttack()); break;
                case 2: StartCoroutine(ShieldSmashAttack()); break;
            }
        }

        private IEnumerator ShieldSmashAttack()
        {
            _isAttacking = true;
            
            _rb.linearVelocity = Vector2.zero;
            yield return new WaitForSeconds(smashTelegraphDuration);
            
            Vector2 spawnPos = (Vector2)transform.position;
            
            GameObject shield = Instantiate(shieldSmashPrefab, spawnPos, Quaternion.identity);
            
            yield return new WaitForSeconds(0.3f);
            _consecutiveMeleeHits++;
            _consecutiveRangedHits = 0;
            _isAttacking = false;
        }

        private IEnumerator WhipAttack()
        {
            _isAttacking = true;
            
            _rb.linearVelocity = Vector2.zero;
            yield return new WaitForSeconds(whipTelegraphDuration);
            
            Vector2 direction = (_player.position - transform.position).normalized;
            Vector2 spawnPos = (Vector2)transform.position + direction * 1.5f;
            
            GameObject whip = Instantiate(whipPrefab, spawnPos, Quaternion.identity);
            
            yield return new WaitForSeconds(0.3f);
            Destroy(whip);
            _consecutiveMeleeHits++;
            _consecutiveRangedHits = 0;
            _isAttacking = false;
        }

        private IEnumerator GemThrowAttack()
        {
            _isAttacking = true;
    
            // throw 3 gems in a spread
            for (int i = 0; i < 3; i++)
            {
                GameObject gem = Instantiate(gemPrefab, transform.position, Quaternion.identity);
                GemThrowable gemScript = gem.GetComponent<GemThrowable>();
        
                // spread the gems by offsetting the angle slightly
                Vector2 baseDirection = (_player.position - transform.position).normalized;
                float angle = (i - 1) * 20f; // -20, 0, +20 degrees
                Vector2 spreadDirection = RotateVector(baseDirection, angle);
        
                gemScript.SetDirection(spreadDirection);
                yield return new WaitForSeconds(0.15f); // slight delay between each gem
            }
    
            _consecutiveRangedHits++;
            _consecutiveMeleeHits = 0;
            _isAttacking = false;
        }
        
        private Vector2 RotateVector(Vector2 v, float degrees)
        {
            float radians = degrees * Mathf.Deg2Rad;
            float cos = Mathf.Cos(radians);
            float sin = Mathf.Sin(radians);
            return new Vector2(cos * v.x - sin * v.y, sin * v.x + cos * v.y);
        }
        
        private IEnumerator GrabAttack()
        {
            _isAttacking = true;

            // telegraph pause
            _rb.linearVelocity = Vector2.zero;
            yield return new WaitForSeconds(grabTelegraphDuration);

            // spawn grab hitbox reaching toward player
            Vector2 direction = (_player.position - transform.position).normalized;
            Vector2 spawnPos = (Vector2)transform.position + direction * 1.5f;
    
            GameObject grab = Instantiate(grabPrefab, spawnPos, Quaternion.identity);
    
            _consecutiveRangedHits = 0;
            _consecutiveMeleeHits++;
            _isAttacking = false;
        }
    }
}