using System;
using UnityEngine;

namespace Game.Gameplay.Levels
{
    [Serializable]
    public class LevelCell : ILevelCell
    {
        public int GridX => _gridX;
        public int GridY => _gridY;
        public float WorldX => _worldX;
        public float WorldY => _worldY;
        public float Size => _size;
        public bool IsWalkable => !IsBusy;
        public bool IsBusy => _isBusy;
        public LevelCellType Type => _type;

        [SerializeField] private int _gridX;
        [SerializeField] private int _gridY;
        [SerializeField] private float _worldX;
        [SerializeField] private float _worldY;
        [SerializeField] private float _size;
        [SerializeField] private bool _isBusy;
        [SerializeField] private LevelCellType _type;
        
        public LevelCell(int gridX, int gridY, float size, LevelCellType type)
        {
            _gridX = gridX;
            _gridY = gridY;
            _size = size;
            _type = type;
        }

        public LevelCell(LevelCell cell)
        {
            _gridX = cell.GridX;
            _gridY = cell.GridY;
            _size = cell.Size;
            _type = cell.Type;
        }
        
        public void SetType(LevelCellType type)
        {
            _type = type;
        }

        public void SetBusy(bool isBusy)
        {
            _isBusy = isBusy;
        }
    }
}