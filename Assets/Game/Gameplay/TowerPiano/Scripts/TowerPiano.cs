using System;
using Game.Gameplay.Worm;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Gameplay.TowerPiano
{
    public sealed class TowerPiano : MonoBehaviour
    {
        [SerializeField] private Button _fireButton;
        [SerializeField] private Button _iceButton;
        [SerializeField] private Button _boltButton;
        [SerializeField] private Button _punchButton;

        [SerializeField] private Worm.Worm _worm;
        
        private void Start()
        {
            _fireButton.onClick.AddListener(OnFireButtonClick);
            _iceButton.onClick.AddListener(OnIceButtonClick);
            _boltButton.onClick.AddListener(OnBoltButtonClick);
            _punchButton.onClick.AddListener(OnPunchButtonClick);
        }

        private void OnFireButtonClick()
        {
            _worm.TakeDamage(DamageType.Fire);
        }
        
        private void OnIceButtonClick()
        {
            _worm.TakeDamage(DamageType.Ice);
        }
        
        private void OnBoltButtonClick()
        {
            _worm.TakeDamage(DamageType.Bolt);
        }
        
        private void OnPunchButtonClick()
        {
            _worm.TakeDamage(DamageType.Punch);
        }
    }
}