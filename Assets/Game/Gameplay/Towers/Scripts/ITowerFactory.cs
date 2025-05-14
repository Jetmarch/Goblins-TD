using UnityEngine;

namespace Game.Gameplay.Towers
{
    public interface ITowerFactory
    {
        void CreateTower(string towerName, Vector2 position);
    }
}