using System;
using System.Collections.Generic;
using Game.Gameplay.Impacts;
using Game.Gameplay.Weapons;

namespace Game.Gameplay.Projectiles
{
    internal sealed class ProjectileRepository : IProjectileRepository
    {
        private readonly Dictionary<string, IProjectileFactory> _projectileFactories;

        public ProjectileRepository(Dictionary<string, IProjectileFactory> projectileFactories)
        {
            _projectileFactories = projectileFactories;
        }

        public void CreateProjectile(SpawnProjectileData spawnProjectileData, ImpactHitData impactHitData)
        {
            if (!_projectileFactories.TryGetValue(spawnProjectileData.ProjectileId, out var projectileFactory))
            {
                throw new ApplicationException($"No projectile factory registered for projectile {spawnProjectileData.ProjectileId}");
            }

            projectileFactory.GetOrCreateProjectile(spawnProjectileData, impactHitData);
        }
    }
}