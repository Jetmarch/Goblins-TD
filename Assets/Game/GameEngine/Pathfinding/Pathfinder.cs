using System.Collections.Generic;
using Game.GameEngine.GridSystem;
using Game.Gameplay.LevelGrid;
using Game.Gameplay.Levels;

namespace Game.GameEngine.Pathfinding
{
    public class Pathfinder : IPathfinder
    {
        private readonly IPathfindingAlgorithm _algorithm;

        public Pathfinder(IPathfindingAlgorithm algorithm)
        {
            _algorithm = algorithm;
        }
        
        //TODO: Path smoothing
        public bool FindPath(ILevelCell startCell, ILevelCell endCell, ILevelGrid grid, out List<ILevelCell> path)
        {
            if (_algorithm == null)
            {
                throw new System.Exception("Pathfinder algorithm not initialized.");
            }
            
            return _algorithm.FindPath(startCell, endCell, grid, out path);
        }
    }
}