using System;

namespace Game.GameEngine.GameplayManager
{
    public interface IGameplayManager
    {
        void EndGameWithResult(GameResult result);
        void StartGame();
        event Action GameStart;
        event Action<GameResult> GameEnd;
    }
}