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
        
        private void ToggleShopPanel() => _panel.SetActive(!_panel.activeSelf);
    }
}