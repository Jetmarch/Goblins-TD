using UnityEngine;

namespace Game.Gameplay.Buildings
{
    public static class Rotator
    {
        public static Quaternion SmoothRotateTowardsTarget(Vector3 targetPosition, Vector3 objectPosition, Quaternion objectRotation, float rotationSpeed, float deltaTime)
        {
            var direction = targetPosition - objectPosition;
            var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
            var targetRotation = Quaternion.Euler(0, 0, angle);
            
            return Quaternion.Slerp(objectRotation, targetRotation, rotationSpeed * deltaTime);
        }
    }
}