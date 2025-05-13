using Game.Gameplay.Impacts;
using Game.Gameplay.Weapons;
using UnityEngine;


namespace Game.Gameplay.Projectiles
{
    internal sealed class RaycastProjectileFactory : IProjectileFactory
    {
        private readonly float _defaultRayDistance = 100f;
        private readonly LayerMask _raycastLayerMask;

        public RaycastProjectileFactory(LayerMask raycastLayerMask)
        {
            _raycastLayerMask = raycastLayerMask;
        }

        public IProjectileView GetOrCreateProjectile(SpawnProjectileData projectileData, ImpactHitData impactHitData)
        {
            var direction =  projectileData.Rotation * Vector2.up;
            var hit = Physics2D.Raycast(projectileData.Position, direction, _defaultRayDistance, _raycastLayerMask);
            if (!hit.collider) return default;
            
            
            if (!hit.collider.gameObject.TryGetComponent(out IHittable hittable)) return default;
            
            hittable.Impact(impactHitData);
            Debug.Log($"Hit some object with name {hit.collider.gameObject.name}");
            
            return default;
        }
    }
}