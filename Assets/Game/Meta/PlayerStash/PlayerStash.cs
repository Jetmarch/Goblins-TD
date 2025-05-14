using System.Collections.Generic;
using Game.GameEngine.GameplayManager;

namespace Game.Meta.PlayerStash
{
    internal sealed class PlayerStash : IPlayerStash
    {
        private readonly List<IReward> _rewards = new List<IReward>();
        
        public void AddReward(IReward reward)
        {
            _rewards.Add(reward);
        }

        public void AddRewards(IEnumerable<IReward> rewards)
        {
            if (rewards == null) return;
            
            _rewards.AddRange(rewards);
        }
    }
}