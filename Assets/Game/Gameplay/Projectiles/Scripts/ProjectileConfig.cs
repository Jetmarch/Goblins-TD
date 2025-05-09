using UnityEngine;

namespace Game.Gameplay.Projectiles
{
    [CreateAssetMenu(fileName = "ProjectileConfig", menuName = "Gameplay/Projectiles/ProjectileConfig")]
    public class ProjectileConfig : ScriptableObject
    {
        [SerializeField] private ProjectileData _projectileData;

        public ProjectileData GetPrototype()
        {
            return new ProjectileData(_projectileData);
        }
    }
}