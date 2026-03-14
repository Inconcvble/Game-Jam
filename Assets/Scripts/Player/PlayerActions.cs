using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class PlayerActions : MonoBehaviour
    {
        [SerializeField] private InputAction healButton;
        [SerializeField] private Player player;

        private void Update()
        {
            if (healButton.IsPressed() && player is not null)
            {
                player.Heal(20);
            }
        }

        private void OnEnable()
        {
            healButton.Enable();
        }

        private void OnDisable()
        {
            healButton.Disable();
        }
    }
}