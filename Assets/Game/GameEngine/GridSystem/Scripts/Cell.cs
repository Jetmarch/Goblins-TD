using System;
using Game.GameEngine.Common;

namespace Game.GameEngine.GridSystem
{
    [Prototype]
    [Serializable]
    public class Cell : ICell
    {
        public int GridX => GridPosX;
        public int GridY => GridPosY;
        
        public float Size => _size;
       
        public int GridPosX => _gridPosX;
        public int GridPosY => _gridPosY;
        
        private float _size;
        private int _gridPosX;
        private int _gridPosY;

        public Cell(int gridPosX, int gridPosY, float size)
        {
            _gridPosX = gridPosX;
            _gridPosY = gridPosY;
            _size = size;
        }
    }
}