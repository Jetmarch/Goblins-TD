using System;

namespace Game.Gameplay.Towers
{
    public interface ITowerPresenter
    {
        void SetTarget(ITarget target);
        void LostTarget();
        event Action AttackRequest;
    }
}