using System;
using UnityEngine;
using VContainer;

namespace Game.Gameplay.Buildings
{
    [RequireComponent(typeof(CircleCollider2D))]
    public class EnemySensor : MonoBehaviour
    {
        public event Action<GameObject> EnemyDetected;
        public event Action<GameObject> EnemyLost;
        
        [SerializeField] private CircleCollider2D _collider;
        
        private EnemySensorData _data;
        
        [Inject]
        private void Initialize(EnemySensorData data)
        {
            _data = data;
            
            SetRadius(_data.Radius);
        }
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            EnemyDetected?.Invoke(other.gameObject);
            
            Debug.Log($"Enemy detected: {other.gameObject}");
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            EnemyLost?.Invoke(other.gameObject);
            
            Debug.Log($"Enemy lost: {other.gameObject}");
        }

        public void SetRadius(float radius)
        {
            _collider.radius = radius;
        }
    }
    
    [Serializable]
    public class EnemySensorData
    {
        public float Radius;

        public EnemySensorData(float radius)
        {
            Radius = radius;
        }
    }
}