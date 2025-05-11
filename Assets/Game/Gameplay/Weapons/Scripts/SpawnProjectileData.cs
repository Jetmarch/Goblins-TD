using UnityEngine;

namespace Game.Gameplay.Weapons
{
    public struct SpawnProjectileData
    {
        public readonly string ProjectileId;
        public readonly Vector3 Position;
        public readonly Quaternion Rotation;

        public SpawnProjectileData(string projectileId, Vector3 position, Quaternion rotation)
        {
            ProjectileId = projectileId;
            Position = position;
            Rotation = rotation;
        }
    }
}