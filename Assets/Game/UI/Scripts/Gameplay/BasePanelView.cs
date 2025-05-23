using System;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
    public abstract class BasePanelView : MonoBehaviour
    {
        [SerializeField] protected GameObject _panel;
        [SerializeField] protected Button _togglePanel;


        private void OnEnable()
        {
            _togglePanel.onClick.AddListener(ToggleShopPanel);
        }

        private void OnDisable()
        {
            _togglePanel.onClick.RemoveListener(ToggleShopPanel);
        }


        [ContextMenu("Toggle")]
        public void ToggleShopPanel() => _panel.SetActive(!_panel.activeSelf);
        
        [ContextMenu("Show")]
        public void Show() => _panel.SetActive(true);
        [ContextMenu("Hide")]
        public void Hide() => _panel.SetActive(false);
    }
}