using System;

namespace Game.GameEngine.GameplayManager
{
    public sealed class GameplayManager : IGameplayManager
    {
        public event Action<GameResult> GameEnd;
        public event Action GameStart;

        public void EndGameWithResult(GameResult result)
        {
            GameEnd?.Invoke(result);
        }

        public void StartGame()
        {
            GameStart?.Invoke();
        }
    }
}