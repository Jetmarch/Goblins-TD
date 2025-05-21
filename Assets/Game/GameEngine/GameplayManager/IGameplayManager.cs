using System;

namespace Game.GameEngine.GameplayManager
{
    public interface IGameplayManager
    {
        void EndGameWithResult(GameResult result);
        event Action<GameResult> GameEnd;
    }
}