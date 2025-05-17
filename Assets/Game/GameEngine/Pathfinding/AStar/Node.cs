using Game.Gameplay.Levels;

namespace Game.GameEngine.Pathfinding
{
    public sealed class Node
    {
        public ILevelCell Cell => _cell;
        public int X => _cell.GridX;
        public int Y => _cell.GridY;

        public Node Parent
        {
            get => _parent;
            set => _parent = value;
        }
        
        /// <summary>
        /// Distance to start point
        /// </summary>
        public int GCost
        {
            get => _gCost;
            set => _gCost = value;
        }

        /// <summary>
        /// Approximately distance to end point
        /// </summary>
        public int HCost
        {
            get => _hCost;
            set => _hCost = value;
        }
        
        /// <summary>
        /// GCost + HCost
        /// </summary>
        public int FCost => _gCost + _hCost;
        
        private readonly ILevelCell _cell;
        private Node _parent;
        private int _gCost;
        private int _hCost;
        public Node(ILevelCell cell)
        {
            _cell = cell;
            _gCost = 0;
            _hCost = 0;
            _parent = null;
        }

        public override bool Equals(object obj)
        {
            if (obj is Node node)
            {
                return node.X == X && node.Y == Y;
            }
            return false;
        }
    }
}