using System.Collections.Generic;
using Game.GameEngine.Common;
using Game.Gameplay.Levels;
using UnityEngine;
using VContainer;

namespace Game.UI.MainMenu
{
    [Prototype]
    public sealed class LevelListView : BasePanelView
    {
        [SerializeField] private LevelItemView _levelItemViewPrefab;
        [SerializeField] private RectTransform _levelItemParent;
        [SerializeField] private List<LevelItemView> _levelItemViews;
        
        

        private void Start()
        {
            _levelItemViews = new();
            
            foreach (var level in LevelManager.Levels)
            {
                var levelItemView = Instantiate(_levelItemViewPrefab, _levelItemParent);
                levelItemView.Configure(level.LevelName);
                levelItemView.StartLevel += OnStartLevel;
                _levelItemViews.Add(levelItemView);
            }
        }

        private void OnStartLevel(string levelName)
        {
            LevelManager.LoadLevel(levelName);
        }
    }
}