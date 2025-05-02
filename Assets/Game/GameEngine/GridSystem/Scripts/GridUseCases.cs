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

        public static void GetPossibleTargetCellsForBuilding(Grid buildingGrid, Grid placementGrid,
            List<Cell> possibleTargetCells)
        {
            possibleTargetCells.Clear();

            var firstBuildingCell = buildingGrid.GetCell(0, 0);
            var firstBuildingCellPosition = GetCellWorldPosition(firstBuildingCell, buildingGrid.Position);

            var firstPlacementCell = placementGrid.GetCellByWorldPosition(firstBuildingCellPosition);

            if (firstPlacementCell == null) return;

            for (int x = 0; x < buildingGrid.Width; x++)
            {
                for (int y = 0; y < buildingGrid.Height; y++)
                {
                    var possiblePlacementCell =
                        placementGrid.GetCell(x + firstPlacementCell.GridPosX, y + firstPlacementCell.GridPosY);
                    if (possiblePlacementCell != null)
                    {
                        possibleTargetCells.Add(possiblePlacementCell);
                    }
                }
            }
        }

        private static Vector2 GetCellWorldPosition(Cell cell, Vector2 gridPosition = default)
        {
            var cellWorldPositionX = cell.GridPosX * cell.Size + gridPosition.x;
            var cellWorldPositionY = cell.GridPosY * cell.Size + gridPosition.y;
            return new Vector2(cellWorldPositionX, cellWorldPositionY);
        }

        public static bool CanBuild(List<Cell> possibleTargetCells, Grid buildingGrid)
        {
            possibleTargetCells.RemoveAll(cell => cell.IsBusy);
            return possibleTargetCells.Count == buildingGrid.Cells.Length;
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