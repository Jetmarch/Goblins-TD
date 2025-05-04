using System.Collections.Generic;
using Game.GameEngine.GridSystem;
using UnityEngine;

namespace Game.GameEngine.Pathfinding
{
    public class PathfinderDebug : MonoBehaviour
    {
        private IPathfinder _pathfinder;

        [SerializeField] private GridView _placementGridView;
        [SerializeField] private GridManager _placementGridManager;
        private Camera _camera;
        private ICell _startPoint;
        private ICell _endPoint;

        private GridSystem.Grid _grid;
        private void Start()
        {
            _pathfinder = new Pathfinder(new AStarPathfinding());
            _camera = Camera.main;
            _placementGridView.Show();

            _grid = _placementGridManager.Grid;
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(1))
            {
                var worldPoint = _camera.ScreenToWorldPoint(Input.mousePosition);
                var cell = _grid.GetCellByWorldPositionOrDefault(worldPoint);

                if (cell != default)
                {
                    _placementGridView.HighlightCell(cell.GridX, cell.GridY);
                    
                    _endPoint = cell;
                    Debug.Log("End point set");
                }
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                _placementGridView.Show();
            }

            
            
            if (Input.GetMouseButtonDown(0))
            {
                var worldPoint = _camera.ScreenToWorldPoint(Input.mousePosition);
                var cell = _grid.GetCellByWorldPositionOrDefault(worldPoint);

                if (cell != default)
                {
                    _placementGridView.HighlightCell(cell.GridX, cell.GridY);
                    
                    _startPoint = cell;
                    Debug.Log("Start point set");
                }
            }
            
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (_startPoint == null)
                {
                    Debug.LogWarning("No start point selected");
                    return;
                }

                if (_endPoint == null)
                {
                    Debug.LogWarning("No end point selected");
                    return;
                }
                
                Debug.Log("Start pathfinding");

                if (!_pathfinder.FindPath(_startPoint, _endPoint, _grid, out List<ICell> path))
                {
                    Debug.Log("No path found!");
                    return;
                }
                
                Debug.Log("Path found! Displaying");
                foreach (var cell in path)
                {
                    _placementGridView.HighlightCell(cell.GridX, cell.GridY);
                }
            }

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                _placementGridView.UnhighlightAllCells();
            }

        }
    }
}