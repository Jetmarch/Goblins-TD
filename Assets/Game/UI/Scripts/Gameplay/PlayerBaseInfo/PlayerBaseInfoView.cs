using System;
using Game.GameEngine.EntityComponents;
using Game.Gameplay.Towers.PlayerBase;
using TMPro;
using UnityEngine;
using VContainer;

namespace Game.UI.PlayerBaseInfo
{
    public sealed class PlayerBaseInfoView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _healthText;

        private IComponentStorage _playerBaseComponents;

        private IPlayerBaseManager _playerBaseManager;

        private HealthStorage _playerBaseHealth;

        private void OnEnable()
        {
            if (_playerBaseManager == null) return;
            _playerBaseManager.BaseCreated += PlayerBaseCreated;
            _playerBaseManager.BaseDestroyed += PlayerBaseDestroyed;
        }

        private void OnDisable()
        {
            if (_playerBaseManager == null) return;
            _playerBaseManager.BaseCreated -= PlayerBaseCreated;
            _playerBaseManager.BaseDestroyed -= PlayerBaseDestroyed;
        }
        
        private void PlayerBaseCreated()
        {
            var playerBase = _playerBaseManager.GetPlayerBase();
            _playerBaseComponents = playerBase.GetComponent<IComponentStorage>();
            _playerBaseHealth = _playerBaseComponents.GetHealthStorage();
            _playerBaseHealth.HealthChanged += UpdateInfo;
            if (_playerBaseComponents == null)
            {
                Debug.LogError($"Player base has no component storage!");
            }
            else
            {
                UpdateInfo();
            }
        }
        
        private void PlayerBaseDestroyed()
        {
            _playerBaseHealth.HealthChanged -= UpdateInfo;
        }


        [Inject]
        private void Construct(IPlayerBaseManager playerBaseManager)
        {
            _playerBaseManager = playerBaseManager;
        }

        private void UpdateInfo()
        {
            var healthStorage = _playerBaseComponents.GetHealthStorage();
            _healthText.text = $"{healthStorage.CurrentHealth}/{healthStorage.MaxHealth}";
        }
    }
}