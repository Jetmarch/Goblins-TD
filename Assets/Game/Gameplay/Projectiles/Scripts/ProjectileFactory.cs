using Game.Gameplay.Weapons;
using UnityEngine;


namespace Game.Gameplay.Projectiles
{
    public class ProjectileFactory : MonoBehaviour
    {
        [SerializeField] private Transform _parent;
        
        [SerializeField] private ProjectileView _sampleProjectilePrefab;

        public void CreateProjectile(SpawnProjectileData projectileData)
        {
            var projectileId = projectileData.ProjectileId;
            var projectile = Instantiate(_sampleProjectilePrefab, projectileData.Position, projectileData.Rotation, _parent);
        }
    }
}