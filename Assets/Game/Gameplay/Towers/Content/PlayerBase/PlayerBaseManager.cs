using System;
using Game.Gameplay.Controllers;

namespace Game.Gameplay.Towers.PlayerBase
{
    internal sealed class PlayerBaseManager : IPlayerBaseManager
    {
        public event Action BaseDestroyed;

        public void NotifyBaseDestroyed()
        {
            BaseDestroyed?.Invoke();
        }
    }
}