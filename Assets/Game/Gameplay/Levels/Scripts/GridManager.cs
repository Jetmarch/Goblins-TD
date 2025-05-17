using System;
using UnityEngine;

namespace Game.Gameplay.Levels
{
    public class GridManager : MonoBehaviour
    {
        public LevelGrid Grid => _levelGrid;

        [SerializeField] private LevelGridConfig _levelGridConfig;

        private LevelGrid _levelGrid;
        [SerializeField] private bool _showGrid;
        #region Unity Callbacks
        
        private void OnDrawGizmos()
        {
            if (_levelGridConfig == null) return;
            if (!_showGrid) return;

            if (_levelGrid == null)
            {
                if (_levelGridConfig.LevelGrid == null)
                {
                    Debug.LogWarning($"LevelGridConfig {_levelGridConfig.name} does not contain LevelGrid");
                    return;
                }
                _levelGrid = _levelGridConfig.GetPrototype();
            }
            
            UpdateGridWorldPosition(transform.position);
            LevelGridUseCases.DebugDrawGrid(_levelGrid);
        }

        private void Awake()
        {
            _levelGrid = _levelGridConfig.GetPrototype();
        }

        #endregion

        public void ShowGrid()
        {
            _showGrid = true;
        }

        public void HideGrid()
        {
            _showGrid = false;
        }

        public void UpdateGridWorldPosition(Vector3 position)
        {
            _levelGrid.WorldPosition = position;
        }
    }
}