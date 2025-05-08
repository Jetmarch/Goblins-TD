using UnityEngine;

namespace Game.Gameplay.Buildings
{
    [CreateAssetMenu(fileName = "TowerConfig", menuName = "Gameplay/Buildings/TowerConfig")]
    public class TowerConfig : ScriptableObject
    {
        [SerializeField] private TowerModel _towerModel;

        public TowerModel GetPrototype()
        {
            return new TowerModel(_towerModel);
        }
    }
}