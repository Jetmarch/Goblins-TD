using System;

namespace Game.GameEngine.GridSystem
{
    [Serializable]
    public class Cell
    {
        public float Size => _size;
        public float XPos => _xPos;
        public float YPos => _yPos;
        public int GridPosX => _gridPosX;
        public int GridPosY => _gridPosY;
        public bool IsBusy => _isBusy;
        
        private float _size;
        private float _xPos;
        private float _yPos;
        private int _gridPosX;
        private int _gridPosY;

        private bool _isBusy;

        public Cell(int gridPosX, int gridPosY, float xPos, float yPos, float size)
        {
            _gridPosX = gridPosX;
            _gridPosY = gridPosY;
            _xPos = xPos;
            _yPos = yPos;
            _size = size;
        }

        public void SetBusy(bool isBusy)
        {
            _isBusy = isBusy;
        }
    }
}