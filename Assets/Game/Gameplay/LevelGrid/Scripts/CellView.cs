using UnityEngine;

namespace Game.Gameplay.LevelGrid
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class CellView : MonoBehaviour
    {
        [SerializeField] private Color _highlightColorEmpty;
        [SerializeField] private Color _highlightColorBusy;
        [SerializeField] private Color _defaultColor;
        [SerializeField] private SpriteRenderer _spriteRenderer;

        private bool _isBusy;
        private bool _isWalkable;
        
        private void Start()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }
        
        public void Highlight()
        {
            _spriteRenderer.color = _isBusy || _isWalkable ? _highlightColorBusy : _highlightColorEmpty;
        }
        public void Unhighlight()
        {
            _spriteRenderer.color = _defaultColor;
        }

        public void SetBusy(bool isBusy)
        {
            _isBusy = isBusy;
        }
        
        public void SetWalkable(bool isWalkable)
        {
            _isWalkable = isWalkable;
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