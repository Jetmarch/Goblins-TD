using System;

namespace Game.Gameplay.Buildings
{
    public interface ITowerPresenter
    {
        void SetTarget(ITarget target);
        void LostTarget();
        event Action AttackRequest;
    }
}