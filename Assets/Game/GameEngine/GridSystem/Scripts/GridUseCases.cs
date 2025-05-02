using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Game.GameEngine.GridSystem
{
    public static class GridUseCases
    {
        public static Vector2 GetCenterOfCells(List<Cell> cells, Vector2 gridPosition = default)
        {
            var centerX = cells.Average(cell => cell.XPos * cell.Size + gridPosition.x);
            var centerY = cells.Average(cell => cell.YPos * cell.Size + gridPosition.y);
            
            return new Vector2(centerX, centerY);
        }

        public static void GetPossibleTargetCellsForBuilding(Grid buildingGrid, Grid placementGrid, List<Cell> possibleTargetCells)
        {
            possibleTargetCells.Clear();
            
            foreach (var cell in buildingGrid.Cells)
            {
                var cellWorldPositionX = cell.XPos * cell.Size + buildingGrid.Position.x ;
                var cellWorldPositionY = cell.YPos * cell.Size + buildingGrid.Position.y ;
                var cellWorldPosition = new Vector2(cellWorldPositionX, cellWorldPositionY);
                
                var globalGridCell = placementGrid.GetCellByWorldPosition(cellWorldPosition);
                if (globalGridCell != null && !globalGridCell.IsBusy)
                {
                    possibleTargetCells.Add(globalGridCell);
                }
            }
        }

        public static bool CanBuild(Grid buildingGrid, Grid placementGrid)
        {
            int countOfPossibleTargetCells = 0;
            foreach (var cell in buildingGrid.Cells)
            {
                var cellWorldPositionX = cell.XPos * cell.Size + buildingGrid.Position.x ;
                var cellWorldPositionY = cell.YPos * cell.Size + buildingGrid.Position.y ;
                var cellWorldPosition = new Vector2(cellWorldPositionX, cellWorldPositionY);
                
                var globalGridCell = placementGrid.GetCellByWorldPosition(cellWorldPosition);
                if (globalGridCell != null && !globalGridCell.IsBusy)
                {
                    countOfPossibleTargetCells++;
                }
            }

            return countOfPossibleTargetCells >= buildingGrid.Cells.Length;
        }
        
#if UNITY_EDITOR
        public static void DebugDrawGrid(Grid grid)
        {
            Gizmos.color = Color.green;
            
            for (int x = 0; x < grid.Width; x++)
            {
                for (int y = 0; y < grid.Height; y++)
                {
                    var currentCell = grid.Cells[x, y];
                    var cellSize = currentCell.Size;
                    var halfCellSize = cellSize * 0.5f;
                    var xPos = (currentCell.XPos * currentCell.Size) + grid.Position.x;
                    var yPos = (currentCell.YPos * currentCell.Size) + grid.Position.y;
                    // xPos -= _localGrid.Width * halfCellSize;
                    // yPos -= _localGrid.Height * halfCellSize;
                    
                    var position = new Vector3(xPos, yPos, 0);
                    var size = new Vector3(cellSize, cellSize, 0);

                    if (currentCell.IsBusy)
                    {
                        Gizmos.DrawCube(position, size);
                    }
                    else
                    {
                        Gizmos.DrawWireCube(position, size);
                    }
                }
            }
        }
#endif
    }
}