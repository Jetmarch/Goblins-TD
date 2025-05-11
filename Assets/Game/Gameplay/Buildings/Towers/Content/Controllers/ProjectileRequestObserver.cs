using System;
using Game.Gameplay.Projectiles;
using Game.Gameplay.Weapons;
using VContainer.Unity;

namespace Game.Gameplay.Buildings.Controllers
{
    internal sealed class ProjectileRequestObserver : IInitializable, IDisposable
    {
        private readonly ProjectileFactory _projectileFactory;
        private readonly IWeaponPresenter _weaponPresenter;

        public ProjectileRequestObserver(ProjectileFactory projectileFactory, IWeaponPresenter weaponPresenter)
        {
            _projectileFactory = projectileFactory;
            _weaponPresenter = weaponPresenter;
        }

        public void Initialize()
        {
            _weaponPresenter.ProjectileRequest += _projectileFactory.CreateProjectile;
        }

        public void Dispose()
        {
            _weaponPresenter.ProjectileRequest -= _projectileFactory.CreateProjectile;
        }
    }
}