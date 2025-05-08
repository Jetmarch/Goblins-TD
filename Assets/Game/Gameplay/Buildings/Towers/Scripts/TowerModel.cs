using System;
using UnityEngine;

namespace Game.Gameplay.Buildings
{
    [Serializable]
    public class TowerModel
    {
        public float Damage => _damage;
        public float AttackSpeed => _attackSpeed;
        public float Range => _range;
        public float RotateSpeed => _rotateSpeed;
        public float CritChance => _critChance;
        
        [SerializeField] private float _damage;
        [SerializeField] private float _attackSpeed;
        [SerializeField] private float _range;
        [SerializeField] private float _rotateSpeed;
        [SerializeField] private float _critChance;
        
        public TowerModel(TowerModel root)
        {
            _damage = root.Damage;
            _attackSpeed = root.AttackSpeed;
            _range = root.Range;
            _rotateSpeed = root.RotateSpeed;
            _critChance = root.CritChance;
        }
    }
}