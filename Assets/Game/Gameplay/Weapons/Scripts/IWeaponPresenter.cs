using System;
using Game.Gameplay.Buildings;
using Modules.Core.GameLoop;
using UnityEngine;

namespace Game.Gameplay.Weapons
{
    
    public struct SpawnProjectileData
    {
        public readonly string ProjectileId;
        public readonly Vector3 Position;
        public readonly Quaternion Rotation;

        public SpawnProjectileData(string projectileId, Vector3 position, Quaternion rotation)
        {
            ProjectileId = projectileId;
            Position = position;
            Rotation = rotation;
        }
    }
    
    public interface IWeaponPresenter
    {
        event Action<SpawnProjectileData> ProjectileRequest;
        void Attack();
    }

    internal sealed class WeaponPresenter : IWeaponPresenter, IUpdateListener
    {
        public event Action<SpawnProjectileData> ProjectileRequest;
        
        private readonly IWeaponView _view;
        private readonly WeaponData _weaponData;

        private float _currentAttackDelay;
        private ITarget _currentTarget;
        

        public WeaponPresenter(WeaponData weaponData, IWeaponView view)
        {
            _weaponData = weaponData;
            _view = view;
        }

        public void Attack()
        {
            if (_currentAttackDelay > 0f)
            {
                return;
            }
            
            ProjectileRequest?.Invoke(new SpawnProjectileData(_weaponData.ProjectileId, _view.ShootPointTransform.position, _view.ShootPointTransform.rotation));
            
            _view.PlayAttackAnimation();

            _currentAttackDelay = 1 / _weaponData.AttackSpeed;
        }

        public void OnUpdate(float deltaTime)
        {
            if (_currentAttackDelay > 0f)
            {
                _currentAttackDelay -= deltaTime;
            }
        }
    }
}