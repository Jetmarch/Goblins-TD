using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Game.Gameplay.LevelGrid
{
    public static class LevelGridUseCases
    {
        public static Vector2 GetCenterOfMultipleCells(List<ILevelCell> cells)
        {
            var centerX = cells.Average(cell => cell.WorldX);
            var centerY = cells.Average(cell => cell.WorldY);
            
            return new Vector2(centerX, centerY);
        }

        public static Vector2 GetCenterOfCell(ILevelCell cell)
        {
            return new Vector2(cell.WorldX, cell.WorldY);
        }

        public static void GetPossibleTargetCellsForBuilding(ILevelGrid buildingGrid, ILevelGrid placementGrid,
            List<ILevelCell> possibleTargetCells)
        {
            possibleTargetCells.Clear();

            var firstBuildingCell = buildingGrid.GetCell(0, 0);
            if (firstBuildingCell == default)
            {
                throw new ApplicationException("Building does not have any cells");
            }
            var firstBuildingCellPosition = GetCellWorldPosition(firstBuildingCell, buildingGrid.WorldPosition);

            var firstPlacementCell = GetCellByWorldPositionOrDefault(placementGrid, firstBuildingCellPosition);

            if (firstPlacementCell == null) return;

            for (int x = 0; x < buildingGrid.Width; x++)
            {
                for (int y = 0; y < buildingGrid.Height; y++)
                {
                    var possiblePlacementCell =
                        placementGrid.GetCell(x + firstPlacementCell.GridX, y + firstPlacementCell.GridY);
                    if (possiblePlacementCell != null)
                    {
                        possibleTargetCells.Add(possiblePlacementCell);
                    }
                }
            }
        }

        private static Vector2 GetCellWorldPosition(ILevelCell cell, Vector2 gridPosition = default)
        {
            var cellWorldPositionX = cell.GridX * cell.Size + gridPosition.x;
            var cellWorldPositionY = cell.GridY * cell.Size + gridPosition.y;
            return new Vector2(cellWorldPositionX, cellWorldPositionY);
        }

        public static bool CanBuild(List<ILevelCell> possibleTargetCells, ILevelGrid buildingGrid)
        {
            possibleTargetCells.RemoveAll(cell => !cell.IsWalkable);
            var countOfBuildingCells = buildingGrid.Width * buildingGrid.Height;
            return possibleTargetCells.Count == countOfBuildingCells;
        }

        public static bool CanBuild(ILevelCell possibleCellForBuilding)
        {
            return !possibleCellForBuilding.IsBusy && !possibleCellForBuilding.IsWalkable;
        }
        
        public static ILevelCell GetCellByWorldPositionOrDefault(ILevelGrid grid, Vector2 worldPosition)
        {
            GetGridPosition(grid, worldPosition, out var gridX, out var gridY);
            
            return grid.GetCell(gridX, gridY);
        }
        
        private static void GetGridPosition(ILevelGrid grid, Vector2 worldPosition, out int gridX, out int gridY)
        {
            gridX = Mathf.FloorToInt((worldPosition.x - grid.WorldPosition.x - grid.CellGap.x) / grid.CellSize);
            gridY = Mathf.FloorToInt((worldPosition.y - grid.WorldPosition.y - grid.CellGap.y) / grid.CellSize);
        }
        
#if UNITY_EDITOR
        public static void DebugDrawGrid(ILevelGrid grid)
        {
            for (int x = 0; x < grid.Width; x++)
            {
                for (int y = 0; y < grid.Height; y++)
                {
                    
                    var currentCell = grid.GetCell(x, y);
                    var cellSize = currentCell.Size;
                    var xPos = currentCell.WorldX;
                    var yPos = currentCell.WorldY;
                    
                    var position = new Vector3(xPos, yPos, 0);
                    var size = new Vector3(cellSize, cellSize, 0);

                    if (currentCell.Type == LevelCellType.Buildable)
                    {
                        Gizmos.color = Color.green;
                        Gizmos.DrawWireCube(position, size);
                    }
                    else if (currentCell.Type == LevelCellType.Walkable)
                    {
                        Gizmos.color = Color.red;
                        Gizmos.DrawCube(position, size);
                    }
                }
            }
        }
#endif
        
    }
}