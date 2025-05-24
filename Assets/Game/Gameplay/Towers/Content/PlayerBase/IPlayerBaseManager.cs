using System;
using UnityEngine;

namespace Game.Gameplay.Towers.PlayerBase
{
    public interface IPlayerBaseManager
    {
        event Action BaseDestroyed;
        event Action BaseCreated;
        void NotifyBaseDestroyed();
        void CreatePlayerBase();
        GameObject GetPlayerBase();
    }
}