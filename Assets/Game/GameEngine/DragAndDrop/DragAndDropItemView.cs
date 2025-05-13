using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Game.GameEngine.DragAndDrop
{
    [RequireComponent(typeof(RectTransform)), RequireComponent(typeof(CanvasGroup))]
    public class DragAndDropItemView : MonoBehaviour, IDragAndDropItemView, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        public event Action OnBeginDrag;
        public event Action<Vector2> OnDrag;
        public event Action OnEndDrag;
        
        [SerializeField] private RectTransform _rectTransform;
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private float _onDragAlpha = 0.35f;

        void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
        {
            _canvasGroup.alpha = _onDragAlpha;
            OnBeginDrag?.Invoke();
        }

        void IDragHandler.OnDrag(PointerEventData eventData)
        {
            _rectTransform.anchoredPosition += eventData.delta;
            
            OnDrag?.Invoke(eventData.position);
        }

        void IEndDragHandler.OnEndDrag(PointerEventData eventData)
        {
            _canvasGroup.alpha = 1f;
            OnEndDrag?.Invoke();
        }
    }
    
    public interface IDragAndDropItemView
    {
        event Action OnBeginDrag;
        event Action<Vector2> OnDrag;
        event Action OnEndDrag;
    }
}