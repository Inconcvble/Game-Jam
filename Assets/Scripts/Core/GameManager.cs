using UnityEngine;

namespace Core
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Manager { get; private set; }
        private void Awake()
        {
            if (Manager != null && Manager != this)
            {
                Destroy(gameObject);
            }
            else
            {
                Manager = this;
            }
        }
    }
}