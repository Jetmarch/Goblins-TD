using Game.GameEngine;
using Game.UI;
using UnityEngine;

namespace Game.Gameplay
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class BasicEnemy : MonoBehaviour
    {
        [SerializeField] private float _speed = 1f;
        [SerializeField] private HealthStorage _healthStorage;
        [SerializeField] private Vector2 _direction;
        
        [SerializeField] private Rigidbody2D _rigidbody;
        
        [SerializeField] private Bounds _deathBounds;
        
        [SerializeField] private EntityHealthBarView _healthBarView;
        

        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _healthStorage.Reset();
        }

        private void OnEnable()
        {
            _healthStorage.OnDeath += Die;
            _healthStorage.OnHealthChanged += UpdateHealthBar;
        }

        private void OnDisable()
        {
            _healthStorage.OnDeath -= Die;
            _healthStorage.OnHealthChanged -= UpdateHealthBar;
        }

        private void FixedUpdate()
        {
            _rigidbody.MovePosition(_rigidbody.position + _direction * (_speed * Time.fixedDeltaTime));

            if (_deathBounds.Contains(_rigidbody.position)) return;
            
            Destroy(gameObject);
        }

        public void TakeDamage(float damage)
        {
            _healthStorage.DecreaseHealth(damage);
            
        }

        private void UpdateHealthBar()
        {
            _healthBarView.UpdateHealthBar(_healthStorage.CurrentHealth, _healthStorage.MaxHealth);
        }

        private void Die()
        {
            Destroy(gameObject);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(_deathBounds.center, _deathBounds.size);
        }
    }
}