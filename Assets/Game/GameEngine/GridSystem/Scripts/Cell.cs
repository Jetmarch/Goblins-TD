using System;
using Game.GameEngine.Pathfinding;

namespace Game.GameEngine.GridSystem
{
    [Serializable]
    public class Cell : ICell
    {
        public int GridX => GridPosX;
        public int GridY => GridPosY;
        public bool IsWalkable => !IsBusy;
        
        public float Size => _size;
        public float WorldX => _worldX;
        public float WorldY => _worldY;
        public int GridPosX => _gridPosX;
        public int GridPosY => _gridPosY;
        public bool IsBusy => _isBusy;
        
        private float _size;
        private float _worldX;
        private float _worldY;
        private int _gridPosX;
        private int _gridPosY;

        private bool _isBusy;

        public Cell(int gridPosX, int gridPosY, float worldX, float worldY, float size)
        {
            _gridPosX = gridPosX;
            _gridPosY = gridPosY;
            _worldX = worldX;
            _worldY = worldY;
            _size = size;
        }

        public void SetBusy(bool isBusy)
        {
            _isBusy = isBusy;
        }

        
    }
}