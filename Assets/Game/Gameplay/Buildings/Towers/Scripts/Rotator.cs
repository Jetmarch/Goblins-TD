using UnityEngine;

namespace Game.Gameplay.Buildings
{
    public sealed class Rotator
    {
        public static void RotateTowardsTarget(Vector3 target, Transform rotatingTransform, float rotationSpeed, float deltaTime)
        {
            var direction = target - rotatingTransform.transform.position;
            var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
            var targetRotation = Quaternion.Euler(0, 0, angle);
            
            rotatingTransform.transform.rotation = Quaternion.Slerp(rotatingTransform.transform.rotation, targetRotation, rotationSpeed * deltaTime);
        }

        
    }
}