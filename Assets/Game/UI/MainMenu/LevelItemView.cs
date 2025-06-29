using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI.MainMenu
{
    public class LevelItemView : MonoBehaviour
    {
        public event Action<string> StartLevel;
        
        [SerializeField] private TextMeshProUGUI _levelNameText;
        [SerializeField] private Button _startLevelBtn;
        
        public void Configure(string levelName)
        {
            _levelNameText.text = levelName;
        }

        private void OnEnable()
        {
            _startLevelBtn.onClick.AddListener(NotifyStartLevel);
        }
        
        private void OnDisable()
        {
            _startLevelBtn.onClick.RemoveListener(NotifyStartLevel);
        }

        private void NotifyStartLevel()
        {
            StartLevel?.Invoke(_levelNameText.text);
        }
    }
}