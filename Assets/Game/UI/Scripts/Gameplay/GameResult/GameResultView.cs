using System;
using Game.GameEngine.GameplayManager;
using UnityEngine;
using VContainer.Unity;

namespace Game.UI.GameResult
{
    public sealed class GameResultView : BasePanelView
    {
        public void ShowRewards(IReward[] rewards)
        {
            Show();
            if (rewards == null) return;
            foreach (var reward in rewards)
            {
                Debug.Log(reward);
            }
        }
    }

    public sealed class GameResultController : IInitializable, IDisposable
    {
        private readonly IGameplayManager _gameplayManager;
        private readonly GameResultView _gameResultView;
        
        private readonly IRewardManager _rewardManager;
        private readonly IPlayerStash _playerStash;

        public GameResultController(IGameplayManager gameplayManager, GameResultView gameResultView, IRewardManager rewardManager, IPlayerStash playerStash)
        {
            _gameplayManager = gameplayManager;
            _gameResultView = gameResultView;
            _rewardManager = rewardManager;
            _playerStash = playerStash;
        }

        public void Initialize()
        {
            _gameplayManager.GameEnd += ShowGameResult;
        }

        public void Dispose()
        {
            _gameplayManager.GameEnd -= ShowGameResult;
        }

        private void ShowGameResult(GameEngine.GameplayManager.GameResult _)
        {
            var rewards = _rewardManager.GetRewards();
            _playerStash.AddMultipleRewards(rewards);
            _gameResultView.ShowRewards(rewards);
        }
    }
}