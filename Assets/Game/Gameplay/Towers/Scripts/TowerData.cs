using System;
using UnityEngine;

namespace Game.Gameplay.Towers
{
    [Serializable]
    public class TowerData
    {
        public float Damage => _damage;
        public float AttackSpeed => _attackSpeed;
        public float AttackRadius => _attackRadius;
        public float RotateSpeed => _rotateSpeed;
        public float CritChance => _critChance;
        public float AttackAngle => _attackAngle;
        public string ProjectileId => _projectileId;
        
        [SerializeField] private float _damage;
        [SerializeField] private float _attackSpeed;
        [SerializeField] private float _attackRadius;
        [SerializeField] private float _rotateSpeed;
        [SerializeField] private float _critChance;
        [SerializeField] private float _attackAngle;
        [SerializeField] private string _projectileId;

        public TowerData(TowerData root)
        {
            _damage = root.Damage;
            _attackSpeed = root.AttackSpeed;
            _attackRadius = root.AttackRadius;
            _rotateSpeed = root.RotateSpeed;
            _critChance = root.CritChance;
            _attackAngle = root.AttackAngle;
            _projectileId = root.ProjectileId;
        }
    }
}