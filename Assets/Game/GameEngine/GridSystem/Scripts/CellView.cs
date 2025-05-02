using UnityEngine;

namespace Game.GameEngine.GridSystem
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class CellView : MonoBehaviour
    {
        [SerializeField] private Color _highlightColor;
        [SerializeField] private Color _defaultColor;
        
        private SpriteRenderer _spriteRenderer;
        private void Start()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }
        [ContextMenu("Highlight")]
        public void Highlight()
        {
            _spriteRenderer.color = _highlightColor;
        }
        [ContextMenu("Unhighlight")]
        public void Unhighlight()
        {
            _spriteRenderer.color = _defaultColor;
        }
        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}