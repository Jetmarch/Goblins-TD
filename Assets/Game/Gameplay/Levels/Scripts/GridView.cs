using UnityEngine;

namespace Game.Gameplay.Levels
{
    public class GridView : MonoBehaviour
    {
        [SerializeField] private GameObject _gridCellPrefab;
        [SerializeField] private Transform _gridCellParent;
        [SerializeField] private GridManager _gridManager;
        private CellView[,] _gridCells;
        
        private void Start()
        {
            ConstructGrid();
            Hide();
        }

        private void ConstructGrid()
        {
            _gridCells = new CellView[_gridManager.Grid.Width, _gridManager.Grid.Height];
            
            var gridXPos = _gridManager.Grid.WorldPosition.x;
            var gridYPos = _gridManager.Grid.WorldPosition.y;
            for (int x = 0; x < _gridManager.Grid.Width; x++)
            {
                for (int y = 0; y < _gridManager.Grid.Height; y++)
                {
                    var cell = _gridManager.Grid.GetCell(x, y);
                    var cellPosition = new Vector3(cell.WorldX, cell.WorldY, 0f);
                    var gridCell = Instantiate(_gridCellPrefab, cellPosition, _gridCellPrefab.transform.rotation, _gridCellParent).GetComponent<CellView>();
                    gridCell.transform.localScale = new Vector3(cell.Size, cell.Size, 1f);
                    _gridCells[x,y] = gridCell;
                }
            }
        }

        [ContextMenu("Show")]
        public void Show()
        {
            foreach (var cell in _gridCells)
            {
                cell.Show();

            }
        }
    
        [ContextMenu("Hide")]
        public void Hide()
        {
            foreach (var cell in _gridCells)
            {
                cell.Hide();
            }
        }

        public void HighlightCell(int x, int y)
        {
            var cell = GetCell(x, y);
            if (cell)
            {
                cell.Highlight();
            }
        }

        public void UnhighlightAllCells()
        {
            foreach (var cell in _gridCells)
            {
                cell.Unhighlight();
            }
        }

        public void SetCellBusy(int x, int y, bool isBusy)
        {
            var cellView = GetCell(x, y);
            cellView?.SetBusy(isBusy);
            var cell = _gridManager.Grid.GetCell(x, y);
            cell?.SetBusy(isBusy);
        }

        private CellView GetCell(int x, int y)
        {
            if (x < 0 || x >= _gridManager.Grid.Width || y < 0 || y >= _gridManager.Grid.Height) return default; 
            return _gridCells[x, y];
        }
    }
}