using System.Collections.Generic;
using Game.GameEngine.GridSystem;

namespace Game.GameEngine.Pathfinding
{
    public interface IPathfindingAlgorithm
    {
        bool FindPath(ICell startCell, ICell endCell, IGrid<ICell> grid, out List<ICell> path);
    }
}