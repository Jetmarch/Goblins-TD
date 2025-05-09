using UnityEngine;


namespace Game.Gameplay.Projectiles
{
    public class ProjectileFactory : MonoBehaviour
    {
        [SerializeField] private ProjectileView _sampleProjectilePrefab;
        
        public void CreateProjectile(string projectileName, Vector3 position, Quaternion rotation)
        {
            //TODO: use projectileName
            var projectile = Instantiate(_sampleProjectilePrefab, position, rotation);
        }
    }
}