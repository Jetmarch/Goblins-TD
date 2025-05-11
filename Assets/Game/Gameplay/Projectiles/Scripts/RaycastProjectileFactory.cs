using Game.Gameplay.Impacts;
using Game.Gameplay.Weapons;
using UnityEngine;


namespace Game.Gameplay.Projectiles
{
    internal sealed class RaycastProjectileFactory : MonoBehaviour, IProjectileFactory
    {
        
        [SerializeField] private float _defaultRayDistance = 100f;
        [SerializeField] private LayerMask _raycastLayerMask;

        public IProjectileView GetOrCreateProjectile(SpawnProjectileData projectileData, ImpactHitData impactHitData)
        {
            var direction =  projectileData.Rotation * Vector2.up;
            var hit = Physics2D.Raycast(projectileData.Position, direction, _defaultRayDistance, _raycastLayerMask);
            if (!hit.collider) return default;
            
            Debug.Log($"Hitted some object with name {hit.collider.gameObject.name}");
            if (!hit.collider.gameObject.TryGetComponent(out IHittable hittable)) return default;
            
            hittable.Impact(impactHitData);

            return default;
        }
    }
}