using System;
using UnityEngine;

namespace Game.Gameplay.Weapons
{
    [Serializable]
    public sealed class WeaponData
    {
        public string ProjectileId => _projectileId;
        public float AttackSpeed => _attackSpeed;
        public float Damage => _damage;
        public float Knockback => _knockback;
        public int AmountOfBulletsPerShot => _amountOfBulletsPerShot;
        public int MaxAmountOfBullets => _maxAmountOfBullets;
        public int MinAmountOfBullets => _minAmountOfBullets;

        [SerializeField] private float _attackSpeed;
        [SerializeField] private string _projectileId;
        [SerializeField] private float _damage;
        [SerializeField] private float _knockback;
        [SerializeField] private int _amountOfBulletsPerShot;
        [SerializeField] private int _maxAmountOfBullets;
        [SerializeField] private int _minAmountOfBullets = 0;

        public WeaponData(WeaponData root)
        {
            _projectileId = root._projectileId;
            _attackSpeed = root._attackSpeed;
            _damage = root._damage;
            _knockback = root._knockback;
            _amountOfBulletsPerShot = root._amountOfBulletsPerShot;
            _maxAmountOfBullets = root._maxAmountOfBullets;
            _minAmountOfBullets = root._minAmountOfBullets;
        }
    }
}