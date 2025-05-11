using UnityEngine;

namespace Game.Gameplay.Buildings
{
    public sealed class ConeDetector
    {
        public static bool IsTargetInCone(Vector3 target, Vector3 objectPosition, Vector3 objectUp, float detectionAngle)
        {
            var direction = (target - objectPosition).normalized;
            var angle = Vector2.Angle(objectUp, direction);

            return angle <= detectionAngle;
        }
    }
}