using System.Collections.Generic;
using UnityEngine;

namespace Modules.Core.GameLoop
{
    public sealed class GameLoopManager : MonoBehaviour, IGameLoopManager
    {
        private readonly List<IGameLoopListener> _gameLoopListeners = new();
        private readonly List<IUpdateListener> _updateListeners = new();
        private readonly List<IFixedUpdateListener> _fixedUpdateListeners = new();
        private readonly List<ILateUpdateListener> _lateUpdateListeners = new();

        private GameState _gameState;

        public void AddListener(IGameLoopListener listener)
        {
            // ReSharper disable once ConvertIfStatementToSwitchStatement
            if (listener is IUpdateListener updateListener)
            {
                if (!_updateListeners.Contains(updateListener))
                {
                    _updateListeners.Add(updateListener);
                }
            }

            if (listener is IFixedUpdateListener fixedUpdateListener)
            {
                if (!_fixedUpdateListeners.Contains(fixedUpdateListener))
                {
                    _fixedUpdateListeners.Add(fixedUpdateListener);
                }
            }

            if (listener is ILateUpdateListener lateUpdateListener)
            {
                if (!_lateUpdateListeners.Contains(lateUpdateListener))
                {
                    _lateUpdateListeners.Add(lateUpdateListener);
                }
            }

            if (!_gameLoopListeners.Contains(listener))
            {
                _gameLoopListeners.Add(listener);
            }
        }

        public void RemoveListener(IGameLoopListener listener)
        {
            if (listener is IUpdateListener updateListener)
            {
                if (_updateListeners.Contains(updateListener))
                {
                    _updateListeners.Remove(updateListener);
                }
            }

            if (listener is IFixedUpdateListener fixedUpdateListener)
            {
                if (_fixedUpdateListeners.Contains(fixedUpdateListener))
                {
                    _fixedUpdateListeners.Remove(fixedUpdateListener);
                }
            }

            if (listener is ILateUpdateListener lateUpdateListener)
            {
                if (_lateUpdateListeners.Contains(lateUpdateListener))
                {
                    _lateUpdateListeners.Remove(lateUpdateListener);
                }
            }

            if (_gameLoopListeners.Contains(listener))
            {
                _gameLoopListeners.Remove(listener);
            }
        }
        
        private void Update()
        {
            if (_gameState != GameState.Running) return;
            
            var deltaTime = Time.deltaTime;
            for (int i = 0; i < _updateListeners.Count; i++)
            {
                _updateListeners[i].OnUpdate(deltaTime);
            }
        }

        private void FixedUpdate()
        {
            if (_gameState != GameState.Running) return;
            
            var deltaTime = Time.deltaTime;
            for (int i = 0; i < _fixedUpdateListeners.Count; i++)
            {
                _fixedUpdateListeners[i].OnFixedUpdate(deltaTime);
            }
        }

        private void LateUpdate()
        {
            if (_gameState != GameState.Running) return;
            
            var deltaTime = Time.deltaTime;
            for (int i = 0; i < _lateUpdateListeners.Count; i++)
            {
                _lateUpdateListeners[i].OnLateUpdate(deltaTime);
            }
        }

        [ContextMenu("Pause game")]
        public void PauseGame()
        {
            _gameState = GameState.Pause;

            for (int i = 0; i < _gameLoopListeners.Count; i++)
            {
                if (_gameLoopListeners[i] is IPauseListener pauseListener)
                {
                    pauseListener.OnPause();
                }
            }
        }

        [ContextMenu("Resume game")]
        public void ResumeGame()
        {
            _gameState = GameState.Running;
            
            for (int i = 0; i < _gameLoopListeners.Count; i++)
            {
                if (_gameLoopListeners[i] is IPauseListener pauseListener)
                {
                    pauseListener.OnResume();
                }
            }
        }
        
        [ContextMenu("Start game")]
        public void StartGame()
        {
            _gameState = GameState.Running;
        }
        
        [ContextMenu("Finish game")]
        public void FinishGame()
        {
            _gameState = GameState.Finished;
        }
        
#if UNITY_EDITOR
        private void OnGUI()
        {
            GUI.color = Color.green;
            GUI.Label(new Rect(10, 10, 300, 30), $"Game state: {_gameState}", new GUIStyle()
            {
                fontStyle = FontStyle.Bold,
                fontSize = 28,
                
            });
            
            if(GUI.Button(new Rect(330, 10, 300, 30), "Toggle game"))
            {
                if (_gameState == GameState.Running)
                {
                    PauseGame();
                }
                else
                {
                    ResumeGame();
                }
            }
        }
#endif
    }
}
