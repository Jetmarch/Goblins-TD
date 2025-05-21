using System;
using TMPro;
using UnityEngine;

namespace Game.UI.PlayerBaseInfo
{
    public class PlayerBaseInfoView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _healthText;

        private void Start()
        {
            _healthText.text = "Fix me!";
        }
    }
}