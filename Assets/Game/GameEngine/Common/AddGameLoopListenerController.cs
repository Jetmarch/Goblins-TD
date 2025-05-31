using System;
using System.Collections.Generic;
using Modules.Core.GameLoop;
using VContainer.Unity;

namespace Game.GameEngine.Common
{
    public sealed class AddGameLoopListenerController : IInitializable, IDisposable
    {
        private readonly IEnumerable<IGameLoopListener> _listeners;
        private readonly IGameLoopManager _gameLoopManager;

        public AddGameLoopListenerController(IEnumerable<IGameLoopListener> listeners, IGameLoopManager gameLoopManager)
        {
            _listeners = listeners;
            _gameLoopManager = gameLoopManager;
        }


        public void Initialize()
        {
            foreach (var listener in _listeners)
            {
                _gameLoopManager.AddListener(listener);
            }
        }

        public void Dispose()
        {
            foreach (var listener in _listeners)
            {
                _gameLoopManager.RemoveListener(listener);
            }
        }
    }
}