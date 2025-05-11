using Game.Gameplay.Impacts;
using Game.Gameplay.Weapons;

namespace Game.Gameplay.Projectiles
{
    public interface IProjectileFactory
    {
        IProjectileView GetOrCreateProjectile(SpawnProjectileData data, ImpactHitData impactHitData);
    }
}