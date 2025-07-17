using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Gameplay.Worm
{
    public sealed class Worm : MonoBehaviour
    {
        public event Action HeadChanged;
        public event Action HeadSuccessDamage;
        public event Action HeadFailedDamage;
        public event Action WormDestroyed;
        public BodySegment Head => _head;
        
        [SerializeField] private float _moveSpeed;
        [SerializeField] private float _amountOfPushBackOnSuccessDamage = 1f;
        [SerializeField] private Vector3 _moveVector;
        [SerializeField] private List<BodySegment> _bodySegments;

        [SerializeField] private BodySegment _head;

        private Queue<BodySegment> _bodySegmentsQueue;
        private Rigidbody2D _rigidbody;
        
        private void Awake()
        {
            _bodySegmentsQueue = new();
            
            foreach (var bodySegment in _bodySegments)
            {
                _bodySegmentsQueue.Enqueue(bodySegment);
            }
            
            MoveNextSegmentToHead();
            SubscribeOnHeadEvents();
        }

        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        private void MoveNextSegmentToHead()
        {
            if (_bodySegmentsQueue.Count <= 0)
            {
                WormDestroyed?.Invoke();
                return;
            }
            
            _head = _bodySegmentsQueue.Dequeue();
            HeadChanged?.Invoke();
        }

        private void SubscribeOnHeadEvents()
        {
            _head.SuccessTakeDamage += HeadOnSuccessTakeDamage;
            _head.FailedTakeDamage += HeadOnFailedTakeDamage;
            _head.DestroySegment += HeadOnDestroySegment;
        }

        private void UnsubscribeOnHeadEvents()
        {
            _head.SuccessTakeDamage -= HeadOnSuccessTakeDamage;
            _head.FailedTakeDamage -= HeadOnFailedTakeDamage;
            _head.DestroySegment -= HeadOnDestroySegment;
        }

        private void FixedUpdate()
        {
            _rigidbody.position += (Vector2)_moveVector * (_moveSpeed * Time.fixedDeltaTime);
        }

        public void TakeDamage(DamageType damageType)
        {
            _head.TakeDamage(damageType);
        }
        
        private void HeadOnDestroySegment()
        {
            UnsubscribeOnHeadEvents();
            Destroy(_head.gameObject);
            MoveNextSegmentToHead();
            SubscribeOnHeadEvents();
        }

        private void HeadOnFailedTakeDamage()
        {
            Debug.Log("Failed to damage head!");
            HeadFailedDamage?.Invoke();
        }

        private void HeadOnSuccessTakeDamage()
        {
            Debug.Log("Head damaged!");
            HeadSuccessDamage?.Invoke();
            PushBack();
        }

        private void PushBack()
        {
            var pushBackVector= -_moveVector * _amountOfPushBackOnSuccessDamage;
            _rigidbody.position += (Vector2)pushBackVector;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            Debug.Log($"Worm triggered with {other.gameObject.name}");

            PushBack();
        }
    }
}