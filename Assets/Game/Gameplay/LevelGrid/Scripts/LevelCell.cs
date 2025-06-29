using System;
using UnityEngine;

namespace Game.Gameplay.LevelGrid
{
    [Serializable]
    public class LevelCell : ILevelCell
    {
        public int GridX => _gridX;
        public int GridY => _gridY;
        public float WorldX => _gridX * _size + _worldPosition.x;
        public float WorldY => _gridY * _size + _worldPosition.y;
        public float Size => _size;
        public bool IsWalkable => _type == LevelCellType.Walkable;
        public bool IsBusy => _occupier != null;
        public LevelCellType Type => _type;
        public GameObject Occupier => _occupier;

        [SerializeField] private int _gridX;
        [SerializeField] private int _gridY;
        [SerializeField] private Vector2 _worldPosition;
        [SerializeField] private float _size;
        [SerializeField] private LevelCellType _type;
        [SerializeField] private GameObject _occupier;
        
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

        public void SetOccupier(GameObject occupier)
        {
            _occupier = occupier;
        }

        public void SetWorldPosition(Vector2 worldPosition)
        {
            _worldPosition = worldPosition;
        }
    }
}