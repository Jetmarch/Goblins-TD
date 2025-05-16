using System.Collections.Generic;
using UnityEngine;

namespace Game.GameEngine.GameplayManager
{
    public sealed class GameplayManager : IGameplayManager
    {
        private readonly IRewardManager _rewardManager;
        private readonly IPlayerStash _playerStash;

        public GameplayManager(IRewardManager rewardManager, IPlayerStash playerStash)
        {
            _rewardManager = rewardManager;
            _playerStash = playerStash;
        }

        public void EndGameWithResult(GameResult result)
        {
            ShowResultScreen(result);
            var rewards = _rewardManager.GetRewards();
            _playerStash.AddMultipleRewards(rewards);
        }

        private void ShowResultScreen(GameResult result)
        {
            Debug.Log($"Game Result {result}");
        }
    }

    public enum GameResult
    {
        Victory,
        Defeat
    }

    public interface IGameplayManager
    {
        void EndGameWithResult(GameResult result);
    }

    public interface IRewardManager
    {
        IReward[] GetRewards();
    }

    public interface IReward
    {
        
    }

    public interface IPlayerStash
    {
        IReadOnlyList<IReward> Rewards { get; }
        void AddReward(IReward reward);
        void AddMultipleRewards(IEnumerable<IReward> rewards);
    }
}