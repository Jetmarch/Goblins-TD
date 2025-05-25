using System.Collections.Generic;
using Game.Gameplay.LevelGrid;

namespace Game.GameEngine.Pathfinding
{
    public interface IPathfindingAlgorithm
    {
        bool FindPath(ILevelCell startCell, ILevelCell endCell, ILevelGrid grid, out List<ILevelCell> path);
    }
}