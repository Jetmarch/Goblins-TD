using UnityEngine;
using UnityEngine.Serialization;

namespace Game.Gameplay.Buildings
{
    [CreateAssetMenu(fileName = "TowerConfig", menuName = "Gameplay/Buildings/TowerConfig")]
    public class TowerConfig : ScriptableObject
    {
        [FormerlySerializedAs("_towerModel")] [SerializeField] private TowerData _towerData;

        public TowerData GetPrototype()
        {
            return new TowerData(_towerData);
        }
    }
}