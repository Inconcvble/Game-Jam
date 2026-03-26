using UnityEngine;
using UnityEngine.EventSystems;

namespace MenuUI
{
    public class ButtonHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        private Vector3 _originalScale;
    
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            _originalScale = transform.localScale;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            transform.localScale = _originalScale * 1.1f; 
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            transform.localScale = _originalScale;
        }
    }
}

