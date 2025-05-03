using System.Collections.Generic;
using UnityEngine;

namespace Game.Gameplay.WaveSystem
{
    [CreateAssetMenu(fileName = "WaveDataConfig", menuName = "Gameplay/WaveDataConfig")]
    public class WaveDataConfig : ScriptableObject
    {
        public List<WaveData> Waves => _waves;
        
        [SerializeField] private List<WaveData> _waves;
    }
}
