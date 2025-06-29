using UnityEngine;

namespace Game.UI
{
    public class EntityHealthBarView : MonoBehaviour
    {
        [SerializeField] private Transform _healthBar;

        public void UpdateHealthBar(float currentHealth, float maxHealth)
        {
            var percentage = currentHealth / maxHealth;
            _healthBar.localScale = new Vector3(percentage, _healthBar.localScale.y, _healthBar.localScale.z);
        }
    }
}