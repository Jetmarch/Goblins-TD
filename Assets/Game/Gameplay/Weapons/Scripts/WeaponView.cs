using System;
using UnityEngine;
using VContainer;

namespace Game.Gameplay.Weapons
{
    public class WeaponView : MonoBehaviour
    {
        [SerializeField] private Animator _weaponAnimator;
        [SerializeField] private string _attackAnimationName = "Attack";

        [SerializeField] private Projectiles.ProjectileFactory _projectileFactory;
        [SerializeField] private Transform _shootPoint;
        
        private int _attackTrigger;
        private float _currentAttackDelay = 0f;
        
        private WeaponData _weaponData;

        [Inject]
        public void Initialize(WeaponData weaponData)
        {
            _weaponData = weaponData;

            _attackTrigger = Animator.StringToHash(_attackAnimationName);
        }
        
        public void Attack()
        {
            if (_currentAttackDelay > 0f)
            {
                return;
            }
            _weaponAnimator.SetTrigger(_attackTrigger);
                
            _projectileFactory.CreateProjectile(_weaponData.ProjectileId, _shootPoint.position, transform.rotation);

            _currentAttackDelay = 1 / _weaponData.AttackSpeed;
        }

        public void Update()
        {
            _currentAttackDelay -= Time.deltaTime;
        }
    }

    public class WeaponData
    {
        public readonly string ProjectileId;
        public readonly float AttackSpeed;

        public WeaponData(string projectileId, float attackSpeed)
        {
            ProjectileId = projectileId;
            AttackSpeed = attackSpeed;
        }
    }
}