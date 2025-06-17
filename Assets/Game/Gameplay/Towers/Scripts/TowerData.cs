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
        public int AmountOfBulletsPerShot => _amountOfBulletsPerShot;
        public string ProjectileId => _projectileId;
        
        [SerializeField] private float _damage;
        [SerializeField] private float _attackSpeed;
        [SerializeField] private float _attackRadius;
        [SerializeField] private float _rotateSpeed;
        [SerializeField] private float _critChance;
        [SerializeField] private float _attackAngle;
        [SerializeField] private int _amountOfBulletsPerShot;
        [SerializeField] private string _projectileId;

        public TowerData(TowerData root)
        {
            _damage = root._damage;
            _attackSpeed = root._attackSpeed;
            _attackRadius = root._attackRadius;
            _rotateSpeed = root._rotateSpeed;
            _critChance = root._critChance;
            _attackAngle = root._attackAngle;
            _amountOfBulletsPerShot = root._amountOfBulletsPerShot;
            _projectileId = root._projectileId;
        }
    }
}