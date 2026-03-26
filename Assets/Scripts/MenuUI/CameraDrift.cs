using UnityEngine;

namespace MenuUI
{
    public class CameraDrift : MonoBehaviour
    {
        public float driftSpeed = 0.3f;
        public float driftAmount = 0.15f;

        Vector3 _startPos;

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
         
            float x = Mathf.Sin(Time.time * driftSpeed) * driftAmount;
            float y = Mathf.Cos(Time.time * driftSpeed) * driftAmount;

            transform.position = _startPos + new Vector3(x, y, 0);

        }
    }
}
