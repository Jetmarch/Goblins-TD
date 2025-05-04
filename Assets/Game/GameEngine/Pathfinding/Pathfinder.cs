using System;
using System.Collections.Generic;
using System.Linq;
using Game.GameEngine.Common;
using Game.GameEngine.GridSystem;
using UnityEngine;
using Grid = Game.GameEngine.GridSystem.Grid;

namespace Game.GameEngine.Pathfinding
{
    public interface IPathfinder
    {
        bool FindPath(ICell startCell, ICell endCell, IGrid grid, out List<ICell> path);
    }
    
    public class Pathfinder : MonoBehaviour, IPathfinder
    {
        
        
        public bool FindPath(ICell startCell, ICell endCell, IGrid grid, out List<ICell> path)
        {
            path = new List<ICell>();
            var startNode = new Node(startCell);
            var endNode = new Node(endCell);
            
            var openList = new PriorityQueue<Node>();
            openList.Enqueue(startNode, startNode.FCost);

            var closedList = new List<Node>();
            while (openList.Count > 0)
            {
                var currentNode = openList.Dequeue();

                if (Equals(currentNode, endNode))
                {
                    while (currentNode != null)
                    {
                        path.Add(currentNode.Cell);
                        currentNode = currentNode.Parent;
                    }
                    
                    return true;
                }
                
                closedList.Add(currentNode);
                
                var neighbors = new List<Node>();

                for (int x = -1; x <= 1; x++)
                {
                    for (int y = -1; y <= 1; y++)
                    {
                        //Skip center
                        if(x == 0 && y == 0) continue;
                        //Skip diagonal paths
                        if(Mathf.Abs(x) == Mathf.Abs(y)) continue;
                        
                        var neighborX = currentNode.X + x;
                        var neighborY = currentNode.Y + y;
                        var neighbor = grid.GetCell(neighborX, neighborY);
                        if (neighbor == null) continue;
                        
                        if(!neighbor.IsWalkable) continue;
                        
                        var neighborNode = new Node(neighbor);
                        neighbors.Add(neighborNode);
                    }
                }

                foreach (var neighbor in neighbors)
                {
                    if(closedList.Any(node => node.X == neighbor.X && node.Y == neighbor.Y)) continue;

                    var newGCost = currentNode.GCost++;

                    if (openList.TryGetElement(neighbor, out var passedNode))
                    {
                        if (newGCost >= passedNode.GCost) continue;
                        passedNode.GCost = newGCost;
                        passedNode.HCost = Mathf.FloorToInt(Mathf.Sqrt(Mathf.Pow(endNode.X - passedNode.X, 2) + Mathf.Pow(endNode.Y - passedNode.Y, 2)));
                        passedNode.Parent = currentNode;
                        
                        openList.UpdatePriority(passedNode, passedNode.FCost);
                    }
                    else
                    {
                        neighbor.GCost = newGCost;
                        neighbor.HCost = Mathf.FloorToInt(Mathf.Sqrt(Mathf.Pow(endNode.X - neighbor.X, 2) + Mathf.Pow(endNode.Y - neighbor.Y, 2)));
                        neighbor.Parent = currentNode;
                        
                        openList.Enqueue(neighbor, neighbor.FCost);
                    }
                }
            }
            
            return false;
        }

        [SerializeField] private GridView _placementGridView;
        [SerializeField] private GridManager _placementgridManager;
        private Camera _camera;
        private ICell _startPoint;
        private ICell _endPoint;

        private Grid _grid;
        private void Start()
        {
            _camera = Camera.main;
            _placementGridView.Show();

            _grid = _placementgridManager.Grid;
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

                if (!FindPath(_startPoint, _endPoint, _grid, out List<ICell> path))
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