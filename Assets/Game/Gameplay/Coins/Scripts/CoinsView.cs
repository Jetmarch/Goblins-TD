using Game.GameEngine.Common;
using TMPro;
using UnityEngine;

namespace Game.Gameplay.Coins
{
    [Prototype]
    public sealed class CoinsView : MonoBehaviour
    {
        [SerializeField] private CoinsStorage _coinsStorage;
        [SerializeField] private TextMeshProUGUI _coinsText;

        private void Start()
        {
            _coinsStorage.CoinsChanged += UpdateCoinsText;
            UpdateCoinsText();
        }

        private void UpdateCoinsText() => _coinsText.text = $"Coins: {_coinsStorage.Coins.ToString()}";
        
#if UNITY_EDITOR
        [ContextMenu("Add 100 coins")]        
        private void Add100Coins() => _coinsStorage.AddCoins(100);
#endif

    }
}