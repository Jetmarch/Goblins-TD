using System.Collections.Generic;

namespace Game.GameEngine.GameplayManager
{
    public interface IPlayerStash
    {
        IReadOnlyList<IReward> Rewards { get; }
        void AddReward(IReward reward);
        void AddMultipleRewards(IEnumerable<IReward> rewards);
    }
}