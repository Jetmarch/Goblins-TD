using System.Collections.Generic;
using Game.GameEngine.GridSystem;

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
        public bool FindPath(ICell startCell, ICell endCell, IGrid<ICell> grid, out List<ICell> path)
        {
            if (_algorithm == null)
            {
                throw new System.Exception("Pathfinder algorithm not initialized.");
            }
            
            return _algorithm.FindPath(startCell, endCell, grid, out path);
        }
    }
}