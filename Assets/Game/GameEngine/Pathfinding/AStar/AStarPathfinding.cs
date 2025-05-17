using System.Collections.Generic;
using System.Linq;
using Game.GameEngine.Common;
using Game.GameEngine.GridSystem;
using Game.Gameplay.Levels;
using UnityEngine;

namespace Game.GameEngine.Pathfinding
{
    public sealed class AStarPathfinding : IPathfindingAlgorithm
    {
        public bool FindPath(ILevelCell startCell, ILevelCell endCell, ILevelGrid grid, out List<ILevelCell> path)
        {
            path = new List<ILevelCell>();
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
    }
}