using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
    public abstract class BasePanelView : MonoBehaviour
    {
        [SerializeField] private GameObject _panel;
        [SerializeField] private Button _togglePanel;

        private void Awake()
        {
            _togglePanel.onClick.AddListener(ToggleShopPanel);
        }
        
        [ContextMenu("Toggle")]
        public void ToggleShopPanel() => _panel.SetActive(!_panel.activeSelf);
        
        [ContextMenu("Show")]
        public void Show() => _panel.SetActive(true);
        [ContextMenu("Hide")]
        public void Hide() => _panel.SetActive(false);
    }
}