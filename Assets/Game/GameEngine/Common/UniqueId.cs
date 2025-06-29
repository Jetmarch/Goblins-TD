using System;
using UnityEngine;

namespace Game.GameEngine.Common
{
    [CreateAssetMenu(menuName = "GameEngine/Common/UniqueId", fileName = "UniqueId")]
    public sealed class UniqueId : ScriptableObject
    {
        public string Value => _uniqueId;
        [SerializeField] private string _uniqueId = Guid.NewGuid().ToString();
    }
}