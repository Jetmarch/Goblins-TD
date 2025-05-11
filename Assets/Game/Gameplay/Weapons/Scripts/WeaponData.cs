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

        [SerializeField] private float _attackSpeed;
        [SerializeField] private string _projectileId;
        [SerializeField] private float _damage;
        [SerializeField] private float _knockback;

        public WeaponData(WeaponData root)
        {
            _projectileId = root.ProjectileId;
            _attackSpeed = root.AttackSpeed;
            _damage = root.Damage;
            _knockback = root.Knockback;
        }
    }
}