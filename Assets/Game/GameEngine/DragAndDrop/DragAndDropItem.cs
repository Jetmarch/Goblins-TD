using System;
using Game.GameEngine.Common;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Game.GameEngine.DragAndDrop
{
    [RequireComponent(typeof(CanvasGroup))]
    [Prototype]
    public class DragAndDropItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        public event Action<PointerEventData> BeginDrag;
        public event Action<PointerEventData> Drag;
        public event Action<PointerEventData, Vector2> EndDrag;
        
        [SerializeField] private RectTransform _rectTransform;
        
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private float _onDragAlpha = 0.35f;
        
        private Vector3 _startPosition;
        
        private void Start()
        {
            _rectTransform = GetComponent<RectTransform>();
            _canvasGroup = GetComponent<CanvasGroup>();
            
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            _startPosition = transform.position;
            _canvasGroup.alpha = _onDragAlpha;
            BeginDrag?.Invoke(eventData);
        }

        public void OnDrag(PointerEventData eventData)
        {
            _rectTransform.anchoredPosition += eventData.delta;
            Drag?.Invoke(eventData);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            _canvasGroup.alpha = 1f;
            EndDrag?.Invoke(eventData, _startPosition);
        }
    }
}