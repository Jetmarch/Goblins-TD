using System.Collections.Generic;
using UnityEngine;

namespace Game.GameEngine.GridSystem
{
    public class GridView : MonoBehaviour
    {
        [SerializeField] private GameObject _gridCellPrefab;
        [SerializeField] private Transform _gridCellParent;
        [SerializeField] private GridManager _gridManager;
        [SerializeField] private List<GameObject> _gridCells;
        
        private void Start()
        {
            _gridCells = new List<GameObject>();

            var cellSize = _gridManager.Grid.CellSize;
            var gridXPos = _gridManager.Grid.Position.x;
            var gridYPos = _gridManager.Grid.Position.y;
            for (int x = 0; x < _gridManager.Grid.Width; x++)
            {
                for (int y = 0; y < _gridManager.Grid.Height; y++)
                {
                    var cellPosition = new Vector3(x * cellSize + gridXPos, y * cellSize + gridYPos, 0f);
                    var gridCell = Instantiate(_gridCellPrefab, cellPosition, _gridCellPrefab.transform.rotation, _gridCellParent);
                    gridCell.transform.localScale = new Vector3(cellSize, cellSize, 1f);
                    _gridCells.Add(gridCell);
                }
            }
        }

        [ContextMenu("Show")]
        public void Show()
        {
            foreach (var cell in _gridCells)
            {
                cell.SetActive(true);
                
            }
        }

        public void Hide()
        {
            foreach (var cell in _gridCells)
            {
                cell.SetActive(false);
            }
        }
    }
}