using UnityEngine;

namespace Game.Gameplay.LevelGrid
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class CellView : MonoBehaviour
    {
        [SerializeField] private Color _highlightColorEmpty;
        [SerializeField] private Color _highlightColorBusy;
        [SerializeField] private Color _defaultColor;
        [SerializeField] private Color _walkableColor;
        [SerializeField] private SpriteRenderer _spriteRenderer;

        private bool _isBusy;
        private LevelCellType _type;
        
        private void Start()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }
        
        public void Highlight()
        {
            _spriteRenderer.color = _type switch
            {
                LevelCellType.Walkable => _walkableColor,
                LevelCellType.Buildable => _isBusy ? _highlightColorBusy : _highlightColorEmpty,
                _ => _defaultColor
            };
        }
        public void Unhighlight()
        {
            _spriteRenderer.color = GetDefaultColorByType();
        }

        private Color GetDefaultColorByType()
        {
            return _type == LevelCellType.Walkable ? _walkableColor : _defaultColor;
        }

        public void SetBusy(bool isBusy)
        {
            _isBusy = isBusy;
        }

        public void SetType(LevelCellType type)
        {
            _type = type;
        }
        
        public void Show()
        {
            gameObject.SetActive(true);
            if (_type == LevelCellType.Walkable)
            {
                Highlight();
            }
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}