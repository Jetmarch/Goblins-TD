using System;
using UnityEngine;

namespace Game.Gameplay.Buildings
{
    [RequireComponent(typeof(CircleCollider2D))]
    public class EnemySensor : MonoBehaviour
    {
        public event Action<GameObject> OnEnemyDetected;
        public event Action<GameObject> OnEnemyLost;
        
        [SerializeField] private CircleCollider2D _collider;
        private void OnTriggerEnter2D(Collider2D other)
        {
            OnEnemyDetected?.Invoke(other.gameObject);
            
            Debug.Log($"Enemy detected: {other.gameObject}");
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            OnEnemyLost?.Invoke(other.gameObject);
            
            Debug.Log($"Enemy lost: {other.gameObject}");
        }

        public void SetRadius(float radius)
        {
            _collider.radius = radius;
        }
    }
}