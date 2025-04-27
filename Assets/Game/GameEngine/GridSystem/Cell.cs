using System;

namespace Game.GameEngine.GridSystem
{
    [Serializable]
    public class Cell
    {
        public float Size => _size;
        public float XPos => _xPos;
        public float YPos => _yPos;
        public bool IsBusy => _isBusy;
        
        private float _size;
        private float _xPos;
        private float _yPos;

        private bool _isBusy;

        public Cell(float xPos, float yPos, float size)
        {
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