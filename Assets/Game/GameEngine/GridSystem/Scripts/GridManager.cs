using UnityEngine;

namespace Game.GameEngine.GridSystem
{
    public class GridManager : MonoBehaviour
    {
        public Grid Grid => _grid;
        
        [SerializeField] private int _gridWidth;
        [SerializeField] private int _gridHeight;
        [SerializeField] private float _cellSize;
        
        [SerializeField] private bool _showGrid;
        [SerializeField] private Vector2 _cellGap;
        
        private Grid _grid;
        #region Unity Callbacks

        private void Awake()
        {
            ConstructGrid();
        }
        
        private void OnDrawGizmos()
        {
            if (!_showGrid) return;
            
            if (_grid == null) return;
            GridUseCases.DebugDrawGrid(_grid);
        }
        #endregion

        [ContextMenu("ConstructGrid")]
        public void ConstructGrid()
        {
            _grid = new Grid(transform.position, _gridWidth, _gridHeight, _cellSize, _cellGap);
        }

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
            _grid.SetPosition(position);
        }
    }
}