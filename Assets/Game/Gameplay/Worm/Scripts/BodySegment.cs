using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Gameplay.Worm
{
    public sealed class BodySegment : MonoBehaviour
    {
        public event Action FailedTakeDamage;
        public event Action SuccessTakeDamage;
        public event Action DestroySegment;

        public Queue<DamageType> RemainingDamageTypesToDestroy => _remainingDamageTypesToDestroy;
        
        [SerializeField] private List<DamageType> _requiredDamageTypesToDestroy;

        private Queue<DamageType> _remainingDamageTypesToDestroy;

        private void Awake()
        {
            _remainingDamageTypesToDestroy = new();
            
            foreach(var type in _requiredDamageTypesToDestroy)
            {
                _remainingDamageTypesToDestroy.Enqueue(type);
            }
        }

        public void TakeDamage(DamageType type)
        {
            var expectedDamageType = _remainingDamageTypesToDestroy.Peek();

            if (type != expectedDamageType)
            {
                FailedTakeDamage?.Invoke();
                return;
            }

            _remainingDamageTypesToDestroy.Dequeue();
            SuccessTakeDamage?.Invoke();

            if (_remainingDamageTypesToDestroy.Count <= 0)
            {
                DestroySegment?.Invoke();
            }
        }
    }

    public enum DamageType
    {
        Fire,
        Ice,
        Bolt,
        Punch
    }
}