using Game.GameEngine.Common;
using Game.UI.Bag.Ammo;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Game.Gameplay.Mines
{
    [Prototype]
    public sealed class Mine : MonoBehaviour, IPointerDownHandler
    {
        [SerializeField] private Slider _gatherProgressSlider;
        
        [SerializeField] private float _amountOfProgressToAddOnClick = 0.25f;

        [SerializeField] private AmmoBagView _ammoBag;

        private float _currentGatherProgress = 0f;
        
        public void OnPointerDown(PointerEventData eventData)
        {
            _currentGatherProgress += _amountOfProgressToAddOnClick;
            _currentGatherProgress = Mathf.Clamp(_currentGatherProgress, 0f, 1f);
            
            _gatherProgressSlider.value = _currentGatherProgress;

            if (_currentGatherProgress >= 1f)
            {
                Debug.Log("Add pack of bullets to player inventory here");
                _ammoBag.AddAmmo(null);
                _currentGatherProgress = 0f;
            }

            if (_currentGatherProgress <= 0f)
            {
                _gatherProgressSlider.gameObject.SetActive(false);
            }
            else
            {
                _gatherProgressSlider.gameObject.SetActive(true);
            }
        }
    }
}