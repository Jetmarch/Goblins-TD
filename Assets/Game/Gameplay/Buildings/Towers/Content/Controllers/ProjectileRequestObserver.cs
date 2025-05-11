using System;
using Game.Gameplay.Impacts;
using Game.Gameplay.Projectiles;
using Game.Gameplay.Weapons;
using VContainer.Unity;

namespace Game.Gameplay.Buildings.Controllers
{
    internal sealed class ProjectileRequestObserver : IInitializable, IDisposable
    {
        private readonly IProjectileRepository _projectileRepository;
        private readonly IWeaponPresenter _weaponPresenter;

        public ProjectileRequestObserver(IProjectileRepository projectileRepository, IWeaponPresenter weaponPresenter)
        {
            _projectileRepository = projectileRepository;
            _weaponPresenter = weaponPresenter;
        }

        public void Initialize()
        {
            _weaponPresenter.ProjectileRequest += _projectileRepository.CreateProjectile;
        }

        public void Dispose()
        {
            _weaponPresenter.ProjectileRequest -= _projectileRepository.CreateProjectile;
        }
    }
}