using Game.Gameplay.Impacts;
using Game.Gameplay.Weapons;

namespace Game.Gameplay.Projectiles
{
    public interface IProjectileRepository
    {
        void CreateProjectile(SpawnProjectileData spawnProjectileData, ImpactHitData impactHitData);
    }
}