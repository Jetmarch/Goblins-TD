using System;
using Game.Gameplay.Weapons;
using VContainer.Unity;

namespace Game.Gameplay.Buildings.Controllers
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public sealed class AttackRequestObserver : IInitializable, IDisposable
    {
        private readonly TowerView _towerView;
        private readonly WeaponView _weaponView;
        
        public AttackRequestObserver(TowerView towerView, WeaponView weaponView)
        {
            _towerView = towerView;
            _weaponView = weaponView;
        }
        
        public void Initialize()
        {
            _towerView.AttackRequest += _weaponView.Attack;
        }

        public void Dispose()
        {
            _towerView.AttackRequest -= _weaponView.Attack;
        }
    }
}