using System;
using UnityEngine;

namespace Game.Gameplay
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class BasicEnemy : MonoBehaviour
    {
        [SerializeField] private float _speed = 1f;
        [SerializeField] private float _currentHealth = 10f;
        [SerializeField] private Vector2 _direction;
        
        [SerializeField] private Rigidbody2D _rigidbody;
        
        [SerializeField] private Bounds _deathBounds;
        

        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate()
        {
            _rigidbody.MovePosition(_rigidbody.position + _direction * (_speed * Time.fixedDeltaTime));

            if (_deathBounds.Contains(_rigidbody.position)) return;
            
            Destroy(gameObject);
        }

        public void TakeDamage(float damage)
        {
            _currentHealth -= damage;

            if (_currentHealth <= 0)
            {
                Destroy(gameObject);
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(_deathBounds.center, _deathBounds.size);
        }
    }
}