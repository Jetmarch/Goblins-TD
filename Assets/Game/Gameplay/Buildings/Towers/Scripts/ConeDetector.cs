using UnityEngine;

namespace Game.Gameplay.Buildings
{
    public sealed class ConeDetector
    {
        public static bool IsTargetInCone(Vector3 target, Transform rotatingTransform, float detectionAngle)
        {
            var direction = (target - rotatingTransform.transform.position).normalized;
            var angle = Vector2.Angle(rotatingTransform.transform.up, direction);

            return angle <= detectionAngle;
        }
    }
}