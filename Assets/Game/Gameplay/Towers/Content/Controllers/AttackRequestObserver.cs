using System;
using Game.Gameplay.Weapons;
using VContainer.Unity;

namespace Game.Gameplay.Towers.Controllers
{
    // ReSharper disable once ClassNeverInstantiated.Global
    internal sealed class AttackRequestObserver : IInitializable, IDisposable
    {
        private readonly ITowerPresenter _towerPresenter;
        private readonly IWeaponPresenter _weaponPresenter;
        
        public AttackRequestObserver(ITowerPresenter towerPresenter, IWeaponPresenter weaponPresenter)
        {
            _towerPresenter = towerPresenter;
            _weaponPresenter = weaponPresenter;
        }
        
        public void Initialize()
        {
            _towerPresenter.AttackRequest += _weaponPresenter.Attack;
        }

        public void Dispose()
        {
            _towerPresenter.AttackRequest -= _weaponPresenter.Attack;
        }
    }
}