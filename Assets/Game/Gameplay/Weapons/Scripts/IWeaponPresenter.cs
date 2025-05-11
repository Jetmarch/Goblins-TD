using System;
using Game.Gameplay.Impacts;

namespace Game.Gameplay.Weapons
{
    public interface IWeaponPresenter
    {
        event Action<SpawnProjectileData, ImpactHitData> ProjectileRequest;
        void Attack();
    }
}