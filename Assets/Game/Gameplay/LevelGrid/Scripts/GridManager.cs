using Game.GameEngine.Common;
using UnityEngine;
using VContainer;

namespace Game.Gameplay.LevelGrid
{
    [Prototype]
    public sealed class GridManager : MonoBehaviour
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
            UpdateGridWorldPosition(transform.position);
        }

        #endregion

        [Inject]
        private void Configure(LevelGridConfig levelGridConfig)
        {
            _levelGridConfig = levelGridConfig;
        }
        
        public void ShowGrid()
        {
            _showGrid = true;
        }

        public void HideGrid()
        {
            _showGrid = false;
        }

        private void UpdateGridWorldPosition(Vector3 position)
        {
            _levelGrid.WorldPosition = position;
        }
    }
}