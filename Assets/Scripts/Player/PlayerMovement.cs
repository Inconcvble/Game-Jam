using UnityEngine;
using UnityEngine.InputSystem;

namespace Player {
    public class PlayerMovement : MonoBehaviour {
        
        [SerializeField] private Rigidbody2D rb;
        [SerializeField] private InputAction playerControls;
        [SerializeField] private float moveSpeed;
        [SerializeField] private Vector2 moveDirection;

        void Start() {
            rb = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            moveDirection.x = playerControls.ReadValue<Vector2>().x;
            moveDirection.y = playerControls.ReadValue<Vector2>().y;
        }

        void FixedUpdate() {
            rb.linearVelocity = new Vector2(moveDirection.x * moveSpeed, moveDirection.y * moveSpeed);
        }

        private void OnEnable() {
            playerControls.Enable();
        }

        private void OnDisable() {
            playerControls.Disable();
        }
    }
}