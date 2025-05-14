using UnityEngine;

namespace Game.Gameplay.Towers
{
    [CreateAssetMenu(fileName = "TowerConfig", menuName = "Gameplay/Buildings/TowerConfig")]
    public class TowerConfig : ScriptableObject
    {
        [SerializeField] private TowerData _towerData;

        public TowerData GetPrototype()
        {
            return new TowerData(_towerData);
        }
    }
}