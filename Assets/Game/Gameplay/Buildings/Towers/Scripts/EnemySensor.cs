using System;
using UnityEngine;
using VContainer;

namespace Game.Gameplay.Buildings
{
    [RequireComponent(typeof(CircleCollider2D))]
    public sealed class EnemySensor : MonoBehaviour, ITargetSensor
    {
        public event Action<ITarget> EnemyDetected;
        public event Action<ITarget> EnemyLost;
        
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
            if (!other.TryGetComponent(out ITarget target)) return;
            
            EnemyDetected?.Invoke(target);
            Debug.Log($"Enemy detected: {other.gameObject}");
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (!other.TryGetComponent(out ITarget target)) return;
            
            EnemyLost?.Invoke(target);
            Debug.Log($"Enemy lost: {other.gameObject}");
        }

        private void SetRadius(float radius)
        {
            _collider.radius = radius;
        }
    }
}