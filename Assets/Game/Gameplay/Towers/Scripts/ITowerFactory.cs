using UnityEngine;

namespace Game.Gameplay.Buildings.Towers.Scripts
{
    public interface ITowerFactory
    {
        void CreateTower(string towerName, Vector2 position);
    }
}